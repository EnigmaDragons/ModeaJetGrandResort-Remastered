using UnityEngine;

namespace EnigmaDragons.NodeSystem.Editor
{
    public interface INode
    {
        Rect Rect { get; }
        string ID { get; }
    }
}