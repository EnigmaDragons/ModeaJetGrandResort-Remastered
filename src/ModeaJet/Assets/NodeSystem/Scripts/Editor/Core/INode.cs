using UnityEngine;

namespace EnigmaDragons.NodeSystem
{
    public interface INode
    {
        Rect Rect { get; }
        string ID { get; }
    }
}