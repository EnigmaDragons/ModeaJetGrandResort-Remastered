using UnityEngine;

public interface IElement
{
    float Width { get; }
    float Height { get; }

    void Draw(Vector2 position);
}
