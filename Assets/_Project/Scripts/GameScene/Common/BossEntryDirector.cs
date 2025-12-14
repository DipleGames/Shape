using DG.Tweening;
using UnityEngine;
using System.Collections;

public class BossEntryDirector : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private Transform player;              // 빨려들어갈 대상
    [SerializeField] private GameObject portalPrefab;       // 발밑 포탈 프리팹
    [SerializeField] private Transform portalParent;        // 없으면 null 가능

    [Header("Portal Tunings")]
    [SerializeField] private float portalOpenTime = 0.35f;  // 포탈 펼쳐지는 시간
    [SerializeField] private float portalHoldTime = 0.15f;  // 잠깐 멈칫
    [SerializeField] private float suckTime = 0.55f;        // 빨려드는 시간
    [SerializeField] private Vector3 portalScale = new Vector3(2.2f, 2.2f, 2.2f);
    [SerializeField] private float playerSpin = 540f;       // 회전량(도)
    [SerializeField] private float sinkDown = 0.35f;        // 살짝 아래로 꺼지는 느낌

    [Header("Boss Room Reverse Tunings")]
    [SerializeField] private float popOutTime = 0.55f;      // 튀어나오는 시간
    [SerializeField] private float popUp = 0.45f;           // 위로 튀는 정도
    [SerializeField] private float portalCloseTime = 0.25f; // 포탈 닫히는 시간
    [SerializeField] private float exitSpin = 360f;         // 나올 때 회전량

    private bool _triggered;

    public void OnRageFull()
    {
        if (_triggered) return;
        _triggered = true;

        player = PlayerManager.Instance.player.transform;
        StartCoroutine(PortalSuckOnly());
    }

    private IEnumerator PortalSuckOnly()
    {
        // 1) 포탈 생성 (플레이어 발밑)
        Vector3 pos = player.position;
        GameObject portalGO = Instantiate(portalPrefab, pos, Quaternion.identity, portalParent);
        Transform portal = portalGO.transform;
        portal.localScale = Vector3.zero;

        var seq = DOTween.Sequence().SetUpdate(true);

        // 포탈 오픈
        seq.Append(portal.DOScale(portalScale * 2f, portalOpenTime).SetEase(Ease.OutBack));
        seq.AppendInterval(portalHoldTime);

        seq.Join(portal.DORotate(new Vector3(0, 0, 180f),
                portalOpenTime + 0.12f + portalHoldTime, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear));

        // 플레이어 움찔
        seq.Append(player.DOScale(1.05f, 0.08f).SetEase(Ease.OutQuad));
        seq.Append(player.DOScale(1.0f, 0.06f).SetEase(Ease.InQuad));

        // 빨려들기
        Vector3 startPos = player.position;
        Vector3 endPos = startPos + Vector3.down * sinkDown;

        seq.Append(player.DOMove(endPos, suckTime).SetEase(Ease.InQuad));
        seq.Join(player.DOScale(0.0f, suckTime).SetEase(Ease.InBack));
        seq.Join(player.DORotate(new Vector3(0, 0, playerSpin), suckTime, RotateMode.FastBeyond360)
            .SetEase(Ease.InCubic));

        // 포탈도 같이
        seq.Join(portal.DOScale(portalScale * 0.85f, suckTime).SetEase(Ease.InQuad));

        var sr = portalGO.GetComponentInChildren<SpriteRenderer>();
        if (sr != null) seq.Join(sr.DOFade(0f, suckTime).SetEase(Ease.InQuad));

        seq.AppendCallback(() => Destroy(portalGO));

        yield return seq.WaitForCompletion();

        // 여기서 씬 전환/페이지 전환
        GameManager.Instance.OnBossPhase();
    }

    /// <summary>
    /// 보스방에서 플레이어가 포탈로부터 튀어나오는 연출을 시작한다.
    /// </summary>
    public void PlayBossRoomExit(Vector3 spawnPos)
    {
        // 보스방 시작 시점에 player 참조가 끊길 수 있으면 다시 잡아도 됨
        if (player == null) player = PlayerManager.Instance.player.transform;

        StartCoroutine(PortalPopOut(spawnPos));
    }

    private IEnumerator PortalPopOut(Vector3 spawnPos)
    {
        // 1) 플레이어를 spawnPos로 이동 + "아직 안 보이게" 준비
        player.position = spawnPos + Vector3.down * sinkDown; // 포탈 아래에서 시작한 느낌
        player.localScale = Vector3.zero;

        // 2) 포탈 생성 (spawnPos)
        GameObject portalGO = Instantiate(portalPrefab, spawnPos, Quaternion.identity, portalParent);
        Transform portal = portalGO.transform;
        portal.localScale = Vector3.zero;

        // 포탈 스프라이트가 있으면 일단 보이게(이전 연출에서 페이드 아웃 했을 수도 있어서)
        var sr = portalGO.GetComponentInChildren<SpriteRenderer>();
        if (sr != null)
        {
            var c = sr.color;
            c.a = 1f;
            sr.color = c;
        }

        var seq = DOTween.Sequence().SetUpdate(true);

        // 3) 포탈 오픈
        seq.Append(portal.DOScale(portalScale * 2f, portalOpenTime).SetEase(Ease.OutBack));
        seq.AppendInterval(0.05f);

        // 포탈 회전(나올 때도 살짝)
        seq.Join(portal.DORotate(new Vector3(0, 0, 180f),
            portalOpenTime + 0.2f, RotateMode.FastBeyond360).SetEase(Ease.Linear));

        // 4) 플레이어 튀어나오기(위로 + 스케일 1 + 회전 풀림)
        Vector3 endPos = spawnPos + Vector3.up * popUp;

        seq.Append(player.DOMove(endPos, popOutTime).SetEase(Ease.OutCubic));
        seq.Join(player.DOScale(1.0f, popOutTime).SetEase(Ease.OutBack));
        seq.Join(player.DORotate(new Vector3(0, 0, -exitSpin), popOutTime, RotateMode.FastBeyond360)
            .SetEase(Ease.OutCubic));

        // 살짝 착지 느낌(선택)
        seq.Append(player.DOMove(spawnPos, 0.12f).SetEase(Ease.InQuad));

        // 5) 포탈 닫히며 사라짐
        seq.AppendInterval(0.05f);
        seq.Append(portal.DOScale(Vector3.zero, portalCloseTime).SetEase(Ease.InBack));

        if (sr != null) seq.Join(sr.DOFade(0f, portalCloseTime).SetEase(Ease.InQuad));

        seq.AppendCallback(() => Destroy(portalGO));

        yield return seq.WaitForCompletion();
    }
}
