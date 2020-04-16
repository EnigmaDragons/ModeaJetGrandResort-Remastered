using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
                if (prop.Name == "NodeTreeIds")
                    _properties[prop.Name] = "[]";
                if (prop.PropertyType == typeof(bool))
                    AddBoolElement(prop, properties);
                else if (prop.PropertyType == typeof(string))
                    AddStringElement(prop, properties);
                else if (prop.PropertyType == typeof(int))
                    AddIntElement(prop, properties);
                else if (prop.PropertyType == typeof(float))
                    AddFloatElement(prop, properties);
                else if (typeof(ScriptableObject).IsAssignableFrom(prop.PropertyType))
                    AddScriptableElement(prop, properties);
                else if (prop.PropertyType == typeof(TextAsset))
                    AddTextAssetElement(prop, properties);
                else if (prop.PropertyType.IsEnum)
                    AddEnumElement(prop, properties);
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

        private void AddBoolElement(PropertyInfo prop, Dictionary<string, string> properties)
        {
            _properties[prop.Name] = properties.ContainsKey(prop.Name) ? properties[prop.Name] : false.ToString();
            _elements.Add(new BoolElement(prop.Name.WithSpaceBetweenWords(), bool.Parse(_properties[prop.Name]), boo => _properties[prop.Name] = boo.ToString(), 200));
        }

        private void AddStringElement(PropertyInfo prop, Dictionary<string, string> properties)
        {
            _properties[prop.Name] = properties.ContainsKey(prop.Name) ? properties[prop.Name] : "";
            _elements.Add(new ElementLabel(new ExpandingTextField(_properties[prop.Name], str => _properties[prop.Name] = str), prop.Name.WithSpaceBetweenWords()));
        }

        private void AddIntElement(PropertyInfo prop, Dictionary<string, string> properties)
        {
            _properties[prop.Name] = properties.ContainsKey(prop.Name) ? properties[prop.Name] : 0.ToString();
            _elements.Add(new ElementLabel(new ExpandingTextField(_properties[prop.Name], num =>
            {
                if (!int.TryParse(num, out _))
                    return false;
                _properties[prop.Name] = num;
                return true;
            }), prop.Name.WithSpaceBetweenWords()));
        }

        private void AddFloatElement(PropertyInfo prop, Dictionary<string, string> properties)
        {
            _properties[prop.Name] = properties.ContainsKey(prop.Name) ? properties[prop.Name] : 0f.ToString();
            _elements.Add(new ElementLabel(new ExpandingTextField(_properties[prop.Name], num =>
            {
                if (!float.TryParse(num, out _))
                    return false;
                _properties[prop.Name] = num;
                return true;
            }), prop.Name.WithSpaceBetweenWords()));
        }

        private void AddScriptableElement(PropertyInfo prop, Dictionary<string, string> properties)
        {
            var items = ScriptableExtensions.GetAllInstances(prop.PropertyType);
            _properties[prop.Name] = properties.ContainsKey(prop.Name) && items.Any(x => x.name == properties[prop.Name])
                ? items.First(x => x.name == properties[prop.Name]).name
                : items.First().name;
            var dictionary = new Dictionary<string, Action>();
            items.ForEach(item => dictionary[item.name] = () => _properties[prop.Name] = item.name);
            _elements.Add(new ElementLabel(new OptionsElement(dictionary, _properties[prop.Name], 200), prop.Name.WithSpaceBetweenWords()));
        }

        private void AddTextAssetElement(PropertyInfo prop, Dictionary<string, string> properties)
        {
            var items = Resources.LoadAll<TextAsset>("NodeTrees");
            _properties[prop.Name] = properties.ContainsKey(prop.Name) && items.Any(x => x.name == properties[prop.Name])
                ? items.First(x => x.name == properties[prop.Name]).name
                : items.First().name;
            var dictionary = new Dictionary<string, Action>();
            items.ForEach(item => dictionary[item.name] = () => _properties[prop.Name] = item.name);
            _elements.Add(new ElementLabel(new OptionsElement(dictionary, _properties[prop.Name], 200), prop.Name.WithSpaceBetweenWords()));
        }

        private void AddEnumElement(PropertyInfo prop, Dictionary<string, string> properties)
        {
            _properties[prop.Name] = properties.ContainsKey(prop.Name) ? properties[prop.Name] : 0.ToString();
            var valueDictionary = new Dictionary<string, int>();
            var optionsDictionary = new Dictionary<string, Action>();
            var enums = Enum.GetValues(prop.PropertyType);
            foreach (var enumValue in enums)
            {
                valueDictionary[enumValue.ToString()] = (int)enumValue;
                optionsDictionary[enumValue.ToString()] = () => _properties[prop.Name] = ((int)enumValue).ToString();
            }

            var selectedEnum = valueDictionary.Any(x => x.Value.ToString() == _properties[prop.Name])
                ? valueDictionary.First(x => x.Value.ToString() == _properties[prop.Name]).Key
                : optionsDictionary.First().Key;
            _elements.Add(new ElementLabel(new OptionsElement(optionsDictionary, selectedEnum, 200), prop.Name.WithSpaceBetweenWords()));
        }
    }
}