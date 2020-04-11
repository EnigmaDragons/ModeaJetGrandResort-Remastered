using System;
using System.Collections.Generic;
using UnityEngine;

public class ConditionNode : INodeContent
{
    private readonly IElement _variableTypeElement;
    private readonly IElement _variableNameElement;
    private readonly IElement _stringValueElement;
    private readonly IElement _intComparisonElement;
    private readonly IElement _intValueElement;
    private readonly IElement _intValue2Element;
    private readonly IElement _boolValueElement;
    private readonly IElement _addConditionButton;
    private readonly Action _addConditionNode;

    private string _name;
    private ConditionType _type;
    private string _stringValue;
    private IntComparison _intComparison;
    private int _intValue;
    private int _intValue2;
    private bool _boolValue;

    public string Name => NodeTypes.Condition;
    public float Width => _variableNameElement.Width;
    public float Height => _variableNameElement.Height + _variableTypeElement.Height + EditorConstants.NodePadding * 2 +
        (_type == ConditionType.Int ? _intComparisonElement.Height + _intValueElement.Height + EditorConstants.NodePadding : _stringValueElement.Height);

    public ConditionNode(Action addConditionNode, IMediaType mediaType, string media)
        : this(addConditionNode, mediaType.ConvertFrom<ConditionNodeData>(media)) { }

    public ConditionNode(Action addConditionNode) : this(addConditionNode, "", ConditionType.String, "", IntComparison.EqualTo, 0, 0, true) { }

    private ConditionNode(Action addConditionNode, ConditionNodeData data)
        : this(addConditionNode, data.Name, data.Type, data.StringValue, data.IntComparison, data.IntValue, data.IntValue2, data.BoolValue) { }

    private ConditionNode(Action addConditionNode, string name, ConditionType type, string stringValue, IntComparison intComparison, int intValue, int intValue2, bool boolValue)
    {
        _addConditionNode = addConditionNode;
        _name = name;
        _type = type;
        _stringValue = stringValue;
        _intComparison = intComparison;
        _intValue = intValue;
        _intValue2 = intValue2;
        _boolValue = boolValue;
        _variableNameElement = new ElementLabel(new ExpandingTextField(_name, x => _name = x), "Name");
        _variableTypeElement = new ElementLabel(new OptionsElement(new Dictionary<string, Action>
        {
            { nameof(ConditionType.String), () => _type = ConditionType.String },
            { nameof(ConditionType.Int), () => _type = ConditionType.Int },
            { nameof(ConditionType.Bool), () => _type = ConditionType.Bool },
            { nameof(ConditionType.And), () => _type = ConditionType.And },
            { nameof(ConditionType.Or), () => _type = ConditionType.Or },
        }, Enum.GetName(typeof(ConditionType), _type), 200), "Type");
        _stringValueElement = new ElementLabel(new ExpandingTextField(_stringValue, x => _stringValue = x), "Value");
        _intComparisonElement = new ElementLabel(new OptionsElement(new Dictionary<string, Action>
        {
            { nameof(IntComparison.EqualTo).WithSpacesBetweenWords(), () => _intComparison = IntComparison.EqualTo },
            { nameof(IntComparison.NotEqualTo).WithSpacesBetweenWords(), () => _intComparison = IntComparison.NotEqualTo },
            { nameof(IntComparison.GreaterThan).WithSpacesBetweenWords(), () => _intComparison = IntComparison.GreaterThan },
            { nameof(IntComparison.LessThan).WithSpacesBetweenWords(), () => _intComparison = IntComparison.LessThan },
            { nameof(IntComparison.Between).WithSpacesBetweenWords(), () => _intComparison = IntComparison.Between },
            { nameof(IntComparison.GreaterThanOrEqualTo).WithSpacesBetweenWords(), () => _intComparison = IntComparison.GreaterThanOrEqualTo },
            { nameof(IntComparison.LessThanOrEqualTo).WithSpacesBetweenWords(), () => _intComparison = IntComparison.LessThanOrEqualTo },
        }, Enum.GetName(typeof(IntComparison), _intComparison).WithSpacesBetweenWords(), 200), "Comparison");
        _intValueElement = new ElementLabel(new ExpandingTextField(_intValue.ToString(), x => int.TryParse(x, out _intValue)), "Value");
        _intValue2Element = new ElementLabel(new ExpandingTextField(_intValue2.ToString(), x => int.TryParse(x, out _intValue2)), "Value 2");
        _boolValueElement = new BoolElement("Value", boolValue, x => _boolValue = x, 200);
        _addConditionButton = new TextButton("Add Condition", addConditionNode);
    }

    public void Draw(Vector2 position)
    {
        if (_type == ConditionType.Bool)
            _boolValueElement.Draw(new Vector2(position.x, position.y + _variableNameElement.Height + _variableTypeElement.Height + EditorConstants.NodePadding * 2));
        if (_type == ConditionType.Int)
        {
            if (_intComparison == IntComparison.Between)
                _intValue2Element.Draw(new Vector2(position.x, position.y + _variableNameElement.Height + _variableTypeElement.Height + _intComparisonElement.Height + _intValueElement.Height + EditorConstants.NodePadding * 4));
            _intValueElement.Draw(new Vector2(position.x, position.y + _variableNameElement.Height + _variableTypeElement.Height + _intComparisonElement.Height + EditorConstants.NodePadding * 3));
            _intComparisonElement.Draw(new Vector2(position.x, position.y + _variableNameElement.Height + _variableTypeElement.Height + EditorConstants.NodePadding * 2));
        }
        if (_type == ConditionType.String)
            _stringValueElement.Draw(new Vector2(position.x, position.y + _variableNameElement.Height + _variableTypeElement.Height + EditorConstants.NodePadding * 2));
        if (_type != ConditionType.And && _type != ConditionType.Or)
            _variableNameElement.Draw(new Vector2(position.x, position.y + _variableTypeElement.Height + EditorConstants.NodePadding));
        if (_type == ConditionType.And || _type == ConditionType.Or)
            _addConditionButton.Draw(new Vector2(position.x, position.y + _variableTypeElement.Height + EditorConstants.NodePadding));
        _variableTypeElement.Draw(position);
    }

    public INodeContent Duplicate() => new ConditionNode(_addConditionNode, _name, _type, _stringValue, _intComparison, _intValue, _intValue2, _boolValue);

    public string Save(IMediaType mediaType) => mediaType.ConvertTo(new ConditionNodeData
    {
        Name = _name,
        Type = _type,
        StringValue = _stringValue,
        IntComparison = _intComparison,
        IntValue = _intValue,
        IntValue2 = _intValue2,
        BoolValue = _boolValue
    });
}

[Serializable]
public class ConditionNodeData
{
    public string Name;
    public ConditionType Type;
    public string StringValue;
    public IntComparison IntComparison;
    public int IntValue;
    public int IntValue2;
    public bool BoolValue;
}