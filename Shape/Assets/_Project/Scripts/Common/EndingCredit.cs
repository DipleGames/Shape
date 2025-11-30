using DG.Tweening;
using UnityEngine;

public class EndingCredit : MonoBehaviour
{
    public RectTransform creditPanel;
    public float rollDuration = 20f;

    void Start()
    {
        // 시작 위치(아래)
        creditPanel.anchoredPosition = new Vector2(0, -Screen.height);

        // 목표 위치(위로 화면 밖)
        float targetY = Screen.height;

        creditPanel.DOAnchorPosY(targetY, rollDuration)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
                #endif
            });
    }
}
