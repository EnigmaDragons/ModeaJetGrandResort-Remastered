using System;
using UnityEditor;
using UnityEngine;

public class ConnectionPoint
{
    public Rect Rect;
    public INode Node;
    public ConnectionPointType Type;

    private Action<ConnectionPoint> _onClick;

    public ConnectionPoint(INode node, ConnectionPointType type, Action<ConnectionPoint> onClick)
    {
        Rect = new Rect(0, 0, 20f, 20f);
        Node = node;
        Type = type;
        _onClick = onClick;
    }

    public void Draw()
    {
        Rect.x = (float)(Node.Rect.x + Node.Rect.width * 0.5 - Rect.width * 0.5f);
        if (Type == ConnectionPointType.In)
            Rect.y = Node.Rect.y - Rect.height / 2;
        else
            Rect.y = Node.Rect.y + Node.Rect.height - Rect.height / 2;
        if (Handles.Button(Rect.center, Quaternion.identity, 4, 8, Handles.CircleHandleCap))
            _onClick(this);
    }
}
