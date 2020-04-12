using System;
using System.Collections.Generic;
using System.Linq;

namespace EnigmaDragons.NodeSystem
{
    public class GuidToTypeMap
    {
        private static Dictionary<string, Type> _map;
        private static List<Type> _commands;
        private static List<Type> _conditions;

        private void InitIfNeeded()
        {
            if (_map == null)
            {
                _map = new Dictionary<string, Type>(); 
                AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).Distinct().ForEach(x => _map[x.GUID.ToString()] = x);
                _commands = new List<Type>();
                _conditions = new List<Type>();
                var nodeCommandType = typeof(INodeCommand);
                var nodeConditionType = typeof(INodeCondition);
                foreach (var type in _map.Values)
                {
                    if (nodeCommandType.IsAssignableFrom(type))
                        _commands.Add(type);
                    if (nodeConditionType.IsAssignableFrom(type))
                        _conditions.Add(type);
                }
            }
        }

        public Type this[string guid]
        {
            get
            {
                InitIfNeeded();
                return _map[guid];
            }
        }

        public List<Type> Commands
        {
            get
            {
                InitIfNeeded();
                return _commands;
            }
        }

        public List<Type> Conditions
        {
            get
            {
                InitIfNeeded();
                return _conditions;
            }
        }
    }
}
