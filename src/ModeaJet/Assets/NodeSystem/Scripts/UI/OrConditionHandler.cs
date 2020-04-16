using System;
using System.Linq;
using UnityEngine;

namespace EnigmaDragons.NodeSystem
{
    [CreateAssetMenu(menuName = "Condition Handlers/Or")]
    public class OrConditionHandler : NodeConditionHandler
    {
        public override Type CondtionType => typeof(OrCondition);

        public override bool IsConditionMet(Func<INodeCondition, bool> isConditionMet, object condition) 
            => ((OrCondition) condition).Conditions.Any(isConditionMet);
    }
}
