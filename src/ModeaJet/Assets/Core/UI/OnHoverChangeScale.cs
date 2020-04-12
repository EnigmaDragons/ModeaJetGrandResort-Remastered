using UnityEngine;
using UnityEngine.EventSystems;

public sealed class OnHoverChangeScale : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private FloatReference scaleFactor = new FloatReference(1);

    private Vector3 _previousScale;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        _previousScale = transform.localScale;
        transform.localScale = new Vector3(_previousScale.x * scaleFactor, _previousScale.y * scaleFactor, _previousScale.z * scaleFactor);
    }

    public void OnPointerExit(PointerEventData eventData) => transform.localScale = _previousScale;
}
