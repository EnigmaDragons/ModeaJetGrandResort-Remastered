using UnityEngine;

namespace Assets.Scripts.Editor.Dialogue
{
    public class ErrorNode : INodeContent
    {
        public string Name { get; }
        public float Width => 200;
        public float Height => 0;

        public ErrorNode(string name)
        {
            Name = name;
        }

        public void Draw(Vector2 position) {}

        public INodeContent Duplicate() => new ErrorNode(Name);

        public string Save(IMediaType mediaType) => "";
    }
}
