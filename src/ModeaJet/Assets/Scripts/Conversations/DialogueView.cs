using Assets.Scripts.DataStructures.Commands;
using UnityEngine;

public sealed class DialogueView : OnMessage<ShowStatement>
{
    [SerializeField] private ProgressiveTextReveal chatBox;
    [SerializeField] private Character playerCharacter;
    [SerializeField] private CharacterView playerCharacterView;
    [SerializeField] private CharacterView otherCharacterView;

    public void StartConversation(Character other)
    {
        playerCharacterView.Init(playerCharacter, Expression.Default);
        otherCharacterView.Init(other, Expression.Default);
    }
    
    protected override void Execute(ShowStatement msg)
    {
        chatBox.Display(msg.Statement);
    }
}
