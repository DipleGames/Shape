using UnityEngine;

public class BossMinion : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("HP")]
    [SerializeField] private float maxHP;
    private float _hp;

    [Header("Damage Tick")]
    [SerializeField] private float tickInterval = 0.5f;

    private Transform target;
    private Rigidbody2D _rb;
    private Animator _anim;
    private float _nextTickTime = 0f;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    void Start()
    {    
        target = GameObject.FindWithTag("Player").transform;  
        maxHP = 180f * GameManager.Instance.Stage;
        _hp = maxHP;
    }

    void FixedUpdate()
    {
        if(target == null) return; 
        Vector3 dir = (target.position - transform.position).normalized;

        // 이동
        _rb.linearVelocity = dir * moveSpeed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("aaProj"))
        {
            _hp -= PlayerManager.Instance.statModel.Stat[StatType.Attack];
            collision.GetComponent<AAProj>().Despawn();
            _anim.SetTrigger("Hit");
        }
        
        if(_hp <= 0) Destroy(gameObject);
    }



    void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Player")) return;

        float now = Time.time;
        if (now < _nextTickTime) return;

        _nextTickTime = now + tickInterval;

        if (collision.collider.TryGetComponent<PlayerController>(out var player))
        {
            int damage = (GameManager.Instance.Stage + 2) * 5;
            player.TakeDamage(damage);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Player")) return;

        // 다시 닿았을 때 즉시 1틱 들어가게 하고 싶으면 now로 초기화
        _nextTickTime = 0f;
    }
}
