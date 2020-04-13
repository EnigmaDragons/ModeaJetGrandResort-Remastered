using System;
using EnigmaDragons.NodeSystem;
using UnityEngine;

[CreateAssetMenu(menuName = "Condition Handlers/Does Remember Dialogue")]
public class DoesRememberDialogueHandler : NodeConditionHandler
{
    [SerializeField] private CurrentGameState gameState;
    public override Type CondtionType => typeof(DoesRememberDialogue);
    public override bool IsConditionMet(object condition) => gameState.GameState.HasViewedItem(((DoesRememberDialogue)condition).Dialogue);
}