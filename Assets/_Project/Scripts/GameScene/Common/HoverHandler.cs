using UnityEngine;
using UnityEngine.EventSystems;

public class HoverHandler : MonoBehaviour
{
    public void OnHoverEnter(BaseEventData eventData)
    {
        // 이벤트 데이터 → PointerEventData로 변환
        PointerEventData pointerData = eventData as PointerEventData;

        // 현재 마우스가 올라간 GameObject
        GameObject hoveredObject = pointerData.pointerEnter;
    }

    public void OnHoverExit(BaseEventData eventData)
    {
        PointerEventData pointerData = eventData as PointerEventData;
        GameObject hoveredObject = pointerData.pointerEnter;
    }
}
