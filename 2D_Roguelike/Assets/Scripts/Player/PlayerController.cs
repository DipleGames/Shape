using UnityEngine;
using System;
using UnityEditor.Experimental.GraphView;
using System.Collections.Generic;
using Unity.VisualScripting;

[DefaultExecutionOrder(-30)]
public class PlayerController : MonoBehaviour
{
    public PlayerManager pm;

    // ===== 이동 지점 =====
    public Vector3 targetPoint { get; private set; }


    #region 플레이어 체력 및 마나
    [Header("현재 체력 / 현재 마나")]
    [SerializeField] private float _hp;
    [SerializeField] private float _mp;

    public event Action<PlayerController> OnHpChanged, OnMpChanged;

    public float Hp
    {
        get => _hp;
        set
        {
            float max = pm.playerStat ? pm.playerStat.stat[StatType.MaxHp] : Mathf.Infinity;
            float nv = Mathf.Clamp(value, 0f, max);
            if (Mathf.Approximately(_hp, nv)) return;
            float ov = _hp;
            _hp = nv;
            OnHpChanged?.Invoke(this); // ui 처리 해야함
            if (_hp <= 0f) Die();
        }
    }

    public float Mp
    {
        get => _mp;
        set
        {
            float max = pm.playerStat ? pm.playerStat.stat[StatType.MaxMp] : Mathf.Infinity;
            float nv = Mathf.Clamp(value, 0f, max);
            if (Mathf.Approximately(_mp, nv)) return;
            float ov = _mp;
            _mp = nv;
            OnMpChanged?.Invoke(this); // ui 처리 해야함
        }
    }
    #endregion


    void Start()
    {
        pm = PlayerManager.Instance;

        OnHpChanged += UIManager.Instance.UpdateUIOnChangePlayerVital;
        OnMpChanged += UIManager.Instance.UpdateUIOnChangePlayerVital;
    }


    KeyCode key = KeyCode.None;
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            var tp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPoint = new Vector3(tp.x, tp.y, transform.position.z);

            pm.spriteRenderer.flipX = transform.position.x < targetPoint.x;
        }

        if (pm.playerStat == null) return;
        if (!pm.playerStat.stat.ContainsKey(StatType.Speed)) return;
        transform.position = Vector3.MoveTowards(transform.position, targetPoint, pm.playerStat.stat[StatType.Speed] * Time.deltaTime);
        
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

        key = KeyCode.None; // ← 매 프레임 끝에 리셋
    }


    public void OnApplyVital(Dictionary<StatType, float> newStat)
    {
        Hp = newStat[StatType.MaxHp];
        Mp = newStat[StatType.MaxMp];
    }

    public void TakeDamage(float amount)
    {
        Hp -= amount;
    }

    public void Die()
    {
        pm.spriteRenderer.enabled = false;
    }
}
