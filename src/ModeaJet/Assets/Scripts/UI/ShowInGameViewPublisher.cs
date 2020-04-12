using UnityEngine;

public sealed class ShowInGameViewPublisher : MonoBehaviour
{
    [SerializeField] private InGameViewId view;
    
    public void Execute() => Message.Publish(new ShowInGameView { View = view });
}
