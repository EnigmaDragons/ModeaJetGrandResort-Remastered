using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GoToDialogueNode : INodeContent
{
    private readonly List<string> _dialogues;
    private readonly IElement _element;

    private string _dialogue;

    public string Name => NodeTypes.GoToSequence;
    public float Width => _element.Width;
    public float Height => _element.Height;

    public GoToDialogueNode(List<string> dialogues, IMediaType mediaType, string media) : this(dialogues, mediaType.ConvertFrom<GoToDialogueNodeData>(media).Dialogue) { }
    public GoToDialogueNode(List<string> dialogues) : this(dialogues, dialogues.First()) { }
    private GoToDialogueNode(List<string> dialogues, string dialogue)
    {
        _dialogues = dialogues;
        var dictionary = new Dictionary<string, Action>();
        dialogues.ForEach(x => dictionary[x] = () => _dialogue = x);
        _dialogue = dialogue;
        _element = new ElementLabel(new OptionsElement(dictionary, _dialogue, 200), "Scene");
    }

    public void Draw(Vector2 position) => _element.Draw(position);
    public INodeContent Duplicate() => new GoToDialogueNode(_dialogues, _dialogue);
    public string Save(IMediaType mediaType) => mediaType.ConvertTo(new GoToDialogueNodeData { Dialogue = _dialogue });
}

[Serializable]
public class GoToDialogueNodeData
{
    public string Dialogue;
}
