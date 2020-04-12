using UnityEngine;

namespace EnigmaDragons.NodeSystem
{
    public class GoToNodeTree : INodeCommand
    {
        public TextAsset NodeTree { get; set; }
    }
}
