using UnityEditor;
using UnityEngine;

namespace EnigmaDragons.NodeSystem
{
    [CustomEditor(typeof(CurrentNodeTree))]
    public class CurrentNodeTreeEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Start"))
                ((CurrentNodeTree)target).StartNodeTree(((CurrentNodeTree)target).NodeTree);
        }
    }
}
