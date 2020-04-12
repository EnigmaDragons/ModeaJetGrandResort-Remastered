using System.Collections.Generic;

namespace EnigmaDragons.NodeSystem
{
    public class NodeData
    {
        public string Id { get; set; }
        public List<string> NextIds { get; set; }
        public string Type { get; set; }
        public Dictionary<string, string> Properties { get; set; }
    }
}
