using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class SequenceOrchestrator : MonoBehaviour
{
    [SerializeField] private SequencerDependencies dependencies;
    [SerializeField] private List<TextAsset> sequences;
    [SerializeField] private CurrentSequence currentSequence;
    [SerializeField] private Variables currentVariables;
    [SerializeField] private CurrentChoices _currentCurrentChoices;

    protected IMediaType _mediaType;
    private SequenceData _sequence;

    protected abstract void PopulateScriptableOnEvent(object e, ScriptableData data);
    protected abstract void SetScriptableVariable(SetVariableData data);
    protected abstract bool IsCustomConditionMet(ConditionData data);

    private void OnEnable()
    {
        currentVariables.Init();
        _mediaType = dependencies.MediaType;
        Message.Subscribe<SequenceStateChanged>(Execute, this);
        Message.Subscribe<SequenceStepFinished>(Execute, this);
    }

    private void Start()
    {
        if (!string.IsNullOrWhiteSpace(currentSequence.Name))
        {
            currentSequence.IsActive = true;
            Execute(new SequenceStateChanged());
        }
    }

    private void OnDisable()
    {
        Message.Unsubscribe(this);
        Message.Unsubscribe(this);
    }

    private void Execute(SequenceStateChanged msg)
    {
        if (currentSequence.IsActive)
        {
            currentSequence.ShouldEnd = false;
            _sequence = _mediaType.ConvertFrom<SequenceData>(sequences.First(x => x.name == currentSequence.Name).text);
            ProcessStep(_sequence.Steps.First(x => x.ID == _sequence.StartID));
        }
    }

    protected void Execute(SequenceStepFinished msg)
    {
        if (!currentSequence.IsActive)
            return;
        if (currentSequence.ShouldEnd)
        {
            currentSequence.IsActive = false;
            Message.Publish(new SequenceStateChanged());
        }
        else
            ProcessStep(_sequence.Steps.First(x => x.ID == currentSequence.NextStepID));
    }

    private void ProcessStep(SequenceStepData step)
    {
        if (currentSequence.IsDebug)
            Debug.Log($"{step.Type} {step.Content}");
        currentSequence.CurrentStepID = step.ID;
        if (step.Type == NodeTypes.GoToSequence)
            GoToSequence(_mediaType.ConvertFrom<GoToSequenceData>(step.Content));
        if (step.Type == NodeTypes.PublishEvent)
            PublishEvent(_mediaType.ConvertFrom<PublishEventData>(step.Content));
        if (step.Type == NodeTypes.Choices)
            GiveChoices(_mediaType.ConvertFrom<GiveChoicesData>(step.Content));
        if (step.Type == NodeTypes.Random)
            Random(_mediaType.ConvertFrom<RandomData>(step.Content));
        if (step.Type == NodeTypes.SetVariable)
            SetVariable(_mediaType.ConvertFrom<SetVariableData>(step.Content));
        if (step.Type == NodeTypes.Switch)
            Condtional(_mediaType.ConvertFrom<ConditionalData>(step.Content));
    }

    private void GoToSequence(GoToSequenceData data)
    {
        currentSequence.Name = data.Sequence;
        _sequence = _mediaType.ConvertFrom<SequenceData>(sequences.First(x => x.name == currentSequence.Name).text);
        ProcessStep(_sequence.Steps.First(x => x.ID == _sequence.StartID));
    }

    private void PublishEvent(PublishEventData data)
    {
        SetNextID(data.NextID);
        var message = _mediaType.ConvertFrom(Type.GetType(data.EventType), _mediaType.ConvertTo(data.Properties));
        data.Scriptables.ForEach(x => PopulateScriptableOnEvent(message, x));
        Message.Publish(message);
    }

    private void GiveChoices(GiveChoicesData data)
    {
        _currentCurrentChoices.List = data.Choices
            .Where(x => !x.HasCondition || ConditionMet(x.ConditionID))
            .Select(x => new Choice { NextID = x.NextID, Text = x.Choice }).ToList();
        _currentCurrentChoices.IsShowing = true;
        Message.Publish(new ChoicesGiven());
    }

    private void Random(RandomData data)
    {
        SetNextID(data.NextIDs.Random());
        Message.Publish(new SequenceStepFinished());
    }

    private void SetVariable(SetVariableData data)
    {
        SetNextID(data.NextID);
        if (!data.Scriptable)
        {
            if (data.Type == ConditionType.String)
                currentVariables.SetString(data.VariableName, data.StringValue);
            else if (data.Type == ConditionType.Int && data.IntOperation == IntOperation.Set)
                currentVariables.SetInt(data.VariableName, data.IntValue);
            else if (data.Type == ConditionType.Int && data.IntOperation == IntOperation.Add)
                currentVariables.AddInt(data.VariableName, data.IntValue);
            else if (data.Type == ConditionType.Bool)
                currentVariables.SetBool(data.VariableName, data.BoolValue);
        }
        else
        {
            SetScriptableVariable(data);
        }
        Message.Publish(new SequenceStepFinished());
    }

    private void Condtional(ConditionalData data)
    {
        SetNextID(GetNextIDFromCondtions(data));
        Message.Publish(new SequenceStepFinished());
    }

    private string GetNextIDFromCondtions(ConditionalData data)
    {
        foreach (var conditionID in data.ConditionIDs)
        {
            var condition = _mediaType.ConvertFrom<ConditionData>(_sequence.Steps.First(x => x.ID == conditionID).Content);
            if (ConditionMet(condition))
                return condition.NextID;
        }
        return data.ElseNextID;
    }

    private bool ConditionMet(string conditionID)
    {
        return ConditionMet(_mediaType.ConvertFrom<ConditionData>(_sequence.Steps.First(x => x.ID == conditionID).Content));
    }

    private bool ConditionMet(ConditionData condition)
    {
        if (condition.Type == ConditionType.String)
            return StringConditionMet(_mediaType.ConvertFrom<StringConditionData>(condition.ConditionContent));
        if (condition.Type == ConditionType.Int)
            return IntConditionMet(_mediaType.ConvertFrom<IntConditionData>(condition.ConditionContent));
        if (condition.Type == ConditionType.Bool)
            return BoolConditionMet(_mediaType.ConvertFrom<BoolConditionData>(condition.ConditionContent));
        if (condition.Type == ConditionType.And)
            return AndCondtionMet(_mediaType.ConvertFrom<MultiConditionData>(condition.ConditionContent));
        if (condition.Type == ConditionType.Or)
            return OrConditionMet(_mediaType.ConvertFrom<MultiConditionData>(condition.ConditionContent));
        if (condition.Type == ConditionType.Custom)
            return IsCustomConditionMet(condition);
        return false;
    }

    private bool StringConditionMet(StringConditionData stringCondition)
    {
        return currentVariables.GetString(stringCondition.VariableName) == stringCondition.StringValue;
    }

    private bool IntConditionMet(IntConditionData intCondition)
    {
        var intValue = currentVariables.GetInt(intCondition.VariableName);
        if (intCondition.IntComparison == IntComparison.EqualTo)
            return intValue == intCondition.IntValue;
        if (intCondition.IntComparison == IntComparison.NotEqualTo)
            return intValue != intCondition.IntValue;
        if (intCondition.IntComparison == IntComparison.GreaterThan)
            return intValue > intCondition.IntValue;
        if (intCondition.IntComparison == IntComparison.LessThan)
            return intValue < intCondition.IntValue;
        if (intCondition.IntComparison == IntComparison.GreaterThanOrEqualTo)
            return intValue >= intCondition.IntValue;
        if (intCondition.IntComparison == IntComparison.LessThanOrEqualTo)
            return intValue <= intCondition.IntValue;
        if (intCondition.IntComparison == IntComparison.Between)
            return intValue >= intCondition.IntValue && intValue <= intCondition.IntValue2;
        return false;
    }

    private bool BoolConditionMet(BoolConditionData boolCondition)
    {
        return currentVariables.GetBool(boolCondition.VariableName) == boolCondition.BoolValue;
    }

    private bool AndCondtionMet(MultiConditionData andCondition)
    {
        return andCondition.ConditionIDs.All(ConditionMet);
    }

    private bool OrConditionMet(MultiConditionData orCondition)
    {
        return orCondition.ConditionIDs.Any(ConditionMet);
    }

    private void SetNextID(string nextID)
    {
        currentSequence.NextStepID = nextID;
        if (string.IsNullOrWhiteSpace(currentSequence.NextStepID))
            currentSequence.ShouldEnd = true;
    }
}