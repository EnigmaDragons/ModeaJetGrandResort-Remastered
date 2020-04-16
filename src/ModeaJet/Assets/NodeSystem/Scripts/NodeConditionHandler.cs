using System;
using UnityEngine;

namespace EnigmaDragons.NodeSystem
{
    public abstract class NodeConditionHandler : ScriptableObject
    {
        public abstract Type CondtionType { get; }
        public virtual bool IsConditionMet(Func<INodeCondition, bool> isConditionMet, object condition) => IsConditionMet(condition);
        public virtual bool IsConditionMet(object condition) => true;
    }
}