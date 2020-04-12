﻿using System.Collections.Generic;
using System.Linq;

namespace EnigmaDragons.NodeSystem.Editor
{
    public class VNSaver
    {
        private readonly IStorage _storage;
        private readonly IMediaType _mediaType;

        public VNSaver(IStorage storage, IMediaType mediaType)
        {
            _storage = storage;
            _mediaType = mediaType;
        }

        public void Save(NodeTreeEditorData data, string saveName)
        {
            var connections = data.Connections.Select(x => _mediaType.ConvertFrom<ConnectionData>(x)).ToList();
            data.Nodes
                .ForEach(oNode => oNode.NodeData.NextIds = data.Nodes
                    .Where(xNode => connections.Any(connection => connection.OutNodeID == oNode.NodeData.Id && connection.InNodeID == xNode.NodeData.Id))
                    .OrderBy(x => x.X)
                    .Select(x => x.NodeData.Id)
                    .ToList());
            _storage.Put(saveName, new NodeTreeData
            {
                StartIds = connections.Where(x => x.OutNodeID == _mediaType.ConvertFrom<StartNodeData>(data.StartNode).ID).Select(x => x.InNodeID).ToArray(),
                Nodes = data.Nodes.ToDictionary(x => x.NodeData.Id, x => x.NodeData)
            });
        }
    }
}