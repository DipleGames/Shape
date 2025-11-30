using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class BossController : MonoBehaviour
{
    public GameObject target;
    public Boss[] boss;
    [SerializeField] private GameObject _preparePortal;

    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider2D;
    private TextMeshProUGUI _health_Text;
    private Slider _health_slider;
    private Animator _anim;
    private Rigidbody2D _rb;


    [Header("보스 스펙")]
    [SerializeField] private float _bossHp;
    public float BossHP
    {
        get => _bossHp;
        set
        {
            float max = boss[GameManager.Instance.Stage - 1].bossHp;
            float nv = Mathf.Clamp(value, 0f, max); // 밸류값이 0과 max사이에서 nv에 저장하고
            if (Mathf.Approximately(_bossHp, nv)) return; // 기존 hp값과 새로운 밸류값이 차이가없으면 리턴해버리고
            float ov = _bossHp; // 차이가 있다면 기존 hp값을 잠시 넣어둔다음 
            _bossHp = nv; // 새로운값을 _enemyMaxHp에 넣는다. 
            OnBossHpChanged.Invoke(max, _bossHp); // ui 처리 해야함
            if (_bossHp <= 0f) OnBossDie.Invoke();
        }
    }

    public event Action<float,float> OnBossHpChanged;
    public event Action OnBossDie;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        //_health_Text = GetComponentInChildren<TextMeshProUGUI>();
        _health_slider = GetComponentInChildren<Slider>();
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();

        //OnBossHpChanged += UpdateBossHealthTextUI;
        OnBossHpChanged += UpdateBossHealthBartUI;
        OnBossDie += BossDie;
    }


    void Start()
    {
        BossInit();
        SetColliderSize();
        target = GameObject.FindWithTag("Player");  
    }

    [SerializeField] private float _patternDelay = 3f;
    private float _t = 0f;
    void Update()
    {
        _t += Time.deltaTime;
        if(_patternDelay <= _t)
        {
            int ran = UnityEngine.Random.Range(0,boss[GameManager.Instance.Stage - 1].patterns.Length);    
            StartCoroutine(boss[GameManager.Instance.Stage - 1].patterns[ran].pattern.ExecutePattern(this, boss[GameManager.Instance.Stage - 1].patterns[ran].parameters));
            _t = 0f;
        }
    }

    void UpdateBossHealthTextUI(float maxHp, float currentHp)
    {
        _health_Text.text = $"{(int)currentHp}";
    }

    void UpdateBossHealthBartUI(float maxHp, float currentHp)
    {
        _health_slider.value = currentHp / maxHp;
    }

    public void TakeDamage(float amount)
    {
        BossHP -= amount; 
        _anim.SetTrigger("Hit");
    }

    public void SetColliderSize()
    {
        var lb = _spriteRenderer.localBounds;   // 피벗 반영된 로컬 공간 Bounds
        _boxCollider2D.size   = lb.size;       // 가로/세로
        _boxCollider2D.offset = lb.center;     // 피벗이 중앙이 아니어도 정렬됨
    }

    public void BossInit()
    {
        _spriteRenderer.sprite = boss[GameManager.Instance.Stage - 1].sprite;
        BossHP = boss[GameManager.Instance.Stage - 1].bossHp;
    }

    public void BossDie()
    {
        // 현재 색
        Color originalColor = _spriteRenderer.color;

        // 흔들림 + 축소 + 페이드아웃 시퀀스
        Sequence seq = DOTween.Sequence();

        seq.Append(transform.DOShakePosition(
            duration: 2f,
            strength: new Vector3(0.5f, 0.5f, 0),
            vibrato: 40,
            randomness: 90,
            fadeOut: true
        ));

        // 2) 축소 (0.5초)
        seq.Append(transform.DOScale(0f, 0.5f).SetEase(Ease.InBack));

        // 3) 동시에 페이드아웃
        seq.Join(_spriteRenderer.DOFade(0f, 0.5f));

        // 4) 끝나면 사라지기
        seq.OnComplete(() =>
        {
            GameManager.Instance.ClearBossPatterns();
            var portal = Instantiate(_preparePortal, transform.position, Quaternion.identity);
            if(GameManager.Instance.Stage == 7) portal.GetComponent<SpriteRenderer>().color = Color.blue;
            ShapeGrowthManager.Instance.shapeGrowth.AddShapePoint(GameManager.Instance.Stage + 1);
            Destroy(gameObject);
        });

    }
}
