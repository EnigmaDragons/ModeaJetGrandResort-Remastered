using Assets.Scripts.DataStructures.Commands;
using UnityEngine;

public sealed class DialogueView : OnMessage<ShowStatement, ChangeExpression>
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

    protected override void Execute(ChangeExpression msg)
    {
        if (msg.Character == playerCharacter)
            playerCharacterView.Init(playerCharacter, msg.Expression);
        else
            otherCharacterView.Init(msg.Character, msg.Expression);
    }
}
