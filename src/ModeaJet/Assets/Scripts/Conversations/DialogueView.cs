using UnityEngine;

public sealed class DialogueView : OnMessage<ShowStatement, ChangeExpression, ShowOptions>
{
    [SerializeField] private ProgressiveTextReveal chatBox;
    [SerializeField] private Character playerCharacter;
    [SerializeField] private CharacterView playerCharacterView;
    [SerializeField] private CharacterView otherCharacterView;
    [SerializeField] private ConversationOptionsView optionsView;

    public void StartConversation(Character other)
    {
        playerCharacterView.Init(playerCharacter, Expression.Default);
        otherCharacterView.Init(other, Expression.Default);
    }
    
    protected override void Execute(ShowStatement msg)
    {
        optionsView.Hide();
        chatBox.Display(msg.Statement);
    }

    protected override void Execute(ChangeExpression msg)
    {
        if (msg.Character == playerCharacter)
            playerCharacterView.Init(playerCharacter, msg.Expression);
        else
            otherCharacterView.Init(msg.Character, msg.Expression);
    }

    protected override void Execute(ShowOptions msg)
    {
        chatBox.Hide();
        optionsView.Show(msg.Options);
    }
}
