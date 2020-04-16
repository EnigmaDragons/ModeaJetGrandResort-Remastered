using UnityEngine;

public class OnGainDialogueMemory : OnMessage<GainDialogueMemory>
{
    [SerializeField] private CurrentGameState gameState;

    protected override void Execute(GainDialogueMemory msg) => gameState.UpdateState(x =>
    {
        x.ViewedItems.Add(msg.Dialogue);
        x.Memories[msg.Dialogue] = msg.Location;
    });
}