using UnityEngine;
using System;

[DefaultExecutionOrder(-30)]
public class PlayerController : MonoBehaviour
{
    public PlayerStats playerStats;

    // ===== 이동 지점 =====
    public Vector3 targetPoint { get; private set; }


    #region 플레이어 체력 및 마나
    [Header("현재 체력 / 현재 마나")]
    [SerializeField] private float _hp;
    [SerializeField] private float _mp;

    public event Action<float, float> OnHpChanged, OnMpChanged;

    public float Hp
    {
        get => _hp;
        set
        {
            float max = playerStats ? playerStats.Stat.MaxHp : Mathf.Infinity;
            float nv = Mathf.Clamp(value, 0f, max);
            if (Mathf.Approximately(_hp, nv)) return;
            float ov = _hp;
            _hp = nv;
            OnHpChanged?.Invoke(ov, _hp); // ui 처리 해야함
            if (_hp <= 0f) Die();
        }
    }

    public float Mp
    {
        get => _mp;
        set
        {
            float max = playerStats ? playerStats.Stat.MaxMp : Mathf.Infinity;
            float nv = Mathf.Clamp(value, 0f, max);
            if (Mathf.Approximately(_mp, nv)) return;
            float ov = _mp;
            _mp = nv;
            OnMpChanged?.Invoke(ov, _mp); // ui 처리 해야함
        }
    }
    #endregion

    void Awake()
    {
        playerStats.statCalc.OnDefaultCalculated += OnInitialize; // 1. 캐릭터 선택시 "스텟 계산기"에서 기초 스텟을 적용한 뒤 현재 체력(풀피) 현재 마나(풀마나) 반영
    }

    void Start()
    {
        playerStats = PlayerManager.Instance.playerStats;
    }


    KeyCode key = KeyCode.None;
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            var tp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPoint = new Vector3(tp.x, tp.y, transform.position.z);

            PlayerManager.Instance.spriteRenderer.flipX = transform.position.x < targetPoint.x;
        }

        if (Input.GetKeyDown(KeyCode.A)) key = KeyCode.A;
        else if (Input.GetKeyDown(KeyCode.Q)) key = KeyCode.Q;
        else if (Input.GetKeyDown(KeyCode.W)) key = KeyCode.W;
        else if (Input.GetKeyDown(KeyCode.E)) key = KeyCode.E;
        else if (Input.GetKeyDown(KeyCode.R)) key = KeyCode.R;
        
        switch (key)
        {
            case KeyCode.A:
                PlayerManager.Instance.battleSystem.autoAttack.AA();
                break;
            case KeyCode.Q:
                break;
            case KeyCode.W:
                break;
            case KeyCode.E:
                break;
            case KeyCode.R:
                break;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPoint, playerStats.Stat.Speed * Time.deltaTime);
        key = KeyCode.None; // ← 매 프레임 끝에 리셋
    }

    void OnInitialize(Stat newStat)
    {
        Hp = newStat.MaxHp;
        Mp = newStat.MaxMp;
    }

    public void Die()
    {
        // 사망 처리
    }
}
