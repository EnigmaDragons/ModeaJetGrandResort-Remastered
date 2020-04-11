using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class OptionsElement : IElement
{
    private readonly Rect _rect;
    private readonly Dictionary<int, Action> _options;
    private readonly string[] _optionNames;

    private int _selectedIndex = 0;

    public float Width => _rect.width;
    public float Height => _rect.height;

    public OptionsElement(Dictionary<string, Action> options, string defaultSelected, float width)
    {
        var names = new List<string>();
        _options = new Dictionary<int, Action>();
        var i = 0;
        options.ForEach(x =>
        {
            names.Add(x.Key);
            _options[i] = x.Value;
            i++;
        });
        _optionNames = names.ToArray();
        _selectedIndex = names.IndexOf(defaultSelected);
        _rect = new Rect(0, 0, width, EditorConstants.OptionsHeight);
    }

    public void Draw(Vector2 position)
    {
        var currentIndex = _selectedIndex;
        _selectedIndex = EditorGUI.Popup(_rect.WithOffset(position), _selectedIndex, _optionNames);
        if (_selectedIndex != currentIndex)
            _options[_selectedIndex]();
    }
}
