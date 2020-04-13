using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor.Graphs;
using UnityEngine;

namespace EnigmaDragons.NodeSystem
{
    public class NodeTreeOrchestrator : MonoBehaviour
    {
        #region Scriptables Accessible By Node Tree System
        [SerializeField] private BoolVariable[] bools;
        [SerializeField] private StringVariable[] strings;
        [SerializeField] private IntVariable[] ints;
        [SerializeField] private FloatVariable[] floats;
        [SerializeField] private Vector3Variable[] vector3s;
        [SerializeField] private ColorReference[] colors;
        [SerializeField] private Character[] characters;
        #endregion

        #region Messages To Wait For Map
        private readonly DictionaryWithDefault<Type, Type[]> _messagesToWaitForMap = new DictionaryWithDefault<Type, Type[]>(new Type[0])
        {
            { typeof(ShowStatement), new Type[] { typeof(CommandFinished<ShowStatement>) } },
            { typeof(ShowOptions), new Type[] { typeof(CommandFinished<ShowOptions>) } },
        };
        #endregion

        #region Main Code
        public CurrentNodeTree CurrentNodeTree;
        public TextAsset[] NodeTrees;
        public NodeConditionHandler[] Conditions;

        private static readonly Dictionary<string, Dictionary<string, Func<INodeCommand>>> _nodeTreeCommandMap = new Dictionary<string, Dictionary<string, Func<INodeCommand>>>();
        private static readonly Dictionary<string, Dictionary<string, Func<INodeCondition>>> _nodeTreeConditionMap = new Dictionary<string, Dictionary<string, Func<INodeCondition>>>();
        private static readonly Dictionary<string, Dictionary<string, Func<INodeObject>>> _nodeTreeObjectMap = new Dictionary<string, Dictionary<string, Func<INodeObject>>>();
        private static Dictionary<Type, NodeConditionHandler> _conditionMap;
        private static Dictionary<int, ScriptableObject> _assetMap;
        private readonly IMediaType _mediaType = new JsonMediaType();

        private NodeTreeData _nodeTree;
        private List<Type> _messagesToWaitFor = new List<Type>();

        private void OnEnable()
        {
            if (_assetMap == null)
                _assetMap = typeof(NodeTreeOrchestrator).GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                    .Where(x => x.FieldType.IsArray && typeof(ScriptableObject).IsAssignableFrom(x.FieldType.GetElementType()))
                    .SelectMany(x => (ScriptableObject[])x.GetValue(this))
                    .ToDictionary(x => x.GetInstanceID(), x => x);
            if (_conditionMap == null)
                _conditionMap = Conditions.ToDictionary(x => x.CondtionType, x => x);
            Message.Subscribe<NodeTreeChanged>(Execute, this);
            Message.Subscribe<MessageProcessed>(Execute, this);
        }

        private void OnDisable()
        {
            Message.Unsubscribe(this);
        }

        private void Execute(NodeTreeChanged msg)
        {
            if (CurrentNodeTree.IsActive)
            {
                _messagesToWaitFor = new List<Type>();
                CurrentNodeTree.ShouldEnd = false;
                _nodeTree = _mediaType.ConvertFrom<NodeTreeData>(CurrentNodeTree.NodeTree.text);
                EnsureNodeTreeObjectsCreated();
                CurrentNodeTree.NextNodeIds = _nodeTree.StartIds;
                Next();
            }
        }

        private void Execute(MessageProcessed msg)
        {
            if (_messagesToWaitFor.Any())
            {
                _messagesToWaitFor.Remove(msg.MessageType);
                if (!_messagesToWaitFor.Any())
                    Next();
            }
        }

        private void EnsureNodeTreeObjectsCreated()
        {
            if (!_nodeTreeCommandMap.ContainsKey(CurrentNodeTree.NodeTree.name)
                || !_nodeTreeConditionMap.ContainsKey(CurrentNodeTree.NodeTree.name)
                || !_nodeTreeObjectMap.ContainsKey(CurrentNodeTree.NodeTree.name))
            {
                _nodeTreeCommandMap[CurrentNodeTree.NodeTree.name] = new Dictionary<string, Func<INodeCommand>>();
                _nodeTreeConditionMap[CurrentNodeTree.NodeTree.name] = new Dictionary<string, Func<INodeCondition>>();
                _nodeTreeObjectMap[CurrentNodeTree.NodeTree.name] = new Dictionary<string, Func<INodeObject>>();
                foreach (var node in _nodeTree.Nodes)
                {
                    var nodeType = Type.GetType(node.Type);
                    if (typeof(INodeCommand).IsAssignableFrom(nodeType))
                        _nodeTreeCommandMap[CurrentNodeTree.NodeTree.name][node.Id] = GetBuildNodeFunc<INodeCommand>(node);
                    if (typeof(INodeCondition).IsAssignableFrom(nodeType))
                        _nodeTreeConditionMap[CurrentNodeTree.NodeTree.name][node.Id] = GetBuildNodeFunc<INodeCondition>(node);
                    if (typeof(INodeObject).IsAssignableFrom(nodeType))
                        _nodeTreeObjectMap[CurrentNodeTree.NodeTree.name][node.Id] = GetBuildNodeFunc<INodeObject>(node);
                }
            }
        }

        private Func<T> GetBuildNodeFunc<T>(NodeData node)
        {
            List<Action<object>> modifications = new List<Action<object>>();
            Dictionary<string, PropertyInfo> props = Type.GetType(node.Type).GetProperties().Where(x => x.CanWrite).ToDictionary(x => x.Name, x => x);
            var type = Type.GetType(node.Type);
            foreach (var prop in node.Properties)
            {
                if (props[prop.Key].Name == "NodeTreeIds")
                {
                    modifications.Add(x => props[prop.Key].SetValue(x, node.NextIds));
                }
                else if (props[prop.Key].PropertyType == typeof(bool))
                {
                    var value = bool.Parse(prop.Value);
                    modifications.Add(x => props[prop.Key].SetValue(x, value));
                }
                else if (props[prop.Key].PropertyType == typeof(string))
                {
                    var value = prop.Value;
                    modifications.Add(x => props[prop.Key].SetValue(x, value));
                }
                else if (props[prop.Key].PropertyType == typeof(int))
                {
                    var value = int.Parse(prop.Value);
                    modifications.Add(x => props[prop.Key].SetValue(x, value));
                }
                else if (props[prop.Key].PropertyType == typeof(float))
                {
                    var value = float.Parse(prop.Value);
                    modifications.Add(x => props[prop.Key].SetValue(x, value));
                }
                else if (props[prop.Key].PropertyType == typeof(TextAsset))
                {
                    var value = NodeTrees.First(x => x.GetInstanceID() == int.Parse(prop.Value));
                    modifications.Add(x => props[prop.Key].SetValue(x, value));
                }
                else if (typeof(ScriptableObject).IsAssignableFrom(props[prop.Key].PropertyType))
                {
                    var val = int.Parse(prop.Value);
                    if (!_assetMap.ContainsKey(val))
                        Debug.LogError($"AssetMap does not contain Id {val}");
                    var value = _assetMap[val];
                    modifications.Add(x => props[prop.Key].SetValue(x, value));
                }
            }
            var propsToPopulate = props
                .Where(x => typeof(INodeCommand[]).IsAssignableFrom(x.Value.PropertyType)
                    || typeof(INodeCondition[]).IsAssignableFrom(x.Value.PropertyType)
                    || typeof(INodeObject[]).IsAssignableFrom(x.Value.PropertyType))
                .Select(x => x.Value)
                .ToArray();
            if (propsToPopulate.Any())
                modifications.Add(x => PopulateNodeTypes(node, x, propsToPopulate));
            return () =>
            {
                var instance = Activator.CreateInstance(type);
                modifications.ForEach(x => x(instance));
                return (T)instance;
            };
        }

        private void PopulateNodeTypes(NodeData node, object nodeInstance, PropertyInfo[] propsToPopulate)
        {
            var commands = node.NextIds
                .Where(x => _nodeTreeCommandMap[CurrentNodeTree.NodeTree.name].ContainsKey(x))
                .Select(x => _nodeTreeCommandMap[CurrentNodeTree.NodeTree.name][x]())
                .ToArray();
            var conditions = node.NextIds
                .Where(x => _nodeTreeConditionMap[CurrentNodeTree.NodeTree.name].ContainsKey(x))
                .ToDictionary(x => _nodeTree[x], x => _nodeTreeConditionMap[CurrentNodeTree.NodeTree.name][x]());
            var objects = node.NextIds
                .Where(x => _nodeTreeObjectMap[CurrentNodeTree.NodeTree.name].ContainsKey(x))
                .Select(x => _nodeTreeObjectMap[CurrentNodeTree.NodeTree.name][x]())
                .ToArray();
            foreach (var prop in propsToPopulate)
            {
                if (typeof(INodeCommand[]).IsAssignableFrom(prop.PropertyType))
                    ReflectionUtilities.SetValueValueOnProperty(prop, nodeInstance, 
                        commands.Where(command => prop.PropertyType.GetElementType().IsAssignableFrom(command.GetType()))
                            .Concat(conditions
                                .Where(condition => IsConditionMet(condition.Value))
                                .SelectMany(condition => condition.Key.NextIds
                                    .Where(id => _nodeTreeCommandMap[CurrentNodeTree.NodeTree.name].ContainsKey(id))
                                    .Select(id => _nodeTreeCommandMap[CurrentNodeTree.NodeTree.name][id]())
                                    .Where(command => prop.PropertyType.GetElementType().IsAssignableFrom(command.GetType()))))
                            .ToArray());
                if (typeof(INodeCondition[]).IsAssignableFrom(prop.PropertyType))
                    ReflectionUtilities.SetValueValueOnProperty(prop, nodeInstance, 
                        conditions.Where(x => prop.PropertyType.GetElementType().IsAssignableFrom(x.Value.GetType())).Select(x => x.Value).ToArray());
                if (typeof(INodeObject[]).IsAssignableFrom(prop.PropertyType))
                    ReflectionUtilities.SetValueValueOnProperty(prop, nodeInstance, 
                        objects.Where(x => prop.PropertyType.GetElementType().IsAssignableFrom(x.GetType()))
                            .Concat(conditions
                                .Where(condition => IsConditionMet(condition.Value))
                                .SelectMany(condition => condition.Key.NextIds
                                    .Where(id => _nodeTreeObjectMap[CurrentNodeTree.NodeTree.name].ContainsKey(id))
                                    .Select(id => _nodeTreeObjectMap[CurrentNodeTree.NodeTree.name][id]())
                                    .Where(command => prop.PropertyType.GetElementType().IsAssignableFrom(command.GetType()))))
                            .ToArray());
            }
        }

        private void Next()
        {
            if (CurrentNodeTree.ShouldEnd)
                CurrentNodeTree.IsActive = false;
            else
            {
                for (var i = 0; i < CurrentNodeTree.NextNodeIds.Length; i++)
                {
                    if (_nodeTreeCommandMap[CurrentNodeTree.NodeTree.name].ContainsKey(CurrentNodeTree.NextNodeIds[i]))
                    {
                        ExecuteCommand(_nodeTree[CurrentNodeTree.NextNodeIds[i]]);
                        return;
                    }
                    else if (_nodeTreeConditionMap[CurrentNodeTree.NodeTree.name].ContainsKey(CurrentNodeTree.NextNodeIds[i]))
                    {
                        ResolveCondition(_nodeTree[CurrentNodeTree.NextNodeIds[i]]);
                        return;
                    }
                }
                CurrentNodeTree.IsActive = false;
            }
        }

        private void ExecuteCommand(NodeData node)
        {
            if (CurrentNodeTree.IsDebug)
                Debug.Log($"Executing Command: {Type.GetType(node.Type).Name}");
            CurrentNodeTree.NextNodeIds = node.NextIds.ToArray();
            var command = _nodeTreeCommandMap[CurrentNodeTree.NodeTree.name][node.Id]();
            _messagesToWaitFor = _messagesToWaitForMap[command.GetType()].ToList();
            if (CurrentNodeTree.IsDebug && _messagesToWaitFor.Any())
                Debug.Log($"Waiting For Messages: {string.Join(", ", _messagesToWaitFor.Select(x => x.Name))}");
            Message.Publish(command);
            if (!_messagesToWaitFor.Any())
                Next();
        }

        private void ResolveCondition(NodeData node)
        {
            if (IsConditionMet(node))
            {
                if (CurrentNodeTree.IsDebug)
                    Debug.Log($"Evaluated Condition To Be True: {Type.GetType(node.Type).Name}");
                CurrentNodeTree.NextNodeIds = node.NextIds.Where(x => _nodeTreeCommandMap[CurrentNodeTree.NodeTree.name].ContainsKey(x)).ToArray();
                Next();
            }
            else if(CurrentNodeTree.IsDebug)
            {
                Debug.Log($"Evaluated Condition To Be True: {Type.GetType(node.Type).Name}");
            }
        }

        private bool IsConditionMet(NodeData node)
            => IsConditionMet(_nodeTreeConditionMap[CurrentNodeTree.NodeTree.name][node.Id]());

        private bool IsConditionMet(INodeCondition condition)
            => _conditionMap[condition.GetType()].IsConditionMet(condition);

        #endregion
    }
}