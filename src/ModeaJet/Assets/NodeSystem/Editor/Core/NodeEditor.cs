using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace EnigmaDragons.NodeSystem.Editor
{
    public class NodeTreeEditor : EditorWindow
    {
        private const float _secondsPerAutoSave = 60;
        private float _secondsSinceLastSave = 0;

        private float _zoom = 1f;
        private Vector2 _vanishingPoint = new Vector2(0, 0);

        private bool _init = false;
        private NodeEditorMenuBar _menuBar;
        private StartNode _startNode;
        private List<Node> _nodes = new List<Node>();
        private List<Connection> _connections = new List<Connection>();
        private Vector2 _gridOffset;
        private ConnectionPoint _selectedInPoint;
        private ConnectionPoint _selectedOutPoint;
        private IMediaType _mediaType = new JsonMediaType();
        private GuidToTypeMap _guidToTypeMap = new GuidToTypeMap();
        private VNSaver _vnSaver;

        [MenuItem("Window/Dialogue Editor")]
        private static void OpenWindow() => GetWindow<NodeTreeEditor>(false, "Node Tree Editor", true);

        private void InitIfNeeded()
        {
            if (_init)
                return;
            _vnSaver = new VNSaver(new JsonFileStorage(() => Application.dataPath + "/Resources/NodeTrees/", ".txt"),
                _mediaType);
            _menuBar = new NodeEditorMenuBar(this, Save, Load, New, Center, ResetZoom);
            _menuBar.NodeTreeName = "Untitled";
            _startNode = new StartNode(new Vector2(this.position.width / 2, this.position.height / 3),
                OnClickConnectionPoint);
            _init = true;
            if (File.Exists(Application.dataPath + "/Resources/NodeTrees/PreviousAutoSave.vn"))
                File.Delete(Application.dataPath + "/Resources/NodeTrees/PreviousAutoSave.vn");
            if (File.Exists(Application.dataPath + "/Resources/NodeTrees/AutoSave.vn"))
                File.Move(Application.dataPath + "/Resources/NodeTrees/AutoSave.vn",
                    Application.dataPath + "/Resources/NodeTrees/PreviousAutoSave.vn");
            Save("AutoSave");
        }

        private void Save(string saveName)
        {
            var data = new NodeTreeEditorData
            {
                Name = _menuBar.NodeTreeName,
                StartNode = _startNode.Save(_mediaType),
                Nodes = _nodes.Select(x => x.Save(_mediaType)).ToList(),
                Connections = _connections.Select(x => x.Save(_mediaType)).ToList(),
                Zoom = _zoom
            };
            File.WriteAllText(Application.dataPath + "/Resources/NodeTrees/" + saveName + ".vn",
                _mediaType.ConvertTo(data));
            _vnSaver.Save(data, saveName);
        }

        private void Load()
        {
            var path = EditorUtility.OpenFilePanel("Load Dialogue File", Application.dataPath + "/Dialogues", "vn");
            if (path.Length != 0)
            {
                var nodeEditor = _mediaType.ConvertFrom<NodeTreeEditorData>(File.ReadAllText(path));
                _menuBar.NodeTreeName = nodeEditor.Name;
                _startNode = new StartNode(_mediaType, nodeEditor.StartNode, OnClickConnectionPoint);
                _nodes = nodeEditor.Nodes.Select(CreateNode).ToList();
                _connections = nodeEditor.Connections
                    .Select(x => new Connection(_mediaType, x, _nodes, _startNode, OnClickRemoveConnection)).ToList();
                _zoom = nodeEditor.Zoom > 0 ? nodeEditor.Zoom : 1;
            }
        }

        private void New()
        {
            _menuBar.NodeTreeName = "Untitled";
            _nodes = new List<Node>();
            _connections = new List<Connection>();
        }

        private void Center()
        {
            OnDrag(new Vector2(-_startNode.Rect.x, -_startNode.Rect.y));
        }

        private void ResetZoom()
        {
            _zoom = 1;
        }

        private void OnGUI()
        {
            InitIfNeeded();
            EditorGUIUtility.labelWidth = EditorConstants.ElementLabelWidth;
            Draw();
            ProcessEvents(Event.current);
            if (GUI.changed)
                Repaint();
        }

        private void Update()
        {
            _secondsSinceLastSave = Time.deltaTime;
            if (_secondsSinceLastSave >= _secondsPerAutoSave)
            {
                _secondsSinceLastSave -= _secondsPerAutoSave;
                Save("AutoSave");
            }
        }

        private void Draw()
        {
            _menuBar.Draw(new Vector2(0, 0));

            GUI.EndGroup();
            _vanishingPoint = new Vector2(0, 0);
            GUI.BeginGroup(new Rect(0, 41 / _zoom, position.width / _zoom, position.height / _zoom));

            var oldMatrix = GUI.matrix;
            var Translation = Matrix4x4.TRS(_vanishingPoint, Quaternion.identity, Vector3.one);
            var Scale = Matrix4x4.Scale(new Vector3(_zoom, _zoom, 1.0f));
            GUI.matrix = Translation * Scale * Translation.inverse;

            DrawGrid(20, 0.2f, Color.gray);
            DrawGrid(100, 0.4f, Color.gray);
            DrawGrid(500, 0.8f, Color.gray);
            DrawGrid(2500, 1.6f, Color.gray);
            DrawGrid(12500, 3.2f, Color.gray);
            DrawCurrentConnectionLine(Event.current);
            _connections.ForEach(x => x.Draw());
            _nodes.ForEach(x => x.Draw());
            _startNode.Draw();

            GUI.matrix = oldMatrix;
        }

        private void DrawGrid(float gridSpacing, float gridOpacity, Color gridColor)
        {
            int widthDivs = Mathf.CeilToInt(position.width / _zoom / gridSpacing);
            int heightDivs = Mathf.CeilToInt(position.height / _zoom / gridSpacing);
            Handles.BeginGUI();
            Handles.color = new Color(gridColor.r, gridColor.g, gridColor.b, gridOpacity);
            Vector3 newOffset = new Vector3(_gridOffset.x % gridSpacing, _gridOffset.y % gridSpacing, 0);
            for (int i = 0; i < widthDivs; i++)
                Handles.DrawLine(new Vector3(gridSpacing * i, -gridSpacing, 0) + newOffset,
                    new Vector3(gridSpacing * i, position.height / _zoom, 0f) + newOffset);
            for (int j = 0; j < heightDivs; j++)
                Handles.DrawLine(new Vector3(-gridSpacing, gridSpacing * j, 0) + newOffset,
                    new Vector3(position.width / _zoom, gridSpacing * j, 0f) + newOffset);
            Handles.color = Color.white;
            Handles.EndGUI();
        }

        private void DrawCurrentConnectionLine(Event e)
        {
            if (_selectedInPoint != null && _selectedOutPoint == null)
            {
                Handles.DrawBezier(
                    _selectedInPoint.Rect.center,
                    e.mousePosition,
                    _selectedInPoint.Rect.center + Vector2.down * 50f,
                    e.mousePosition + Vector2.up * 50f,
                    Color.white,
                    null,
                    2f
                );
                GUI.changed = true;
            }

            if (_selectedOutPoint != null && _selectedInPoint == null)
            {
                Handles.DrawBezier(
                    _selectedOutPoint.Rect.center,
                    e.mousePosition,
                    _selectedOutPoint.Rect.center + Vector2.up * 50f,
                    e.mousePosition + Vector2.down * 50f,
                    Color.white,
                    null,
                    2f
                );
                GUI.changed = true;
            }
        }

        private void ProcessEvents(Event e)
        {
            var translatedMousePosition = new Vector2(e.mousePosition.x / _zoom, e.mousePosition.y / _zoom);
            var hasSelected = _selectedInPoint != null || _selectedOutPoint != null;
            _startNode.ProcessEvents(e, translatedMousePosition);
            for (int i = _nodes.Count - 1; i >= 0; i--)
            {
                _nodes[i].ProcessEvents(e, translatedMousePosition,
                    _startNode.IsDragged || _nodes.Any(x => x != _nodes[i] && x.IsDragged));
            }

            if (e.type == EventType.MouseDown && _nodes.None(x => x.CapturedClick))
            {
                if (e.button == 1)
                    ProcessContextMenu(translatedMousePosition);
                else if (e.button == 0 && hasSelected)
                    ClearConnectionSelection();
            }
            else if (e.type == EventType.MouseDrag && e.button == 0 && _nodes.None(x => x.IsDragged) &&
                     !_startNode.IsDragged)
                OnDrag(e.delta);

            if (e.type == EventType.ScrollWheel)
            {
                var newZoom = Mathf.Max(0.1f, Mathf.Min(10, _zoom + (e.delta.y > 0 ? -0.1f : 0.1f)));
                OnDrag(new Vector2((+position.width - position.width / _zoom) / 2,
                    (+position.height - position.height / _zoom) / 2));
                OnDrag(new Vector2((-position.width + position.width / newZoom) / 2,
                    (-position.height + position.height / newZoom) / 2));
                _zoom = newZoom;
                GUI.changed = true;
            }
        }

        private void ProcessContextMenu(Vector2 mousePosition)
        {
            var genericMenu = new GenericMenu();
            _guidToTypeMap.Commands.ForEach(x =>
                genericMenu.AddItem(new GUIContent("Commands/" + x.Name.WithSpaceBetweenWords()), false,
                    () => _nodes = _nodes.With(CreateNode(new NodeContent(x), mousePosition))));
            _guidToTypeMap.Conditions.ForEach(x =>
                genericMenu.AddItem(new GUIContent("Conditions/" + x.Name.WithSpaceBetweenWords()), false,
                    () => _nodes = _nodes.With(CreateNode(new NodeContent(x), mousePosition))));
            genericMenu.ShowAsContext();
        }

        private Node CreateNode(NodeContent content, Vector2 mousePosition)
            => new Node(content, mousePosition, OnClickConnectionPoint, OnClickRemoveNode, x => _nodes = _nodes.With(x));

        private Node CreateNode(NodeEditorData data)
            => new Node(_guidToTypeMap, data, OnClickConnectionPoint, OnClickRemoveNode, x => _nodes = _nodes.With(x));

        private void OnClickConnectionPoint(ConnectionPoint point)
        {
            if (point.Type == ConnectionPointType.In)
                OnClickInPoint(point);
            else if (point.Type == ConnectionPointType.Out)
                OnClickOutPoint(point);
        }

        private void OnClickInPoint(ConnectionPoint inPoint)
        {
            _selectedInPoint = inPoint;
            if (_selectedOutPoint != null)
            {
                if (_selectedOutPoint.Node != _selectedInPoint.Node)
                    CreateConnection(_selectedInPoint, _selectedOutPoint);
                ClearConnectionSelection();
            }
        }

        private void OnClickOutPoint(ConnectionPoint outPoint)
        {
            _selectedOutPoint = outPoint;
            if (_selectedInPoint != null)
            {
                if (_selectedOutPoint.Node != _selectedInPoint.Node)
                    CreateConnection(_selectedInPoint, _selectedOutPoint);
                ClearConnectionSelection();
            }
        }

        private void CreateConnection(ConnectionPoint inPoint, ConnectionPoint outPoint) => _connections =
            _connections.With(new Connection(inPoint, outPoint, OnClickRemoveConnection));

        private void OnClickRemoveConnection(Connection connection) => _connections = _connections.Without(connection);

        private void ClearConnectionSelection()
        {
            _selectedInPoint = null;
            _selectedOutPoint = null;
        }

        private void OnClickRemoveNode(Node node)
        {
            _connections = _connections.Where(x => x.InPoint != node.InPoint && x.OutPoint != node.OutPoint).ToList();
            _nodes = _nodes.Without(node);
        }

        private void OnDrag(Vector2 delta)
        {
            _startNode.Drag(delta);
            _nodes.ForEach(x => x.Drag(delta));
            _gridOffset += delta;
            GUI.changed = true;
        }
    }

    [Serializable]
    public class NodeTreeEditorData
    {
        public string Name;
        public string StartNode;
        public List<NodeEditorData> Nodes;
        public List<string> Connections;
        public float Zoom;
    }
}