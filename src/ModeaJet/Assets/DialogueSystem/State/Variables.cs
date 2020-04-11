using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class Variables : ScriptableObject
{
    [SerializeField] private SequencerDependencies dependencies;

    private DictionaryWithDefault<string, string> _stringVariables;
    private DictionaryWithDefault<string, int> _intVariables;
    private DictionaryWithDefault<string, bool> _boolVariables;

    public string GetString(string key) => _stringVariables[key];
    public int GetInt(string key) => _intVariables[key];
    public bool GetBool(string key) => _boolVariables[key];
    public void SetString(string key, string value) => _stringVariables[key] = value;
    public void SetInt(string key, int value) => _intVariables[key] = value;
    public void SetBool(string key, bool value) => _boolVariables[key] = value;

    public void AddInt(string key, int value)
    {
        if (!_intVariables.ContainsKey(key))
            _intVariables[key] = 0;
        _intVariables[key] += value;
    }

    public void Init()
    {
        _stringVariables = new DictionaryWithDefault<string, string>("");
        _intVariables = new DictionaryWithDefault<string, int>(0);
        _boolVariables = new DictionaryWithDefault<string, bool>(false);
    }

    public string Save()
    {
        return dependencies.MediaType.ConvertTo(new VariablesData
        {
            StringData = _stringVariables.Select(x => new VariablesStringData
            {
                Name = x.Key,
                Value = x.Value
            }).ToList(),
            IntData = _intVariables.Select(x => new VariablesIntData
            {
                Name = x.Key,
                Value = x.Value
            }).ToList(),
            BoolData = _boolVariables.Select(x => new VariablesBoolData
            {
                Name = x.Key,
                Value = x.Value
            }).ToList()
        });
    }

    public void Load(string variables)
    {
        Init();
        var data = dependencies.MediaType.ConvertFrom<VariablesData>(variables);
        data.StringData.ForEach(x => _stringVariables[x.Name] = x.Value);
        data.IntData.ForEach(x => _intVariables[x.Name] = x.Value);
        data.BoolData.ForEach(x => _boolVariables[x.Name] = x.Value);
    }

}

[Serializable]
public class VariablesData
{
    public List<VariablesStringData> StringData;
    public List<VariablesIntData> IntData;
    public List<VariablesBoolData> BoolData;
}

[Serializable]
public class VariablesStringData
{
    public string Name;
    public string Value;
}

[Serializable]
public class VariablesIntData
{
    public string Name;
    public int Value;
}

[Serializable]
public class VariablesBoolData
{
    public string Name;
    public bool Value;
}