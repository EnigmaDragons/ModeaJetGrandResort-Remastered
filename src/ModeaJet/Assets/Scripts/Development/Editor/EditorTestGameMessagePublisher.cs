#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TestGameMessagePublisher))]
public class EditorTestGameMessagePublisher : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var s = (TestGameMessagePublisher)target;
        if(GUILayout.Button("Start Conversation")) 
            s.StartConversation();
    }
}
#endif
