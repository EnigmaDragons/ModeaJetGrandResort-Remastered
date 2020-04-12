using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace EnigmaDragons.NodeSystem
{
    public class NodeContent
    {
        private readonly List<IElement> _elements = new List<IElement>();
        private readonly Dictionary<string, string> _properties = new Dictionary<string, string>();
        private readonly Type _type;

        public string Name { get; }
        public float Width => _elements.Any() ? _elements.Max(x => x.Width) : 200;
        public float Height => _elements.Any() ? _elements.Sum(x => x.Height) + EditorConstants.NodePadding * _elements.Count - 1 : 0;

        public NodeContent(NodeData nodeData) : this(Type.GetType(nodeData.Type), nodeData.Properties) { }
        public NodeContent(Type type) : this(type, new Dictionary<string, string>()) { }
        public NodeContent(Type type, Dictionary<string, string> properties)
        {
            _type = type;
            Name = type.Name.WithSpaceBetweenWords();
            type.GetProperties().Where(x => x.CanWrite).ToArray().ForEach(prop =>
            {
                if (prop.PropertyType == typeof(bool))
                {
                    _properties[prop.Name] = properties.ContainsKey(prop.Name) ? properties[prop.Name] : false.ToString();
                    _elements.Add(new BoolElement(prop.Name, bool.Parse(_properties[prop.Name]), boo => _properties[prop.Name] = boo.ToString(), 200));
                }
                else if (prop.PropertyType == typeof(string))
                {
                    _properties[prop.Name] = properties.ContainsKey(prop.Name) ? properties[prop.Name] : "";
                    _elements.Add(new ElementLabel(new ExpandingTextField(_properties[prop.Name], str => _properties[prop.Name] = str), prop.Name));
                }
                else if (prop.PropertyType == typeof(int))
                {
                    _properties[prop.Name] = properties.ContainsKey(prop.Name) ? properties[prop.Name] : 0.ToString();
                    _elements.Add(new ElementLabel(new ExpandingTextField(_properties[prop.Name], num =>
                    {
                        if (!int.TryParse(num, out _))
                            return false;
                        _properties[prop.Name] = num;
                        return true;
                    }), prop.Name));
                }
                else if (prop.PropertyType == typeof(float))
                {
                    _properties[prop.Name] = properties.ContainsKey(prop.Name) ? properties[prop.Name] : 0f.ToString();
                    _elements.Add(new ElementLabel(new ExpandingTextField(_properties[prop.Name], num =>
                    {
                        if (!float.TryParse(num, out _))
                            return false;
                        _properties[prop.Name] = num;
                        return true;
                    }), prop.Name));
                }
                else if (typeof(ScriptableObject).IsAssignableFrom(prop.PropertyType))
                {
                    var items = ScriptableExtensions.GetAllInstances(prop.PropertyType);
                    _properties[prop.Name] = properties.ContainsKey(prop.Name) && items.Any(x => x.GetInstanceID().ToString() == properties[prop.Name])
                        ? items.First(x => x.GetInstanceID().ToString() == properties[prop.Name]).GetInstanceID().ToString()
                        : items.First().GetInstanceID().ToString();
                    var dictionary = new Dictionary<string, Action>();
                    items.ForEach(item => dictionary[item.name] = () => _properties[prop.Name] = item.GetInstanceID().ToString());
                    _elements.Add(new ElementLabel(new OptionsElement(dictionary, EditorUtility.InstanceIDToObject(int.Parse(_properties[prop.Name])).name, 200), prop.Name));
                }
                else if (prop.PropertyType == typeof(TextAsset))
                {
                    var items = Resources.LoadAll<TextAsset>("NodeTrees");
                    _properties[prop.Name] = properties.ContainsKey(prop.Name) && items.Any(x => x.GetInstanceID().ToString() == properties[prop.Name])
                        ? items.First(x => x.GetInstanceID().ToString() == properties[prop.Name]).GetInstanceID().ToString()
                        : items.First().GetInstanceID().ToString();
                    var dictionary = new Dictionary<string, Action>();
                    items.ForEach(item => dictionary[item.name] = () => _properties[prop.Name] = item.GetInstanceID().ToString());
                    _elements.Add(new ElementLabel(new OptionsElement(dictionary, EditorUtility.InstanceIDToObject(int.Parse(_properties[prop.Name])).name, 200), prop.Name));
                }
            });
        }

        public void Draw(Vector2 position)
        {
            for (int i = _elements.Count - 1; i >= 0; i--)
            {
                var height = _elements.Take(i).Any() ? _elements.Take(i).Sum(x => x.Height) + EditorConstants.NodePadding * i : 0;
                _elements[i].Draw(new Vector2(position.x, position.y + height));
            }
        }

        public NodeContent Duplicate() => new NodeContent(_type, _properties);
        public NodeData Save() => new NodeData { Type = _type.AssemblyQualifiedName, Properties = _properties };
    }
}