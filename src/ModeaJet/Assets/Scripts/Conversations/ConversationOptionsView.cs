﻿using EnigmaDragons.NodeSystem;
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
            {
                var msg = msgOptions[i];
                buttons[i].Show(msg.Text, () => SetSelectedOption(msg.NodeTreeIds));
            }
            else if (i == numOptions)
                buttons[i].Show("Ok, Great", EndConversation);
            else
                buttons[i].gameObject.SetActive(false);
        }
        gameObject.SetActive(true);
    }

    private void SetSelectedOption(string[] nextNodeTreeIDs)
    {
        currentNodeTree.NextNodeIds = nextNodeTreeIDs;
        gameObject.SetActive(false);
        Message.Publish(new CommandFinished<ShowOptions>());
    }

    private void EndConversation()
    {
        currentNodeTree.StopNodeTree();
        gameObject.SetActive(false);
        Message.Publish(new ConversationFinished { OtherCharacter = _otherCharacter });
        Message.Publish(new DismissCurrentView());
    }
    
}
