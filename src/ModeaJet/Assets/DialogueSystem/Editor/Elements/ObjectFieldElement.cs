using System;
using UnityEditor;
using UnityEngine;

public class ObjectFieldElement<T> : IElement where T : UnityEngine.Object
{ 
    private readonly Type _type;
    private readonly Action<T> _setObj;
    private readonly string _name;
    private readonly Rect _rect;

    private T _selected;

    public float Width { get; }
    public float Height { get; }

    public ObjectFieldElement(Action<T> setObj, T selected, string name, float width)
    {
        _type = typeof(T);
        _setObj = setObj;
        _selected = selected;
        _name = name;
        Width = width;
        Height = EditorConstants.ButtonHeight;
        _rect = new Rect(0, 0, width, EditorConstants.ButtonHeight);
    }

    public void Draw(Vector2 position)
    {
        _selected = (T)EditorGUI.ObjectField(_rect.WithOffset(position), _name, _selected, _type, false);
        if (_selected)
            _setObj(_selected);
    }
}
