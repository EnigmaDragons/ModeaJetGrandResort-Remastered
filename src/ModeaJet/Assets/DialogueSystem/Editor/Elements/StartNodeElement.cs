using UnityEngine;

public class StartNodeElement : IElement
{
    private readonly NodeNameLabel _nodeNameLabel;
    private readonly GUIStyle _defaultStyle;
    private readonly GUIStyle _selectedStyle;

    private Rect _rect;
    private bool _isSelected;

    public float Width => _rect.width;
    public float Height => _rect.height;

    public StartNodeElement()
    {
        _nodeNameLabel = new NodeNameLabel("Start From Here", 140);
        _rect = _rect = new Rect(0, 0, _nodeNameLabel.Width + EditorConstants.NodePadding * 2, _nodeNameLabel.Height + EditorConstants.NodePadding * 3);
        _defaultStyle = EditorConstants.GetNodeStyle();
        _selectedStyle = EditorConstants.GetSelectedNodeStyle();
    }

    public void Draw(Vector2 position)
    {
        UpdateIsSelected(position);
        GUI.Box(_rect.WithOffset(position), "", _isSelected ? _selectedStyle : _defaultStyle);
        _nodeNameLabel.Draw(new Vector2(EditorConstants.NodePadding, EditorConstants.NodePadding) + position);
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
}
