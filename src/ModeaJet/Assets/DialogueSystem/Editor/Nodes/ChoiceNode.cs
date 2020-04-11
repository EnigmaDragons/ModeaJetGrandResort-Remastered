using System;
using UnityEngine;

public class ChoiceNode : INodeContent
{
    private readonly IElement _choiceElement;

    private string _choice;

    public string Name => NodeTypes.Choice;
    public float Width => _choiceElement.Width;
    public float Height => _choiceElement.Height;

    public ChoiceNode(IMediaType mediaType, string media) : this(mediaType.ConvertFrom<ChoiceNodeData>(media).Choice) { }
    public ChoiceNode() : this("") { }
    private ChoiceNode(string choice)
    {
        _choice = choice;
        _choiceElement = new ElementLabel(new ExpandingTextField(_choice, x => _choice = x), "Choice");
    }

    public void Draw(Vector2 position) => _choiceElement.Draw(position);
    public INodeContent Duplicate() => new ChoiceNode(_choice);
    public string Save(IMediaType mediaType) => mediaType.ConvertTo(new ChoiceNodeData { Choice = _choice });
}

[Serializable]
public class ChoiceNodeData
{
    public string Choice;
}