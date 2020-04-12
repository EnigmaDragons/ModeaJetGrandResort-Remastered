using Assets.Scripts.DataStructures.Commands;
using UnityEngine;

public sealed class DialogueView : OnMessage<ShowStatement>
{
    [SerializeField] private ProgressiveTextReveal chatBox;
    
    protected override void Execute(ShowStatement msg)
    {
        chatBox.Display(msg.Statement);
    }
}
