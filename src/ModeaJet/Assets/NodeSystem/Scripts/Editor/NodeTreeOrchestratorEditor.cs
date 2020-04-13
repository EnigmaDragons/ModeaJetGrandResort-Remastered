using System;
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
            
            if (!GUILayout.Button("Refresh")) return;
            
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
                    
            orchestrator.NodeTrees = Resources.LoadAll<TextAsset>("NodeTrees");
            orchestrator.CurrentNodeTree = ScriptableExtensions.GetAllInstances<CurrentNodeTree>().First();
            //orchestrator.Conditions = ScriptableExtensions.GetAllInstancesByLabel("condition")
            //    .Cast<NodeConditionHandler>().ToArray();
        }
    }
}