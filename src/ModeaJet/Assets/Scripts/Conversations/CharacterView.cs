using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class CharacterView : OnMessage<ShowStatement, ShowOptions>
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI nameLabel;
    [SerializeField] private ColorReference unfocusedTint;

    private Character _character;
    private Color _originalTint;

    private void Awake() => _originalTint = image.color;
    private void OnEnable() => Highlight();
    public void Highlight() => image.color = _originalTint;
    public void Unhighlight() => image.color = unfocusedTint;

    protected override void Execute(ShowStatement msg) => image.color = msg.Character == _character ? _originalTint : unfocusedTint;
    protected override void Execute(ShowOptions msg) => Highlight();
    
    public void Init(Character character, Expression expression)
    {
        _character = character;
        var sprite = character.GetExpression(expression);
        image.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, sprite.rect.width);
        image.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, sprite.rect.height);
        image.sprite = sprite;
        if (nameLabel != null)
            nameLabel.text = character.DisplayName;
    }
}
