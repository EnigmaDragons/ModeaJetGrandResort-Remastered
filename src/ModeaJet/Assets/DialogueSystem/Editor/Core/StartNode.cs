using System;
using UnityEngine;

public class StartNode : INode
{
    private readonly StartNodeElement _element;

    private Rect _rect;

    public string ID { get; }
    public Rect Rect => _rect;
    public ConnectionPoint OutPoint;
    public bool IsDragged = false;
    public bool CapturedClick = false;

    public StartNode(IMediaType mediaType, string media, Action<ConnectionPoint> onClickConnection)
        : this(mediaType.ConvertFrom<StartNodeData>(media), onClickConnection) { }

    public StartNode(Vector2 position, Action<ConnectionPoint> onClickConnection)
        : this(position, Guid.NewGuid().ToString(), onClickConnection) { }

    private StartNode(StartNodeData data, Action<ConnectionPoint> onClickConnection)
        : this(new Vector2(data.X, data.Y), data.ID, onClickConnection) {}

    private StartNode(Vector2 position, string id, Action<ConnectionPoint> onClickConnection)
    {
        ID = id;
        _element = new StartNodeElement();
        OutPoint = new ConnectionPoint(this, ConnectionPointType.Out, onClickConnection);
        _rect = new Rect(position.x, position.y, _element.Width, _element.Height);
        Drag(new Vector2(0, 0));
    }

    public void Drag(Vector2 adjustment)
    {
        _rect.position += adjustment;
        GUI.changed = true;
    }

    public void Draw()
    {
        OutPoint.Draw();
        _element.Draw(_rect.position);
    }

    public void ProcessEvents(Event e, Vector2 mousePosition)
    {
        CapturedClick = false;
        if (e.type == EventType.MouseDown && _rect.Contains(mousePosition))
        {
            CapturedClick = true;
            if (e.button == 0)
                IsDragged = true;
        }
        else if (e.type == EventType.MouseUp && e.button == 0)
            IsDragged = false;
        else if (e.type == EventType.MouseDrag && IsDragged)
            Drag(e.delta);
    }

    public string Save(IMediaType mediaType) => mediaType.ConvertTo(new StartNodeData { ID = ID, X = _rect.x, Y = _rect.y });
}

[Serializable]
public class StartNodeData
{
    public string ID;
    public float X;
    public float Y;
}
