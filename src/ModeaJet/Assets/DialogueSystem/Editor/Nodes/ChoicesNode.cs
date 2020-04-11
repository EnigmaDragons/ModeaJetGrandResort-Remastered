using System;
using UnityEngine;

public class ChoicesNode : INodeContent
{
    private readonly IElement _addChoiceButton;
    private readonly IElement _addConditionalChoiceButton;
    private readonly Action _addChoiceNode;
    private readonly Action _addConditionalChoiceNode;

    public string Name => NodeTypes.Choices;
    public float Width => _addChoiceButton.Width + 10 + _addConditionalChoiceButton.Width;
    public float Height => _addChoiceButton.Height;

    public ChoicesNode(Action addChoiceNode, Action addConditionalChoiceNode)
    {
        _addChoiceNode = addChoiceNode;
        _addConditionalChoiceNode = addConditionalChoiceNode;
        _addChoiceButton = new TextButton("Add Choice", addChoiceNode);   
        _addConditionalChoiceButton = new TextButton("Add Conditional Choice", addConditionalChoiceNode);
    }

    public void Draw(Vector2 position)
    {
        _addChoiceButton.Draw(position);
        _addConditionalChoiceButton.Draw(position + new Vector2(_addChoiceButton.Width + 10, 0));
    } 

    public INodeContent Duplicate() => new ChoicesNode(_addChoiceNode, _addConditionalChoiceNode);
    public string Save(IMediaType mediaType) => "";
}
