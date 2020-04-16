using UnityEngine;

public class OnViewItem : OnMessage<ViewItem>
{
    [SerializeField] private CurrentGameState gameState;

    protected override void Execute(ViewItem msg) => gameState.UpdateState(x => x.ViewedItems.Add(msg.Item));
}