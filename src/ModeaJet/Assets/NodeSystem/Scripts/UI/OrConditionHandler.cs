using System;
using System.Linq;

namespace EnigmaDragons.NodeSystem
{
    public class OrConditionHandler : NodeConditionHandler
    {
        public override Type CondtionType => typeof(OrCondition);

        public new bool IsConditionMet(Func<INodeCondition, bool> isConditionMet, object condition) 
            => ((OrCondition) condition).Conditions.Any(isConditionMet);
    }
}
