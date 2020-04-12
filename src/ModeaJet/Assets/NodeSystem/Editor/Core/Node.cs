using System;
using UnityEditor;
using UnityEngine;

namespace EnigmaDragons.NodeSystem.Editor
{
    public class Node : INode
    {
        private readonly NodeContent _content;
        private readonly NodeElement _element;

        private readonly Action<ConnectionPoint> _onClickConnection;
        private readonly Action<Node> _onRemoveNode;
        private readonly Action<Node> _onDuplicateComplete;

        private Rect _rect;

        public string ID { get; }
        public Rect Rect => _rect;
        public ConnectionPoint InPoint;
        public ConnectionPoint OutPoint;
        public bool IsDragged = false;
        public bool CapturedClick = false;

        public Node(NodeContent content, Vector2 position,
            Action<ConnectionPoint> onClickConnection, Action<Node> onRemoveNode, Action<Node> onDuplicateComplete)
            : this(content, position, Guid.NewGuid().ToString(), onClickConnection, onRemoveNode, onDuplicateComplete) { }

        public Node(GuidToTypeMap map, NodeEditorData data,
            Action<ConnectionPoint> onClickConnection, Action<Node> onRemoveNode, Action<Node> onDuplicateComplete)
            : this(new NodeContent(map, data.NodeData), new Vector2(data.X, data.Y), data.NodeData.Id, onClickConnection, onRemoveNode, onDuplicateComplete) { }

        private Node(NodeContent content, Vector2 position, string id,
            Action<ConnectionPoint> onClickConnection, Action<Node> onRemoveNode, Action<Node> onDuplicateComplete)
        {
            ID = id;
            _onClickConnection = onClickConnection;
            _onRemoveNode = onRemoveNode;
            _onDuplicateComplete = onDuplicateComplete;

            InPoint = new ConnectionPoint(this, ConnectionPointType.In, onClickConnection);
            OutPoint = new ConnectionPoint(this, ConnectionPointType.Out, onClickConnection);

            _content = content;
            _element = new NodeElement(_content, () => onRemoveNode(this));
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
            _rect = new Rect(_rect.x, _rect.y, _element.Width, _element.Height);
            InPoint.Draw();
            OutPoint.Draw();
            _element.Draw(_rect.position);
        }

        public void ProcessEvents(Event e, Vector2 mousePosition, bool isDraggingAnotherElement)
        {
            CapturedClick = false;
            if (e.type == EventType.MouseDown && _rect.Contains(mousePosition) && !isDraggingAnotherElement)
            {
                CapturedClick = true;
                if (e.button == 0)
                    IsDragged = true;
                else if (e.button == 1)
                    ShowContextMenu();
            }
            else if (e.type == EventType.MouseUp && e.button == 0)
                IsDragged = false;
            else if (e.type == EventType.MouseDrag && IsDragged && !isDraggingAnotherElement)
                Drag(e.delta);
        }

        private void ShowContextMenu()
        {
            var genericMenu = new GenericMenu();
            genericMenu.AddItem(new GUIContent("Duplicate Node"), false, () => _onDuplicateComplete(new Node(_content.Duplicate(), _rect.position + new Vector2(_rect.width, 0), _onClickConnection, _onRemoveNode, _onDuplicateComplete)));
            genericMenu.ShowAsContext();
        }

        public NodeEditorData Save(IMediaType mediaType)
        {
            var nodeData = _content.Save();
            nodeData.Id = ID;
            return new NodeEditorData { X = _rect.x, Y = _rect.y, NodeData = nodeData };
        }
    }

    [Serializable]
    public class NodeEditorData
    {
        public float X;
        public float Y;
        public NodeData NodeData;
    }
}