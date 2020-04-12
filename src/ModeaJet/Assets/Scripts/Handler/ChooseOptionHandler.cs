using Assets.Scripts.DataStructures.Commands;
using EnigmaDragons.NodeSystem;
using UnityEngine;

public class ChooseOptionHandler : OnMessage<ChooseOption>
{
    [SerializeField] private CurrentNodeTree currentNodeTree;

    protected override void Execute(ChooseOption msg)
    {
        currentNodeTree.NextNodeIds = msg.NodeTreeIds;
        Message.Publish(new ProgressNodeTree());
    }
}