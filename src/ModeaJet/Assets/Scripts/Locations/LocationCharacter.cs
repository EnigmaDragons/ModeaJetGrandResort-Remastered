using UnityEngine;

public sealed class LocationCharacter : OnMessage<StartConversation, ConversationFinished>
{
    [SerializeField] private GameObject characterObject;
    [SerializeField] private Character character;
    
    protected override void Execute(StartConversation msg)
    {
        if (msg.OtherCharacter == character)
            characterObject.SetActive(false);
    }

    protected override void Execute(ConversationFinished msg)
    {
        if (msg.OtherCharacter == character)
            characterObject.SetActive(true);
    }
}
