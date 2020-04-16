using System;
using EnigmaDragons.NodeSystem;
using UnityEngine;

[CreateAssetMenu(menuName = "Condition Handlers/Does Remember Dialogue")]
public class DoesRememberDialogueHandler : NodeConditionHandler
{
    [SerializeField] private CurrentGameState gameState;

    public override Type CondtionType => typeof(DoesRememberDialogue);

    public new bool IsConditionMet(object condition)
    {
        var doesRemember = (DoesRememberDialogue) condition;
        return gameState.GameState.HasViewedItem(doesRemember.Dialogue) != doesRemember.Inverse;
    }
}