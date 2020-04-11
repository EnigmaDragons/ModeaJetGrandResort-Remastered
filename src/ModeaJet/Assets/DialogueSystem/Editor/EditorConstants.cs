using UnityEditor;
using UnityEngine;

public static class EditorConstants
{
    public const int FontSize = 12;
    public const float LineHeight = 20;
    public const float ButtonPadding = 5;
    public const float ButtonHeight = 20;
    public const float ToggleHeight = 20;
    public const float OptionsHeight = 20;
    public const float NodePadding = 10;
    public const float TextFieldWidth = 200;

    public const float ElementLabelWidth = 100;
    public const float MenuBarHeight = 20;
    public const float MenuBarButtonWidth = 50;

    public static GUIStyle GetLabelStyle()
    {
        var style = new GUIStyle("label");;
        style.fontSize = FontSize;
        return style;
    }

    public static GUIStyle GetButtonStyle()
    {
        return new GUIStyle("button");
    }

    public static GUIStyle GetNodeStyle()
    {
        return new GUIStyle
        {
            normal = { background = EditorGUIUtility.Load("builtin skins/lightskin/images/node1.png") as Texture2D },
            border = new RectOffset(30, 30, 30, 30)
        };
    }

    public static GUIStyle GetSelectedNodeStyle()
    {
        return new GUIStyle
        {
            normal = { background = EditorGUIUtility.Load("builtin skins/lightskin/images/node1 on.png") as Texture2D },
            border = new RectOffset(30, 30, 30, 30)
        };
    }

    public static GUIStyle GetScrollBoxStyle()
    {
        return new GUIStyle("box");
    }

    public static GUIStyle GetTextFieldStyle()
    {
        var textField = new GUIStyle("textField");
        textField.fontSize = 12;
        textField.wordWrap = true;
        return textField;
    }
}
