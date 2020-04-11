using System;
using System.Text.RegularExpressions;
using Assets.Scripts.Editor;
using UnityEditor;
using UnityEngine;

public class ExpandingTextField : IElement
{
    private readonly Func<string, bool> _onChange;
    private readonly GUIStyle _style;
    private readonly float _lineHeight;

    private Rect _rect;
    private string _input;

    public float Width => _rect.width;
    public float Height => _rect.height;

    public ExpandingTextField(string startingInput, Action<string> onChange) : this(startingInput, x =>
        {
            onChange(x);
            return true;
        }) {}

    public ExpandingTextField(string startingInput, Func<string, bool> onChange)
    {
        _input = startingInput;
        _onChange = onChange;
        _style = EditorConstants.GetTextFieldStyle();
        _style.wordWrap = true;
        _lineHeight = _style.CalcHeight(new GUIContent("Q"), 999);
        _rect = new Rect(0, 0, EditorConstants.TextFieldWidth, _lineHeight * Mathf.Max(1, _style.CalcLines(_input, EditorConstants.TextFieldWidth)));

    }

    public void Draw(Vector2 position)
    {
        var input = EditorGUI.TextField(_rect.WithOffset(position), _input, _style);
        input = Regex.Unescape(input);
        if (input != null && input != _input)
        {
            if (_onChange(input))
            {
                _input = input;
                _rect = new Rect(0, 0, EditorConstants.TextFieldWidth, _lineHeight * Mathf.Max(1, _style.CalcLines(_input, Width)));
            }
        }
    }
}
