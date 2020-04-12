using UnityEngine;

namespace EnigmaDragons.NodeSystem.Editor
{
    public interface IElement
    {
        float Width { get; }
        float Height { get; }

        void Draw(Vector2 position);
    }
}