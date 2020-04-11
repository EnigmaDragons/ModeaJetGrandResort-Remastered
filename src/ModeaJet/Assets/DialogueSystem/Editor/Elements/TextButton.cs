using System;
using UnityEngine;

public class TextButton : IElement
{
    private readonly string _text;
    private readonly Action _onClick;
    private readonly Rect _rect;
    private readonly GUIStyle _buttonStyle;
    private readonly GUIStyle _labelStyle;

    public float Width => _rect.width;
    public float Height => _rect.height;

    public TextButton(string text, Action onClick) 
        : this(text, onClick, EditorConstants.GetLabelStyle().CalcSize(new GUIContent(text)).x + EditorConstants.ButtonPadding * 2) {}

    public TextButton(string text, Action onClick, float width)
    {
        _text = text;
        _onClick = onClick;
        _labelStyle = EditorConstants.GetLabelStyle();
        _labelStyle.alignment = TextAnchor.MiddleCenter;
        _rect = new Rect(0, 0, width, EditorConstants.LineHeight);
        _buttonStyle = EditorConstants.GetButtonStyle();
    }

    public void Draw(Vector2 position)
    {
        if (GUI.Button(_rect.WithOffset(position), "", _buttonStyle))
            _onClick();
        GUI.Label(_rect.WithOffset(position), _text, _labelStyle);
    }
}
