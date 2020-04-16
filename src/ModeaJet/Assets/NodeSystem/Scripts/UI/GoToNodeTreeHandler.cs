using UnityEngine;

namespace EnigmaDragons.NodeSystem
{
    public class GoToNodeTreeHandler : OnMessage<GoToNodeTree>
    {
        [SerializeField] private CurrentNodeTree currentNodeTree;
        protected override void Execute(GoToNodeTree msg) => currentNodeTree.StartNodeTree(msg.NodeTree);
    }
}
