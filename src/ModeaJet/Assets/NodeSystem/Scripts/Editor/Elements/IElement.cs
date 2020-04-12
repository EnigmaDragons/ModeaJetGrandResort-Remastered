using UnityEngine;

namespace EnigmaDragons.NodeSystem
{
    public interface IElement
    {
        float Width { get; }
        float Height { get; }

        void Draw(Vector2 position);
    }
}