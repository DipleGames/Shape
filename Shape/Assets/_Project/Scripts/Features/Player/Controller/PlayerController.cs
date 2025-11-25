using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

[DefaultExecutionOrder(-30)]
public class PlayerController : MonoBehaviour
{
    public PlayerManager pm;

    public Vector3 targetPoint;

    [Header("현재 체력 / 현재 마나")]
    [SerializeField] private float _hp;
    [SerializeField] private float _mp;
    [SerializeField] private float _stamina;

    public event Action<PlayerController> OnHpChanged, OnMpChanged, OnStaminaChanged;

    Rigidbody2D rb;     
    float speed => pm && pm.playerStat && pm.playerStat.stat.ContainsKey(StatType.Speed)
                    ? pm.playerStat.stat[StatType.Speed] : 0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public float Hp
    {
        get => _hp;
        set
        {
            float max = pm.playerStat ? pm.playerStat.stat[StatType.MaxHp] : Mathf.Infinity;
            float nv = Mathf.Clamp(value, 0f, max);
            if (Mathf.Approximately(_hp, nv)) return;
            _hp = nv;
            OnHpChanged?.Invoke(this);
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
            _mp = nv;
            OnMpChanged?.Invoke(this);
        }
    }

    public float Stamina
    {
        get => _stamina;
        set
        {
            float max = pm.playerStat ? pm.playerStat.stat[StatType.MaxStamina] : Mathf.Infinity;
            float nv = Mathf.Clamp(value, 0f, max);
            if (Mathf.Approximately(_stamina, nv)) return;
            _stamina = nv;
            OnStaminaChanged?.Invoke(this);
        }
    }

    void Start()
    {
        pm = PlayerManager.Instance;
        OnHpChanged += UIManager.Instance.playerView.UpdateUIOnChangePlayerVital;
        OnMpChanged += UIManager.Instance.playerView.UpdateUIOnChangePlayerVital;
        OnStaminaChanged += UIManager.Instance.playerView.UpdateUIOnChangePlayerVital;

        // 시작점 초기화
        targetPoint = transform.position;
    }

    public KeyCode key = KeyCode.None;
    public bool dashPressed = false;

    void Update()
    {
        // 목적지 갱신만 Update에서
        if (Input.GetMouseButtonDown(1))
        {
            var tp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPoint = new Vector3(tp.x, tp.y, transform.position.z);
            pm.spriteRenderer.flipX = transform.position.x < targetPoint.x;
        }

        // 공격 스킬
        if (Input.GetKeyDown(KeyCode.A)) key = KeyCode.A;
        else if (Input.GetKeyDown(KeyCode.Q)) key = KeyCode.Q;
        else if (Input.GetKeyDown(KeyCode.W)) key = KeyCode.W;
        else if (Input.GetKeyDown(KeyCode.E)) key = KeyCode.E;
        else if (Input.GetKeyDown(KeyCode.R)) key = KeyCode.R;

        // 이동 스킬(입력만 여기서받고 fixed update에서 처리해야함)
        if (Input.GetKeyDown(KeyCode.D))
        {
            if(UIManager.Instance.skillRuntimeView.D_skillSlot.skillInstance.TryDashCast())
            {
                dashPressed = true;   
            }
        }

        switch (key)
        {
            case KeyCode.A:
                pm.battleSystem.autoAttack.AA(pm);
                break;
            case KeyCode.Q:
                if(UIManager.Instance.skillRuntimeView.Q_skillSlot.skillInstance.TryCast()) // 쓸수있으면 
                {
                    pm.battleSystem.SkillExecutor(pm.character.Q_SkillInstance); // 써라
                }
                break;
            case KeyCode.W:
                if(UIManager.Instance.skillRuntimeView.W_skillSlot.skillInstance.TryCast())
                {
                    pm.battleSystem.SkillExecutor(pm.character.W_SkillInstance);
                }
                break;
            case KeyCode.E:
                if(UIManager.Instance.skillRuntimeView.E_skillSlot.skillInstance.TryCast())
                {
                    pm.battleSystem.SkillExecutor(pm.character.E_SkillInstance);
                }
                break;
            case KeyCode.R:
                if(UIManager.Instance.skillRuntimeView.R_skillSlot.skillInstance.TryCast())
                {
                    pm.battleSystem.SkillExecutor(pm.character.R_SkillInstance);
                }
                break;
        }

        key = KeyCode.None;
    }

    void FixedUpdate()
    {
        if (speed <= 0f) return;

        // 방향
        Vector3 dir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
        
        // 이동
        Vector2 cur = rb.position;
        Vector2 tp = new Vector2(targetPoint.x, targetPoint.y);
        Vector2 toTp = tp - cur;

        float step = speed * Time.fixedDeltaTime;
        if (toTp.sqrMagnitude > 0.0001f)
        {
            Vector2 next = cur + Vector2.ClampMagnitude(toTp, step);
            rb.MovePosition(next);
        }

        if (dashPressed)
        {
            dashPressed = false;
            StartCoroutine(pm.battleSystem.utile.DashRoutine(pm.rb, pm.character.D_SkillInstance.staminaCost));
        }
    }
       

    void OnCollisionStay2D(Collision2D collision)
    {
        // 충돌 중 공격 트리거는 성능/감각상 비추천.
        // 가능하면 Enemy가 Overlap/Trigger로 공격 판정하도록 이전.
        if (collision.collider.CompareTag("Enemy"))
        {
            var enemyController = collision.collider.GetComponent<EnemyController>();
            enemyController?.Attack();
        }
    }

    public void OnApplyVital(Dictionary<StatType, float> newStat)
    {
        Hp = newStat[StatType.MaxHp];
        Mp = newStat[StatType.MaxMp];
        Stamina = newStat[StatType.MaxStamina];
    }

    public void TakeDamage(float amount) => Hp -= amount;

    public void Die() => pm.spriteRenderer.enabled = false;

    public IEnumerator AutoManaRecoverCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            Mp += 2f;
        }
    }

    public IEnumerator AutoStaminaRecoverCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            Stamina += 2f;
        }
    }
}
