﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        #endregion

        #region Messages To Wait For Map
        private readonly Dictionary<Type, Type[]> _messagesToWaitForMap = new DictionaryWithDefault<Type, Type[]>(new Type[0]);
        #endregion

        #region Main Code
        public CurrentNodeTree CurrentNodeTree;
        public TextAsset[] NodeTrees;
        public NodeConditionHandler[] Conditions;

        private static readonly Dictionary<string, Dictionary<string, INodeCommand>> _nodeTreeCommandMap = new Dictionary<string, Dictionary<string, INodeCommand>>();
        private static readonly Dictionary<string, Dictionary<string, INodeCondition>> _nodeTreeConditionMap = new Dictionary<string, Dictionary<string, INodeCondition>>();
        private static Dictionary<Type, NodeConditionHandler> _conditionMap;
        private static Dictionary<int, ScriptableObject> _assetMap;
        private readonly IMediaType _mediaType = new JsonMediaType();

        private NodeTreeData _nodeTree;
        private List<Type> _messagesToWaitFor = new List<Type>();

        private void OnEnable()
        {
            if (_assetMap == null)
                _assetMap = typeof(NodeTreeOrchestrator).GetFields()
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
                || !_nodeTreeConditionMap.ContainsKey(CurrentNodeTree.NodeTree.name))
            {
                _nodeTreeCommandMap[CurrentNodeTree.NodeTree.name] = new Dictionary<string, INodeCommand>();
                _nodeTreeConditionMap[CurrentNodeTree.NodeTree.name] = new Dictionary<string, INodeCondition>();
                _nodeTree.Nodes.ForEach(x =>
                {
                    var nodeInstance = BuildNode(x.Value);
                    if (nodeInstance is INodeCommand)
                        _nodeTreeCommandMap[CurrentNodeTree.NodeTree.name][x.Value.Id] = (INodeCommand)nodeInstance;
                    if (nodeInstance is INodeCondition)
                        _nodeTreeConditionMap[CurrentNodeTree.NodeTree.name][x.Value.Id] = (INodeCondition)nodeInstance;
                });
            }
        }

        private object BuildNode(NodeData node)
        {
            Dictionary<string, PropertyInfo> props = Type.GetType(node.Type).GetProperties().Where(x => x.CanWrite).ToDictionary(x => x.Name, x => x);
            var nodeInstance = Activator.CreateInstance(Type.GetType(node.Type));
            foreach (var prop in node.Properties)
            {
                if (props[prop.Key].PropertyType == typeof(bool))
                    props[prop.Key].SetValue(nodeInstance, bool.Parse(prop.Value));
                if (props[prop.Key].PropertyType == typeof(string))
                    props[prop.Key].SetValue(nodeInstance, prop.Value);
                if (props[prop.Key].PropertyType == typeof(int))
                    props[prop.Key].SetValue(nodeInstance, int.Parse(prop.Value));
                if (props[prop.Key].PropertyType == typeof(float))
                    props[prop.Key].SetValue(nodeInstance, float.Parse(prop.Value));
                if (props[prop.Key].PropertyType == typeof(TextAsset))
                    props[prop.Key].SetValue(nodeInstance, NodeTrees.First(x => x.GetInstanceID() == int.Parse(prop.Value)));
                if (typeof(ScriptableObject).IsAssignableFrom(props[prop.Key].PropertyType))
                    props[prop.Key].SetValue(nodeInstance, _assetMap[int.Parse(prop.Value)]);
            }
            return nodeInstance;
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
                        ExecuteCommand(_nodeTree.Nodes[CurrentNodeTree.NextNodeIds[i]]);
                        return;
                    }
                    else if (_nodeTreeConditionMap[CurrentNodeTree.NodeTree.name].ContainsKey(CurrentNodeTree.NextNodeIds[i]))
                    {
                        ResolveCondition(_nodeTree.Nodes[CurrentNodeTree.NextNodeIds[i]]);
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
            _messagesToWaitFor = _messagesToWaitForMap[_nodeTreeCommandMap[node.Id].GetType()].ToList();
            if (CurrentNodeTree.IsDebug && _messagesToWaitFor.Any())
                Debug.Log($"Waiting For Messages: {string.Join(", ", _messagesToWaitFor.Select(x => x.Name))}");
            Message.Publish(_nodeTreeCommandMap[node.Id]);
            if (!_messagesToWaitFor.Any())
                Next();
        }

        private void ResolveCondition(NodeData node)
        {
            if (_conditionMap[Type.GetType(node.Type)].IsConditionMet(_nodeTreeConditionMap[CurrentNodeTree.NodeTree.name][node.Id]))
            {
                if (CurrentNodeTree.IsDebug)
                    Debug.Log($"Evaluated Condition To Be True: {Type.GetType(node.Type).Name}");
                CurrentNodeTree.NextNodeIds = node.NextIds.Where(x => _nodeTreeCommandMap.ContainsKey(x)).ToArray();
                Next();
            }
            else if(CurrentNodeTree.IsDebug)
            {
                Debug.Log($"Evaluated Condition To Be True: {Type.GetType(node.Type).Name}");
            }
        }
        #endregion
    }
}