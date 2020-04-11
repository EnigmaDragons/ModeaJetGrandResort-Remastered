using System;
using System.Collections.Generic;
using System.Linq;

public class VariableNameSupplier
{
    private readonly Func<List<NodeData>> _getNodes;
    private readonly IMediaType _mediaType;

    public VariableNameSupplier(Func<List<NodeData>> getNodes, IMediaType mediaType)
    {
        _getNodes = getNodes;
        _mediaType = mediaType;
    }

    public List<string> GetEncapsulatedStringVariables() => GetEncapsulatedVariables(ConditionType.String);
    public List<string> GetEncapsulatedIntVariables() => GetEncapsulatedVariables(ConditionType.Int);
    public List<string> GetEncapsulatedBoolVariables() => GetEncapsulatedVariables(ConditionType.Bool);
    public List<string> GetScriptableStringVariables() => ScriptableExtensions.GetAllInstances<StringVariable>().Select(x => x.name).ToList();
    public List<string> GetScriptableIntVariables() => ScriptableExtensions.GetAllInstances<IntVariable>().Select(x => x.name).ToList();
    public List<string> GetScriptableBoolVariables() => ScriptableExtensions.GetAllInstances<BoolVariable>().Select(x => x.name).ToList();

    private List<string> GetEncapsulatedVariables(ConditionType type) 
        => _getNodes().Where(x => x.Type == NodeTypes.Condition && _mediaType.ConvertFrom<ConditionNodeData>(x.Content).Type == type)
            .Select(x => _mediaType.ConvertFrom<ConditionNodeData>(x.Content).Name)
            .ToList();
}