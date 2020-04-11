using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
 
public class Connection
{
    private Action<Connection> _onClick;

    public ConnectionPoint InPoint;
    public ConnectionPoint OutPoint;

    public Connection(IMediaType mediaType, string media, List<Node> nodes, StartNode startNode, Action<Connection> onClick) 
        : this(mediaType.ConvertFrom<ConnectionData>(media), nodes, startNode, onClick) {}

    private Connection(ConnectionData data, List<Node> nodes, StartNode startNode, Action<Connection> onClick) : this(
        nodes.First(x => x.ID == data.InNodeID).InPoint,
        startNode.ID == data.OutNodeID ? startNode.OutPoint : nodes.First(x => x.ID == data.OutNodeID).OutPoint,
        onClick) {}

    public Connection(ConnectionPoint inPoint, ConnectionPoint outPoint, Action<Connection> onClick)
    {
        InPoint = inPoint;
        OutPoint = outPoint;
        _onClick = onClick;
    }

    public void Draw()
    {
        Handles.DrawBezier(
            InPoint.Rect.center,
            OutPoint.Rect.center,
            InPoint.Rect.center + Vector2.down * 50f,
            OutPoint.Rect.center + Vector2.up * 50f,
            Color.white,
            null,
            2f);
        if (Handles.Button((InPoint.Rect.center + OutPoint.Rect.center) * 0.5f, Quaternion.identity, 4, 8, Handles.CircleHandleCap))
            _onClick(this);
    }

    public string Save(IMediaType mediaType) => mediaType.ConvertTo(new ConnectionData { InNodeID = InPoint.Node.ID, OutNodeID = OutPoint.Node.ID });
}

[Serializable]
public class ConnectionData
{
    public string InNodeID;
    public string OutNodeID;
}
