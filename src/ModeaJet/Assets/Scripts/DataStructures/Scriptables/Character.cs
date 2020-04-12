using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Character : SerializedScriptableObject
{
    [SerializeField] private string name;
    [SerializeField] private string scanInfo;
    [SerializeField] private TextAsset dialogue;
    [SerializeField] private Dictionary<Expression, Sprite> expressions;

    public Expression CurrentExpression;
}
