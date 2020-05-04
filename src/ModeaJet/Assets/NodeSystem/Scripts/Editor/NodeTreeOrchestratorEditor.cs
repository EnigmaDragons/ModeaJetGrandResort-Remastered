using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace EnigmaDragons.NodeSystem
{
    [CustomEditor(typeof(NodeTreeOrchestrator))]
    public class NodeTreeOrchestratorEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector(); // Maybe useful for seeing what is loaded. Can be disabled if desired.
            if (GUILayout.Button("Refresh")) 
                LoadAssets();
            if (GUILayout.Button("Validate")) 
                Validate();
        }

        public void LoadAssets()
        {
            var orchestrator = (NodeTreeOrchestrator)target;
            var scriptableObjectArrayFields = typeof(NodeTreeOrchestrator)
                .GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(x => x.FieldType.IsArray && typeof(ScriptableObject).IsAssignableFrom(x.FieldType.GetElementType()))
                .ToArray();
            foreach (var f in scriptableObjectArrayFields)
            {
                var arrItemType = f.FieldType.GetElementType();
                var instances = ScriptableExtensions.GetAllInstances(arrItemType);
                ReflectionUtilities.SetArrayValueOnField(f, target, instances);
            }

            orchestrator.NodeTrees = Resources.LoadAll<TextAsset>("NodeTrees").Where(x => !x.name.ToLower().Contains("autosave")).ToArray();
            orchestrator.CurrentNodeTree = ScriptableExtensions.GetAllInstances<CurrentNodeTree>().First();
        }

        public void Validate()
        {
            var orchestrator = (NodeTreeOrchestrator)target;
            orchestrator.OnEnable();
            var mediaType = new JsonMediaType();
            foreach (var nodeTreeResource in orchestrator.NodeTrees)
            {
                NodeTreeData nodeTree = null;
                try
                {
                    nodeTree = mediaType.ConvertFrom<NodeTreeData>(nodeTreeResource.text);
                }
                catch (Exception ex)
                {
                    Debug.LogError($"File: {nodeTreeResource.name} was not a valid NodeTreeData object");
                    continue;
                }
                if (!nodeTree.StartIds.Any())
                    Debug.LogError($"Node Tree: {nodeTreeResource.name} has no starting point");
                foreach (var node in nodeTree.Nodes)
                {
                    Type nodeType = null;
                    try
                    {
                        nodeType = Type.GetType(node.Type);
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError($"Node Tree: {nodeTreeResource.name} had Node: {node.Id} with no matching type {node.Type}");
                        continue;
                    }
                    Dictionary<string, PropertyInfo> props = nodeType.GetProperties().Where(x => x.CanWrite).ToDictionary(x => x.Name, x => x);
                    foreach (var prop in node.Properties)
                    {
                        if (!props.ContainsKey(prop.Key))
                        {
                            Debug.LogError($"Node Tree: {nodeTreeResource.name} has Node: {node.Id} which has the Property: {prop.Key} but the corresponding Type: {nodeType.Name} is missing that property");
                            continue;
                        }
                        if (props[prop.Key].PropertyType == typeof(bool))
                        {
                            if (!bool.TryParse(prop.Value, out _))
                                Debug.LogError($"Node Tree: {nodeTreeResource.name} has Node: {node.Id} of Type: {nodeType.Name} had a Property: {prop.Key} with a value of {prop.Value} that was not a bool");
                        }
                        else if (props[prop.Key].PropertyType == typeof(int))
                        {
                            if (!int.TryParse(prop.Value, out _))
                                Debug.LogError($"Node Tree: {nodeTreeResource.name} has Node: {node.Id} of Type: {nodeType.Name} had a Property: {prop.Key} with a value of {prop.Value} that was not a int");
                        }
                        else if (props[prop.Key].PropertyType == typeof(float))
                        {
                            if (!float.TryParse(prop.Value, out _))
                                Debug.LogError($"Node Tree: {nodeTreeResource.name} has Node: {node.Id} of Type: {nodeType.Name} had a Property: {prop.Key} with a value of {prop.Value} that was not a float");
                        }
                        else if (props[prop.Key].PropertyType == typeof(TextAsset))
                        {
                            if (orchestrator.NodeTrees.All(x => x.name != prop.Value))
                                Debug.LogError($"Node Tree: {nodeTreeResource.name} has Node: {node.Id} of Type: {nodeType.Name} had a Property: {prop.Key} with a value of {prop.Value}, but there was no NodeTree with that name");
                        }
                        else if (typeof(ScriptableObject).IsAssignableFrom(props[prop.Key].PropertyType))
                        {
                            if (!NodeTreeOrchestrator.AssetMap.ContainsKey(prop.Value))
                                Debug.LogError($"Node Tree: {nodeTreeResource.name} has Node: {node.Id} of Type: {nodeType.Name} had a Property: {prop.Key} with a value of {prop.Value}, but there was no Asset found with that name in AssetMap (Try Refreshing)");
                        }
                    }
                }
            }
            Debug.Log("Validation Complete");
        }
    }
}