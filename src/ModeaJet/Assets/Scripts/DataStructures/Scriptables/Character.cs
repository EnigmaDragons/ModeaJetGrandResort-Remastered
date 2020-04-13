using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Content/Character")]
public class Character : SerializedScriptableObject
{
    [SerializeField] private string displayName;
    [SerializeField] private string scanName;
    [SerializeField, TextArea(10, 12)] private string scanInfo;
    [SerializeField] private TextAsset dialogue;
    [SerializeField] private Dictionary<Expression, Sprite> expressions;

    public Expression CurrentExpression;

    public string DisplayName => displayName;
    public string ScanName => scanName;
    public string ScanInfo => scanInfo;

    public Sprite GetExpression(Expression e) =>
        expressions.TryGetValue(e, out var exp) 
            ? exp 
            : expressions[Expression.Default];
}
