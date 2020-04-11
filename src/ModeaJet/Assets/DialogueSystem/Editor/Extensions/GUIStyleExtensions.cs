using UnityEngine;

namespace Assets.Scripts.Editor
{
    public static class GUIStyleExtensions
    {
        public static int CalcLines(this GUIStyle style, string toCalc, float width)
        {
            var lineHeight = style.CalcHeight(new GUIContent("Q"), 999);
            var actualHeight = style.CalcHeight(new GUIContent(toCalc), width);
            return Mathf.CeilToInt(actualHeight / lineHeight);
        }
    }
}
