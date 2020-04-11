using System;
using UnityEditor;
using UnityEngine;

public class NodeEditorMenuBar : IElement
{
    private readonly EditorWindow _window;
    private readonly Action<string> _onSave;
    private readonly Action _onLoad;
    private readonly Action _onNew;
    private readonly Action _onCenter;
    private readonly Action _onResetZoom;

    private Rect _rect;

    public string DialogueName;
    public float Width => _rect.width;
    public float Height => _rect.height;

    public NodeEditorMenuBar(EditorWindow window, Action<string> onSave, Action onLoad, Action onNew, Action onCenter, Action onResetZoom)
    {
        _window = window;
        _onSave = onSave;
        _onLoad = onLoad;
        _onNew = onNew;
        _onCenter = onCenter;
        _onResetZoom = onResetZoom;
    }

    public void Draw(Vector2 position)
    {
        _rect = new Rect(0, 0, _window.position.width, EditorConstants.MenuBarHeight);

        GUILayout.BeginArea(_rect.WithOffset(position), EditorStyles.toolbar);
        GUILayout.BeginHorizontal();

        if (GUILayout.Button(new GUIContent("Save"), EditorStyles.toolbarButton, GUILayout.Width(EditorConstants.MenuBarButtonWidth)))
            _onSave(DialogueName);
        GUILayout.Space(5);
        if (GUILayout.Button(new GUIContent("Load"), EditorStyles.toolbarButton, GUILayout.Width(EditorConstants.MenuBarButtonWidth)))
            _onLoad();
        GUILayout.Space(5);
        if (GUILayout.Button(new GUIContent("New"), EditorStyles.toolbarButton, GUILayout.Width(EditorConstants.MenuBarButtonWidth)))
            _onNew();
        GUILayout.Space(5);
        if (GUILayout.Button(new GUIContent("Center"), EditorStyles.toolbarButton, GUILayout.Width(EditorConstants.MenuBarButtonWidth)))
            _onCenter();
        if (GUILayout.Button(new GUIContent("Reset Zoom"), EditorStyles.toolbarButton, GUILayout.Width(EditorConstants.MenuBarButtonWidth)))
            _onResetZoom();
        DialogueName = GUILayout.TextField(DialogueName, EditorStyles.textField);

        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }
}
