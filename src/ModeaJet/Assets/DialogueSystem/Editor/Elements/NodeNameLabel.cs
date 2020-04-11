using UnityEngine;

public class NodeNameLabel : IElement
{
    private readonly string _name;
    private readonly Rect _rect;
    private readonly GUIStyle _style;

    public float Width { get; }
    public float Height => EditorConstants.LineHeight;

    public NodeNameLabel(string name, float width)
    {
        _name = name;
        Width = width;
        _rect = new Rect(0, 0, width, EditorConstants.LineHeight);
        _style = EditorConstants.GetLabelStyle();
        _style.alignment = TextAnchor.MiddleCenter;
        _style.fontStyle = FontStyle.Bold;
    }

    public void Draw(Vector2 position) => GUI.Label(_rect.WithOffset(position), _name, _style);
}
