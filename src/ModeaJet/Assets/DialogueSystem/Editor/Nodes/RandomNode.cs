using UnityEngine;

public class RandomNode : INodeContent
{
    public string Name => NodeTypes.Random;
    public float Width => 200;
    public float Height => 0;
    public void Draw(Vector2 position) {}
    public INodeContent Duplicate() => new RandomNode();
    public string Save(IMediaType mediaType) => "";
}