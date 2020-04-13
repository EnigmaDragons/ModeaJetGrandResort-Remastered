using UnityEngine;

public sealed class TestGameMessagePublisher : MonoBehaviour
{
    [SerializeField] private Character otherCharacter;

    public void StartConversation() => Message.Publish(new StartConversation { OtherCharacter = otherCharacter });
}
