using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace EnigmaDragons.NodeSystem.Editor
{
    public static class ScriptableExtensions
    {
        public static List<T> GetAllInstances<T>() where T : ScriptableObject
            => AssetDatabase.FindAssets("t:" + typeof(T).Name)
                .Select(x => AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(x))).ToList();

        public static Object[] GetAllInstances(Type type)
            => AssetDatabase.FindAssets("t:" + type.Name)
                .Select(x => AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(x), type)).ToArray();

        public static IEnumerable<Object> GetAllInstancesByLabel(string label)
            => AssetDatabase.FindAssets("l:" + label)
                .Select(x => AssetDatabase.LoadAssetAtPath<Object>(AssetDatabase.GUIDToAssetPath(x))).ToList();
    }
}