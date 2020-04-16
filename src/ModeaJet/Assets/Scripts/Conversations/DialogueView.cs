using UnityEngine;
using UnityEngine.UI;

public sealed class DialogueView : OnMessage<ShowStatement, ChangeExpression, ShowOptions, ScanCharacter>
{
    [SerializeField] private ProgressiveTextReveal chatBox;
    [SerializeField] private Character playerCharacter;
    [SerializeField] private CharacterView playerCharacterView;
    [SerializeField] private CharacterView otherCharacterView;
    [SerializeField] private ConversationOptionsView optionsView;
    [SerializeField] private Button scanButton;
    [SerializeField] private ScanView scanView;

    private Character _current;
    
    private void Awake()
    {
        scanButton.onClick.AddListener(() => Message.Publish(new ScanCharacter { Character = _current }));
    }
    
    public void StartConversation(Character other)
    {
        _current = other;
        playerCharacterView.Init(playerCharacter, Expression.Default);
        otherCharacterView.Init(other, Expression.Default);
    }
    
    protected override void Execute(ShowStatement msg)
    {
        optionsView.Hide();
        chatBox.Display(msg.Statement, () => Message.Publish(new CommandFinished<ShowStatement>()));
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
        optionsView.Show(_current, msg.Options);
    }

    protected override void Execute(ScanCharacter msg)
    {
        playerCharacterView.gameObject.SetActive(false);
        scanButton.gameObject.SetActive(false);
        scanView.Show(_current, () =>
        {
            scanButton.gameObject.SetActive(true);
            playerCharacterView.gameObject.SetActive(true);
        });
    }
}
