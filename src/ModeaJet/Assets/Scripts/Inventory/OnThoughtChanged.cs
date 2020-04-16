using UnityEngine;

public class OnThoughtChanged : OnMessage<ThoughtGained, ThoughtLost>
{
    [SerializeField] private CurrentGameState gameState;

    protected override void Execute(ThoughtGained msg) => gameState.UpdateState(x => x.Thoughts.Add(msg.Thought));
    protected override void Execute(ThoughtLost msg) => gameState.UpdateState(x => x.Thoughts.Remove(msg.Thought));
}