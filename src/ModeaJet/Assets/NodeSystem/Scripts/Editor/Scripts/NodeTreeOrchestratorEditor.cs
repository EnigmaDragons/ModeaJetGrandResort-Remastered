using System;
using System.Linq;
using EnigmaDragons.NodeSystem;
using UnityEditor;
using UnityEngine;

namespace EnigmaDragons.NodeSystem
{
    [CustomEditor(typeof(NodeTreeOrchestrator))]
    public class NodeTreeOrchestratorEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Refresh"))
            {
                var castMethod = this.GetType().GetMethod("Cast");
                var orchestrator = (NodeTreeOrchestrator)target;
                var fields = typeof(NodeTreeOrchestrator).GetFields().ToArray();
                fields.Where(x =>
                        x.FieldType.IsArray && typeof(ScriptableObject).IsAssignableFrom(x.FieldType.GetElementType()))
                    .ToArray()
                    .ForEach(x => x.SetValue(target, castMethod.MakeGenericMethod(x.FieldType).Invoke(null, new [] { ScriptableExtensions.GetAllInstances(x.FieldType.GetElementType()) })));
                orchestrator.NodeTrees = Resources.LoadAll<TextAsset>("NodeTrees");
                orchestrator.CurrentNodeTree = ScriptableExtensions.GetAllInstances<CurrentNodeTree>().First();
                //orchestrator.Conditions = ScriptableExtensions.GetAllInstancesByLabel("condition")
                //    .Cast<NodeConditionHandler>().ToArray();
            }
        }

        private static T Cast<T>(object o)
        {
            return (T)o;
        }
    }
}