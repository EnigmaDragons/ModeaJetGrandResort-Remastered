using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Content/Character")]
public class Character : SerializedScriptableObject
{
    [SerializeField] private string name;
    [SerializeField, TextArea(10, 12)] private string scanInfo;
    [SerializeField] private TextAsset dialogue;
    [SerializeField] private Dictionary<Expression, Sprite> expressions;

    public Expression CurrentExpression;
}
