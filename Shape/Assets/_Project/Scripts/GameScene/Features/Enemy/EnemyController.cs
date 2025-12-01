using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class EnemyController : MonoBehaviour
{
    public Transform target;
    public PoolManager.EnemyPool OriginPool { get; set; }
    [SerializeField] private GameObject _deathEffectPrefab;
    [SerializeField] private Transform _canvasTr;

    [Header("적 객체")]
    public Enemy enemy;

    [Header("적 스펙")]
    [SerializeField] private float _enemyHp;
    [SerializeField] private float _maxHp;

    public float EnemyHP
    {
        get => _enemyHp;
        set
        {
            float nv = Mathf.Clamp(value, 0f, _maxHp);

            if (Mathf.Approximately(_enemyHp, nv)) return;
            _enemyHp = nv;

            OnEnemyHpChanged?.Invoke(_maxHp, _enemyHp);
            if (_enemyHp <= 0f) Die();
        }
    }
    public float enemySpeed;
    [SerializeField] private float _delay = 1f;

    public event Action<float, float> OnEnemyHpChanged;

    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider2D;
    private TextMeshProUGUI _health_Text;
    private Slider _health_Slider;
    private Animator _anim;
    private Rigidbody2D _rb;

    [Header("돌연변이인가?")]
    public bool isMutation = false;
    
    [Header("돌연변이 돌진 패턴 설정")]
    [SerializeField] private float _detectRange = 5f;    // 사거리
    [SerializeField] private float _chargeTime = 1f;     // 기 모으는 시간
    [SerializeField] private float _dashDistance = 5f;   // 돌진 거리
    [SerializeField] private float _dashDuration = 0.2f; // 돌진하는 데 걸리는 시간

    private bool _isCharging = false;
    private bool _isDashing  = false;
    private bool _hasHitPlayerOnDash = false;
    private Vector2 _dashDir;


    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _rb = GetComponent<Rigidbody2D>();
        //_health_Text = GetComponentInChildren<TextMeshProUGUI>(); 슬라이더로할땐 잠시 보류
        _health_Slider = GetComponentInChildren<Slider>();
        _anim = GetComponent<Animator>();
        //OnEnemyHpChanged += UpdateHealthTextUI;
        OnEnemyHpChanged += UpdateHealthBarUI;
    }

    void Start()
    {    
        target = GameObject.FindWithTag("Player").transform;  
    }

    void FixedUpdate()
    {
        // 돌진 중이거나 기 모으는 중에는 기존 Move/Rotate 막기
        if (_isCharging || _isDashing)
            return;

        // 사거리 안에 플레이어가 들어오면 돌진 패턴 시작
        float dist = Vector2.Distance(transform.position, target.position);
        if (isMutation && dist <= _detectRange)
        {
            MutationChargeAttack();
            return;
        }

        // 평소에는 기존 AI
        Move();
        Rotate();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_isDashing) return;
        if (_hasHitPlayerOnDash) return;

        if (collision.collider.CompareTag("Player"))
        {
            PlayerManager.Instance.playerController.TakeDamage(enemy.attackDamage * 1.5f);
            _hasHitPlayerOnDash = true;
        }
    }


    void LateUpdate()
    {
        // 부모의 회전을 상쇄시키기
        _canvasTr.rotation = Quaternion.identity;
    }

    // void EnemyAI() // 나중에 좀 똑똑하게 바꿀때 쓸꺼임
    // {
    //     Move();
    // }

    float t = 0.9f;
    public void Attack()
    {
        t += Time.deltaTime;
        if(t >= _delay)
        {
            PlayerManager.Instance.playerController.TakeDamage(enemy.attackDamage);
            t = 0f;
        }
    }

    void Move()
    {
        // 방향 벡터 계산
        Vector3 dir = (target.position - transform.position).normalized;

        // 이동
        _rb.linearVelocity = dir * enemySpeed;

            // 플립
        if (transform.position.x > target.position.x)
            _spriteRenderer.flipX = true;
        else
            _spriteRenderer.flipX = false;
    }

    void Rotate()
    {
        Vector3 dir = (target.position - transform.position).normalized;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
    }

    /// <summary>
    /// 초기화 작업
    /// </summary>
    public void EnemyInit()
    {
        if (isMutation)
        {
            _maxHp = enemy.hp * 5f;
            enemySpeed = enemy.speed;
            _spriteRenderer.sprite = enemy.mutationSprtie;
        }
        else
        {
            _maxHp = enemy.hp;
            enemySpeed = enemy.speed;
            _spriteRenderer.sprite = enemy.sprite;
        }

        EnemyHP = _maxHp;   // 현재 체력을 최대체력으로 세팅
        SetColliderSize();
    }


    void UpdateHealthTextUI(float maxHp, float currentHp)
    {
        _health_Text.text = $"{(int)currentHp}";
    }

    void UpdateHealthBarUI(float maxHp, float currentHp)
    {
        _health_Slider.value = currentHp / maxHp;
    }

    public void SetColliderSize()
    {
        var lb = _spriteRenderer.localBounds;   // 피벗 반영된 로컬 공간 Bounds
        _boxCollider2D.size   = lb.size;       // 가로/세로
        _boxCollider2D.offset = lb.center;     // 피벗이 중앙이 아니어도 정렬됨
    }


    public void TakeDamage(float amount, bool isCritical)
    {
        EnemyHP -= amount;
        PlayerManager.Instance.playerController.Hp += amount * ( PlayerManager.Instance.playerController.lifeLeech / 100 ); // 생명력 흡수 로직
        //AudioManager.Instance.PlayEnemyHitSFX();
        PoolManager.Instance.hitTextPools.GetHitText(this, isCritical, amount);
        _anim.SetTrigger("Hit");
    }

    void Die()
    {
        ItemManager.Instance.DropExp(transform.position, enemy.xpValue);

        int ran = UnityEngine.Random.Range(0,99);
        if(ran < 20)
            ItemManager.Instance.DropHeal(transform.position);
        GameManager.Instance.IncreaseThreatGuage(100); // 임시로 100 넣어놓음
        CoinManager.Instance.AddCoin((int)enemy.hp / 10);
        ParticleSystem ps = PoolManager.Instance.deathEffectPools.GetParticleSystem(transform.position);
        PoolManager.Instance.deathEffectPools.particleQueue.Enqueue(ps); // 생성과 동시에 넣어주기. 근데 왜 disable할때 넣으면 오류나는지 모르겠음.. 트러블슈팅과제
        OriginPool.Return(gameObject);
    }

    public void DespawnEnemy()
    {
        OriginPool.Return(gameObject);
    }

    void MutationChargeAttack()
    {
        if (_isCharging || _isDashing) return;

        _isCharging = true;

        // 플레이어 방향 고정 (이후 플레이어가 움직여도 방향 안 바뀜)
        _dashDir = (target.position - transform.position).normalized;

        // 리지드바디 멈추기
        _rb.linearVelocity = Vector2.zero;

        // 돌진 방향으로 회전도 맞춰줌 (선택)
        float angle = Mathf.Atan2(_dashDir.y, _dashDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);

        // DOTween으로 기 모으는 연출
        // 예시: 살짝 커졌다가 돌아오기 + 붉게 번쩍
        var seq = DOTween.Sequence();

        // 스케일로 떨리는 느낌
        seq.Append(transform.DOScale(1.7f, _chargeTime * 0.5f).SetLoops(2, LoopType.Yoyo));

        // 3) 기모으기 끝나면 돌진 시작
        seq.AppendCallback(() =>
        {
            _isCharging = false;
            _isDashing  = true;
            _hasHitPlayerOnDash = false;   // ← 이번 돌진 동안 아직 안 맞았다

            Vector3 startPos  = transform.position;
            Vector3 targetPos = startPos + (Vector3)_dashDir * _dashDistance;

            _rb.linearVelocity = Vector2.zero;

            transform.DOMove(targetPos, _dashDuration)
                .SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    _isDashing = false;
                });
        });

    }
}
