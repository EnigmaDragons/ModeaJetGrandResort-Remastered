using System.Collections.Generic;
using System.Linq;

namespace EnigmaDragons.NodeSystem
{
    public class NodeTreeData
    {
        private Dictionary<string, NodeData> _nodeMap;

        public string[] StartIds;
        public NodeData[] Nodes { get; set; }

        public NodeData this[string id]
        {
            get
            {
                if (_nodeMap == null)
                    _nodeMap = Nodes.ToDictionary(x => x.Id, x => x);
                return _nodeMap[id];
            }
        }
    }
}
