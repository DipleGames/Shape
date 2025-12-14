using UnityEngine;

public class Aim : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Canvas canvas;

    void Awake()
    {
        // 자기 RectTransform 자동 캐싱
        if (rectTransform == null)
            rectTransform = transform as RectTransform;

        // 부모에서 Canvas 자동 찾기
        if (canvas == null)
            canvas = GetComponentInParent<Canvas>();
    }

    void Update()
    {
        FollowMouse();
    }

    void FollowMouse()
    {
        if (canvas == null || rectTransform == null)
        {
            Debug.LogError("Aim: canvas 또는 rectTransform이 null입니다.", this);
            return;
        }

        Vector2 localPos;

        RectTransform canvasRect = canvas.transform as RectTransform;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            Input.mousePosition,
            canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera,
            out localPos);

        rectTransform.anchoredPosition = localPos;
    }

    public Vector3 GetAimWorldPos(float zPlane = 0f)
    {
        Vector3 aimPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        aimPos.z = zPlane;
        return aimPos;
    }
}
