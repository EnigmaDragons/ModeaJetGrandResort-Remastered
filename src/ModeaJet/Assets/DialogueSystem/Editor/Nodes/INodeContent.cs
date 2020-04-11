using UnityEngine;

public interface INodeContent
{
    string Name { get; }
    float Width { get; }
    float Height { get; }
    void Draw(Vector2 position);
    INodeContent Duplicate();
    string Save(IMediaType mediaType);
}
