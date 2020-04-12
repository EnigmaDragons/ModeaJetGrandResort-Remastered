using UnityEngine;
using UnityEngine.EventSystems;

public sealed class HoverTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private string tooltip;
    
    public void OnPointerEnter(PointerEventData eventData) => Message.Publish(new ShowUiTooltip { Tooltip = tooltip });
    public void OnPointerExit(PointerEventData eventData) => Message.Publish(new ShowUiTooltip { Tooltip = "" });
}
