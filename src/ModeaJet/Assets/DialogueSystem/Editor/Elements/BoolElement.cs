using System;
using UnityEditor;
using UnityEngine;

public class BoolElement : IElement
{
    private readonly string _name;
    private readonly Action<bool> _onChange;
    private readonly Rect _rect;

    private bool _value;

    public float Width { get; }
    public float Height { get; }

    public BoolElement(string name, bool initialValue, Action<bool> onChange, int width)
    {
        _name = name;
        _onChange = onChange;
        _value = initialValue;
        Width = width;
        Height = EditorConstants.ToggleHeight;
        _rect = new Rect(0, 0, Width, Height);
    }

    public void Draw(Vector2 position)
    {
        var value = EditorGUI.Toggle(_rect.WithOffset(position), _name, _value);
        if (_value != value)
            _onChange(value);
        _value = value;
    }
}
