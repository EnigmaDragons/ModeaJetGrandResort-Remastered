using System;
using UnityEngine;

public class ConditionalNode : INodeContent
{
    private readonly IElement _button;
    private readonly Action _addConditionButton;

    public string Name => NodeTypes.Switch;
    public float Width => _button.Width;
    public float Height => _button.Height;

    public ConditionalNode(Action addConditionNode)
    {
        _button = new TextButton("Add Condition", addConditionNode);
    }

    public void Draw(Vector2 position) => _button.Draw(position);
    public INodeContent Duplicate() => new ConditionalNode(_addConditionButton);
    public string Save(IMediaType mediaType) => "";
}
