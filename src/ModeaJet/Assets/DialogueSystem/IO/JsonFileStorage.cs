using System;
using System.IO;
using UnityEngine;

public class JsonFileStorage : IStorage
{
    private readonly Func<string> _getDataFolderPath;
    private readonly string _extension;
    private string _dataFolderPath = "";

    public JsonFileStorage(Func<string> getDataFolderPath, string extension)
    {
        _getDataFolderPath = getDataFolderPath;
        _extension = extension;
    }

    public bool Exists(string key) => File.Exists(GetSavePath(key));
    public T Get<T>(string key) => JsonUtility.FromJson<T>(File.ReadAllText(GetSavePath(key)));
    public void Remove(string key) => File.Delete(GetSavePath(key));
    public void Put<T>(string key, T value)
    {
        if (_dataFolderPath == "")
            _dataFolderPath = _getDataFolderPath();
        if (!Directory.Exists(_dataFolderPath))
            Directory.CreateDirectory(_dataFolderPath);
        File.WriteAllText(GetSavePath(key), JsonUtility.ToJson(value));
    }

    private string GetSavePath(string saveName)
    {
        if (_dataFolderPath == "")
            _dataFolderPath = _getDataFolderPath();
        return Path.Combine(_dataFolderPath, saveName + _extension);
    }
}