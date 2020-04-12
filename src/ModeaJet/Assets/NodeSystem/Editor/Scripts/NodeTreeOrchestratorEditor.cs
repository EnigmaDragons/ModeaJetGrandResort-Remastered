using System.Linq;
using UnityEditor;
using UnityEngine;

namespace EnigmaDragons.NodeSystem.Editor
{
    [CustomEditor(typeof(NodeTreeOrchestrator))]
    public class NodeTreeOrchestratorEditor : UnityEditor.Editor
    {
        public void OnInspectorGUI()
        {
            if (GUILayout.Button("Refresh"))
            {
                var orchestrator = (NodeTreeOrchestrator) target;
                var fields = typeof(NodeTreeOrchestrator).GetFields().ToArray();
                fields.Where(x =>
                        x.FieldType.IsArray && typeof(ScriptableObject).IsAssignableFrom(x.FieldType.GetElementType()))
                    .ToArray()
                    .ForEach(
                        x => x.SetValue(target, ScriptableExtensions.GetAllInstances(x.FieldType.GetElementType())));
                orchestrator.NodeTrees = Resources.LoadAll<TextAsset>("NodeTrees");
                orchestrator.CurrentNodeTree = ScriptableExtensions.GetAllInstances<CurrentNodeTree>().First();
                orchestrator.Conditions = ScriptableExtensions.GetAllInstancesByLabel("condition")
                    .Cast<NodeConditionHandler>().ToArray();
            }
        }
    }
}