using System;
using System.Reflection;

namespace EnigmaDragons.NodeSystem
{
    public static class ReflectionUtilities
    {
        public static void SetArrayValueOnField(FieldInfo info, object target, object[] instances)
        {
            var arrItemType = info.FieldType.GetElementType();
            var typedArray = Array.CreateInstance(arrItemType, instances.Length);
            for (var i = 0; i < instances.Length; i++)
                typedArray.SetValue(instances[i], i);
            info.SetValue(target, typedArray);
        }

        public static void SetValueValueOnProperty(PropertyInfo info, object target, object[] instances)
        {
            var arrItemType = info.PropertyType.GetElementType();
            var typedArray = Array.CreateInstance(arrItemType, instances.Length);
            for (var i = 0; i < instances.Length; i++)
                typedArray.SetValue(instances[i], i);
            info.SetValue(target, typedArray);
        }
    }
}
