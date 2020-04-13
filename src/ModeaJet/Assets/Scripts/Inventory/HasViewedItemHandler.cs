using System;
using EnigmaDragons.NodeSystem;
using UnityEngine;

[CreateAssetMenu(menuName = "Condition Handlers/Has Viewed Item")]
public class HasViewedItemHandler : NodeConditionHandler
{
    [SerializeField] private CurrentGameState gameState;
    public override Type CondtionType => typeof(HasViewedItem);
    public override bool IsConditionMet(object condition) => gameState.GameState.HasViewedItem(((HasViewedItem)condition).Item);
}