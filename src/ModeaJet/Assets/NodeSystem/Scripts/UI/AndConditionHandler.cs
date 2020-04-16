using System;
using System.Linq;
using UnityEngine;

namespace EnigmaDragons.NodeSystem
{
    [CreateAssetMenu(menuName = "Condition Handlers/And")]
    public class AndConditionHandler : NodeConditionHandler
    {
        public override Type CondtionType => typeof(AndCondition);

        public override bool IsConditionMet(Func<INodeCondition, bool> isConditionMet, object condition)
            => ((OrCondition)condition).Conditions.All(isConditionMet);
    }
}