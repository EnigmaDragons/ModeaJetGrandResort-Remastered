using UnityEngine;

public class ElementLabel : IElement
{
    private readonly IElement _element;
    private readonly string _elementName;
    private readonly Rect _labelRect;
    private readonly GUIStyle _labelStyle;

    public float Width => _labelRect.width + _element.Width;
    public float Height => Mathf.Max(_labelRect.height, _element.Height);

    public ElementLabel(IElement innerElement, string name)
    {
        _element = innerElement;
        _elementName = name + ":";
        _labelRect = new Rect(0, 0, EditorConstants.ElementLabelWidth, EditorConstants.LineHeight);
        _labelStyle = EditorConstants.GetLabelStyle();
        _labelStyle.alignment = TextAnchor.MiddleLeft;
    }

    public void Draw(Vector2 position)
    {
        GUI.Label(_labelRect.WithOffset(position), _elementName, _labelStyle);
        _element.Draw(position + new Vector2(EditorConstants.ElementLabelWidth, 0));
    }
}
