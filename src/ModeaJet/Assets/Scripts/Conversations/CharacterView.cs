using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterView : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI nameLabel;
    
    public void Init(Character character, Expression expression)
    {
        var sprite = character.GetExpression(expression);
        image.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, sprite.rect.width);
        image.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, sprite.rect.height);
        image.sprite = sprite;
        if (nameLabel != null)
            nameLabel.text = character.DisplayName;
    }
}
