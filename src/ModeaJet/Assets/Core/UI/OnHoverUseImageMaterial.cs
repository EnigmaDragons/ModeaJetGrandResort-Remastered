using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public sealed class OnHoverUseImageMaterial : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image image;
    [SerializeField] private Material hoverMaterial;
    private Material _initialMaterial;

    private void Awake() => _initialMaterial = image.material;

    public void OnPointerEnter(PointerEventData eventData) => image.material = hoverMaterial;
    public void OnPointerExit(PointerEventData eventData) => image.material = _initialMaterial;
}
