using EnigmaDragons.NodeSystem;
using UnityEngine;

public sealed class ConversationOptionsView : MonoBehaviour
{
    [SerializeField] private ConversationOptionButton[] buttons;
    [SerializeField] private CurrentNodeTree currentNodeTree;

    private Character _otherCharacter;
    
    public void Hide() => gameObject.SetActive(false);
    
    public void Show(Character otherCharacter, Option[] msgOptions)
    {
        _otherCharacter = otherCharacter;
        var numOptions = msgOptions.Length;
        for (var i = 0; i < buttons.Length; i++)
        {
            if (i < numOptions)
                buttons[i].Show(msgOptions[i].Text, () => SetSelectedOption(msgOptions[i].NodeTreeIds));
            else if (i == numOptions)
                buttons[i].Show("Ok, Great", EndConversation);
            else
                buttons[i].gameObject.SetActive(false);
        }
    }

    private void SetSelectedOption(string[] nextNodeTreeIDs)
    {
        currentNodeTree.NextNodeIds = nextNodeTreeIDs;
        Message.Publish(new CommandFinished<ShowOptions>());
    }

    private void EndConversation()
    {
        currentNodeTree.StopNodeTree();
        Message.Publish(new ConversationFinished { OtherCharacter = _otherCharacter });
        Message.Publish(new DismissCurrentView());
    }
    
}
