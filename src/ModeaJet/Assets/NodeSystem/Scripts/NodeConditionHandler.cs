using System;
using UnityEngine;

namespace EnigmaDragons.NodeSystem
{
    public abstract class NodeConditionHandler : ScriptableObject
    {
        public abstract Type CondtionType { get; }
        public abstract bool IsConditionMet(object condition);
    }
}