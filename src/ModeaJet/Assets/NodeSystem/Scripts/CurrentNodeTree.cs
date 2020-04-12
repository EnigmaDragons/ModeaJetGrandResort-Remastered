using UnityEngine;

namespace EnigmaDragons.NodeSystem
{
    [CreateAssetMenu(menuName = "Node Tree/Current Node Tree")]
    public class CurrentNodeTree : ScriptableObject
    {
        public TextAsset NodeTree;
        public string CurrentNodeId;
        public string[] NextNodeIds;
        public bool IsActive;
        public bool ShouldEnd;
        public bool IsDebug;

        public void StartNodeTree(TextAsset nodeTree)
        {
            NodeTree = nodeTree;
            IsActive = true;
            Message.Publish(new NodeTreeChanged());
        }

        public void StopNodeTree()
        {
            IsActive = false;
            ShouldEnd = true;
            Message.Publish(new NodeTreeChanged());
        }
    }
}