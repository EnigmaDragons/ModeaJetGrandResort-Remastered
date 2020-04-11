using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SetVariableNode : INodeContent
{
    private readonly VariableNameSupplier _variableNameSupplier;
    private readonly IElement _variableTypeElement;
    private readonly IElement _encapsulatedElement;
    private readonly IElement _stringValueElement;
    private readonly IElement _intOperationElement;
    private readonly IElement _intValueElement;
    private readonly IElement _boolValueElement;

    private IElement _variableNameElement;

    private string _name;
    private bool _scriptable;
    private ConditionType _type;
    private string _stringValue;
    private IntOperation _intOperation;
    private int _intValue;
    private bool _boolValue;

    public string Name => NodeTypes.SetVariable;
    public float Width => _variableNameElement.Width;
    public float Height => _variableNameElement.Height + _variableTypeElement.Height + _encapsulatedElement.Height + EditorConstants.NodePadding * 3 +
        (_type == ConditionType.Int ? _intOperationElement.Height + _intValueElement.Height + EditorConstants.NodePadding : _stringValueElement.Height);

    public SetVariableNode(VariableNameSupplier variableNameSupplier, IMediaType mediaType, string media) 
        : this(variableNameSupplier, mediaType.ConvertFrom<SetVariableNodeData>(media)) {}
    public SetVariableNode(VariableNameSupplier variableNameSupplier) : this(variableNameSupplier, "", false, ConditionType.String, "", IntOperation.Set, 0, true) {}
    private SetVariableNode(VariableNameSupplier variableNameSupplier, SetVariableNodeData data) 
        : this(variableNameSupplier, data.Name, data.Scriptable, data.Type, data.StringValue, data.IntOperation, data.IntValue, data.BoolValue) {}

    private SetVariableNode(VariableNameSupplier variableNameSupplier, string name, bool encapsulated, ConditionType type, string stringValue, IntOperation intOperation, int intValue, bool boolValue)
    {
        _variableNameSupplier = variableNameSupplier;
        _name = name;
        _scriptable = encapsulated;
        _type = type;
        _stringValue = stringValue;
        _intOperation = intOperation;
        _intValue = intValue;
        _boolValue = boolValue;
        RegenerateNameElement();
        _encapsulatedElement = new BoolElement("Scriptable", encapsulated, x => { _scriptable = x; RegenerateNameElement(); }, 200);
        _variableTypeElement = new ElementLabel(new OptionsElement(new Dictionary<string, Action>
        {
            { nameof(ConditionType.String), () => { _type = ConditionType.String; RegenerateNameElement(); } },
            { nameof(ConditionType.Int), () => { _type = ConditionType.Int; RegenerateNameElement(); } },
            { nameof(ConditionType.Bool), () => { _type = ConditionType.Bool; RegenerateNameElement(); } }
        }, Enum.GetName(typeof(ConditionType), _type), 200), "Type");
        _stringValueElement = new ElementLabel(new ExpandingTextField(_stringValue, x => _stringValue = x), "Value");
        _intOperationElement = new ElementLabel(new OptionsElement(new Dictionary<string, Action>
        {
            { nameof(IntOperation.Set), () => _intOperation = IntOperation.Set },
            { nameof(IntOperation.Add), () => _intOperation = IntOperation.Add }
        }, Enum.GetName(typeof(IntOperation), _intOperation), 200), "Operation");
        _intValueElement = new ElementLabel(new ExpandingTextField(_intValue.ToString(), x => int.TryParse(x, out _intValue)), "Value");
        _boolValueElement = new BoolElement("Value", boolValue, x => _boolValue = x, 200);
    }

    public void Draw(Vector2 position)
    {
        if (_type == ConditionType.Bool)
            _boolValueElement.Draw(new Vector2(position.x, position.y + _variableNameElement.Height + _variableTypeElement.Height + _encapsulatedElement.Height + EditorConstants.NodePadding * 3));
        if (_type == ConditionType.Int)
        {
            _intOperationElement.Draw(new Vector2(position.x, position.y + _variableNameElement.Height + _variableTypeElement.Height + _encapsulatedElement.Height + EditorConstants.NodePadding * 3));
            _intValueElement.Draw(new Vector2(position.x, position.y + _variableNameElement.Height + _variableTypeElement.Height + _encapsulatedElement.Height + _intOperationElement.Height + EditorConstants.NodePadding * 4));
        }
        if (_type == ConditionType.String)
            _stringValueElement.Draw(new Vector2(position.x, position.y + _variableNameElement.Height + _variableTypeElement.Height + _encapsulatedElement.Height + EditorConstants.NodePadding * 3));
        _variableNameElement.Draw(new Vector2(position.x, position.y + _variableTypeElement.Height + _encapsulatedElement.Height + EditorConstants.NodePadding * 2));
        _encapsulatedElement.Draw(new Vector2(position.x, position.y + _variableTypeElement.Height + EditorConstants.NodePadding));
        _variableTypeElement.Draw(position);
    }

    public INodeContent Duplicate() => new SetVariableNode(_variableNameSupplier, _name, _scriptable, _type, _stringValue, _intOperation, _intValue, _boolValue);

    public string Save(IMediaType mediaType) => mediaType.ConvertTo(new SetVariableNodeData
    {
        Name = _name,
        Scriptable = _scriptable,
        Type = _type,
        StringValue = _stringValue,
        IntOperation = _intOperation,
        IntValue = _intValue,
        BoolValue = _boolValue
    });

    private void RegenerateNameElement()
    {
        if (!_scriptable)
            _variableNameElement = new ElementLabel(new ExpandingTextField(_name, x => _name = x), "Variable");
        else if (_scriptable && _type == ConditionType.String)
            RegenerateNameElement(_variableNameSupplier.GetScriptableStringVariables());
        else if (_scriptable && _type == ConditionType.Int)
            RegenerateNameElement(_variableNameSupplier.GetScriptableIntVariables());
        else if (_scriptable && _type == ConditionType.Bool)
            RegenerateNameElement(_variableNameSupplier.GetScriptableBoolVariables());
    }

    private void RegenerateNameElement(List<string> variables)
    {
        var options = new Dictionary<string, Action>();
        _variableNameSupplier.GetEncapsulatedIntVariables().ForEach(x => options[x] = () => _name = x);
        _variableNameElement = new ElementLabel(new OptionsElement(options, options.Any(x => x.Key == _name) ? options.First(x => x.Key == _name).Key : options.First().Key, 200), "Variable");
    }
}

[Serializable]
public class SetVariableNodeData
{
    public string Name;
    public bool Scriptable;
    public ConditionType Type;
    public string StringValue;
    public IntOperation IntOperation;
    public int IntValue;
    public bool BoolValue;
}
