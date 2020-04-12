using UnityEngine;
using UnityEngine.UI;

public sealed class ConversationImageTint : MonoBehaviour
{
    [SerializeField] private bool forPlayerCharacter;
    [SerializeField] private ColorReference tint;
    [SerializeField] private Image image;

    public void Apply(Character speakingCharacter)
    {
        // TODO: Implement
    }
}
