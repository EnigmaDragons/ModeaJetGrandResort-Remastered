using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public static class ScriptableExtensions
{
    public static List<T> GetAllInstances<T>() where T : ScriptableObject 
        => AssetDatabase.FindAssets("t:" + typeof(T).Name)
            .Select(x => AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(x))).ToList();

    public static List<Object> GetAllInstances(Type type)
        => AssetDatabase.FindAssets("t:" + type.Name)
            .Select(x => AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(x), type)).ToList();
}
