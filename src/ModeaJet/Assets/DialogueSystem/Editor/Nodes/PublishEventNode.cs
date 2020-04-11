using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PublishEventNode : INodeContent
{
    private readonly IElement _eventElement;
    private readonly List<IElement> _propertyElements = new List<IElement>();

    private string _eventType;
    private Dictionary<string, object> _properties = new Dictionary<string, object>();
    private List<ScriptableNodeData> _scriptables = new List<ScriptableNodeData>();

    public string Name => NodeTypes.PublishEvent;
    public float Width => _eventElement.Width;
    public float Height => _eventElement.Height + _propertyElements.Sum(x => x.Height) + _propertyElements.Count * EditorConstants.NodePadding;

    public PublishEventNode(IMediaType mediaType, string media) : this(mediaType, mediaType.ConvertFrom<PublishEventNodeData>(media)) { }
    public PublishEventNode() : this(GameEventList.Types.First().AssemblyQualifiedName, new Dictionary<string, object>(), new List<ScriptableNodeData>()) { }
    private PublishEventNode(IMediaType mediaType, PublishEventNodeData data) : this(data.EventType, data.Properties, data.Scriptables) { }
    private PublishEventNode(string eventType, Dictionary<string, object> properties, List<ScriptableNodeData> scriptables)
    {
        _eventType = eventType;
        _properties = properties;
        _scriptables = scriptables;
        Type.GetType(_eventType).GetProperties().ForEach(prop =>
        {
            if (prop.PropertyType == typeof(bool))
            {
                var startValue = _properties.ContainsKey(prop.Name) && (bool)_properties[prop.Name];
                _propertyElements.Add(new BoolElement(prop.Name, startValue, boo => _properties[prop.Name] = boo, 200));
                _properties[prop.Name] = startValue;
            }
            else if (prop.PropertyType == typeof(string))
            {
                var startValue = _properties.ContainsKey(prop.Name) ? _properties[prop.Name].ToString() : "";
                _propertyElements.Add(new ElementLabel(new ExpandingTextField(startValue, str => _properties[prop.Name] = str), prop.Name));
                _properties[prop.Name] = startValue;
            }
            else if (prop.PropertyType == typeof(int))
            {
                var startValue = _properties.ContainsKey(prop.Name) ? _properties[prop.Name].ToString() : "0";
                _propertyElements.Add(new ElementLabel(new ExpandingTextField(startValue, num =>
                {
                    if (!int.TryParse(num, out _))
                        return false;
                    _properties[prop.Name] = int.Parse(num);
                    return true;
                }), prop.Name));
                _properties[prop.Name] = int.Parse(startValue);
            }
            else if (typeof(ScriptableObject).IsAssignableFrom(prop.PropertyType))
            {
                var items = ScriptableExtensions.GetAllInstances(prop.PropertyType);
                var startValue = _scriptables.FirstOrDefault(x => x.PropertyName == prop.Name && x.Type == prop.PropertyType.Name && items.Any(item =>item.name == x.Name))
                    ?? new ScriptableNodeData { Name = items.First().name, PropertyName = prop.Name, Type = prop.PropertyType.Name };

                _scriptables.Add(startValue);
                var i = _scriptables.Count - 1;
                var dictionary = new Dictionary<string, Action>();
                items.ForEach(x => dictionary[x.name] = () => _scriptables[i].Name = x.name);
                _propertyElements.Add(new ElementLabel(new OptionsElement(dictionary, startValue.Name, 200), prop.Name));
            }
        });

        var eventDictionary = new Dictionary<string, Action>();
        GameEventList.Types.ForEach(x =>
        {
            eventDictionary[x.AssemblyQualifiedName] = () =>
            {
                if (_eventType == x.AssemblyQualifiedName)
                    return;
                _propertyElements.Clear();
                _properties.Clear();
                _scriptables.Clear();
                _eventType = x.AssemblyQualifiedName;
                x.GetProperties().ForEach(prop =>
                {
                    if (prop.PropertyType == typeof(bool))
                    {
                        _propertyElements.Add(new BoolElement(prop.Name, false, boo => _properties[prop.Name] = boo, 200));
                        _properties[prop.Name] = false;
                    }
                    else if (prop.PropertyType == typeof(string))
                    {
                        _propertyElements.Add(new ElementLabel(new ExpandingTextField("", str => _properties[prop.Name] = str), prop.Name));
                        _properties[prop.Name] = "";
                    }
                    else if (prop.PropertyType == typeof(int))
                    {
                        _propertyElements.Add(new ElementLabel(new ExpandingTextField("", num =>
                        {
                            if (!int.TryParse(num, out _))
                                return false;
                            _properties[prop.Name] = int.Parse(num);
                            return true;
                        }), prop.Name));
                        _properties[prop.Name] = 0;
                    }
                    else if (typeof(ScriptableObject).IsAssignableFrom(prop.PropertyType))
                    {
                        var items = ScriptableExtensions.GetAllInstances(prop.PropertyType);
                        _scriptables.Add(new ScriptableNodeData { Name = items.First().name, PropertyName = prop.Name, Type = prop.PropertyType.Name });
                        var i = _scriptables.Count - 1;
                        var dictionary = new Dictionary<string, Action>();
                        items.ForEach(item => dictionary[item.name] = () => _scriptables[i].Name = item.name);
                        _propertyElements.Add(new ElementLabel(new OptionsElement(dictionary, _scriptables.Last().Name, 200), prop.Name));
                    }
                });
            };
        });
        _eventElement = new ElementLabel(new OptionsElement(eventDictionary, _eventType, 200), "Event");
    }

    public void Draw(Vector2 position)
    {
        _eventElement.Draw(position);
        for (var i = _propertyElements.Count; i > 0; i--)
        {
            var heightAdded = _propertyElements.Take(i - 1).Any() ? _propertyElements.Take(i - 1).Sum(x => x.Height) : 0;
            _propertyElements[i - 1].Draw(position + new Vector2(0, _eventElement.Height + (EditorConstants.NodePadding * i) + heightAdded));
        }
    }

    public INodeContent Duplicate() => new PublishEventNode(_eventType, _properties, _scriptables);
    public string Save(IMediaType mediaType) => mediaType.ConvertTo(new PublishEventNodeData { EventType = _eventType, Properties = _properties, Scriptables = _scriptables });
}

[Serializable]
public class PublishEventNodeData
{
    public string EventType;
    public Dictionary<string, object> Properties;
    public List<ScriptableNodeData> Scriptables;
}

[Serializable]
public class ScriptableNodeData
{
    public string Type;
    public string PropertyName;
    public string Name;
}