    using System;
    using UnityEngine;

public class NodeElement : IElement
{
    private readonly INodeContent _content;
    private readonly NodeNameLabel _nodeNameLabel;
    private readonly TextButton _removeNodeButton;
    private readonly GUIStyle _defaultStyle;
    private readonly GUIStyle _selectedStyle;

    private Rect _rect;
    private bool _isSelected;
    private Vector2 _contentSize;

    public float Width => _rect.width;
    public float Height => _rect.height;

    public NodeElement(INodeContent content, Action removeNode)
    {
        _content = content;
        _contentSize = new Vector2(0, 0);
        _rect = new Rect(0, 0, 0, 0);
        _nodeNameLabel = new NodeNameLabel(_content.Name, _content.Width - EditorConstants.NodePadding * 2);
        _removeNodeButton = new TextButton("X", removeNode, EditorConstants.ButtonHeight);
        UpdateDimensions();
        _defaultStyle = EditorConstants.GetNodeStyle();
        _selectedStyle = EditorConstants.GetSelectedNodeStyle();
    }

    public void Draw(Vector2 position)
    {
        UpdateIsSelected(position);
        UpdateDimensions();
        GUI.Box(_rect.WithOffset(position), "", _isSelected ? _selectedStyle : _defaultStyle);
        _nodeNameLabel.Draw(new Vector2(EditorConstants.NodePadding, EditorConstants.NodePadding) + position);
        _removeNodeButton.Draw(new Vector2(_rect.width - _removeNodeButton.Width - EditorConstants.NodePadding, EditorConstants.NodePadding) + position);
        _content.Draw(new Vector2(EditorConstants.NodePadding, EditorConstants.NodePadding * 2 + _nodeNameLabel.Height) + position);
    }

    private void UpdateIsSelected(Vector2 position)
    {
        var e = Event.current;
        if (e.type == EventType.MouseDown && e.button == 0)
        {
            _isSelected = _rect.WithOffset(position).Contains(e.mousePosition);
            GUI.changed = true;
        }
    }

    private void UpdateDimensions()
    {
        if (_contentSize.x != _content.Width || _contentSize.y != _content.Height)
        {
            _rect = new Rect(0, 0, 
                _content.Width + EditorConstants.NodePadding * 2,
                _nodeNameLabel.Height + _content.Height + EditorConstants.NodePadding * 3);
            GUI.changed = true;
            _contentSize = new Vector2(_content.Width, _content.Height);
        }
    }
}
