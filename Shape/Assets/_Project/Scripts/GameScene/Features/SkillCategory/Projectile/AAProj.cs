using UnityEngine;

public class AAProj : Proj
{
    private BattleSystem.AAPool _aaPool;
    private bool _isCritical;
    
    [Header("업그레이드 정보")]
    public int rangeUgCount = 1;
    public int penetrationUgCount = 1;

    [SerializeField] float _realLifetime = 0f;
    [SerializeField] float _hitCount = 0;

    // 생성시 초기화 작업
    public void InitAAProj(BattleSystem.AAPool pool, Vector3 direction, bool isCritical)
    {
        _aaPool = pool;
        _dir = direction.normalized;
        _isCritical = isCritical;
        _t = 0f;
        _hitCount = 0;
        _realLifetime = _lifetime; // 진짜 생존타임에 디폴트 라이프타임 대입

        // 업그레이드 횟수
        rangeUgCount = PrepareManager.Instance.aaShop.rangeUgCount;
        penetrationUgCount = PrepareManager.Instance.aaShop.penetrationUgCount;

        _realLifetime = _lifetime * (1f + 0.05f * (rangeUgCount - 1)); // 선형구조(복리 x)

        gameObject.SetActive(true);
    }

    protected override void Update()
    {
        base.Update();
        _t += Time.deltaTime;
        if (_t >= _realLifetime)
            Despawn();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        _damage = 
        _isCritical ?
        PlayerManager.Instance.playerStat.Stat[StatType.Attack] * PlayerManager.Instance.playerStat.Stat[StatType.CriticalValue] : 
        PlayerManager.Instance.playerStat.Stat[StatType.Attack];

        // 1) 일반 적
        if (collision.CompareTag("Enemy"))
        {
            var enemy = collision.GetComponent<EnemyController>();
            if (enemy != null)
            {
                _hitCount++;
                enemy.TakeDamage(_damage, _isCritical);
                Debug.Log($"{_damage} 데미지 → Enemy HP: {enemy.EnemyHP}");
            }

            if(_hitCount == penetrationUgCount) Despawn();

            return;
        }

        // 2) 보스
        if (collision.CompareTag("Boss"))
        {
            var boss = collision.GetComponent<BossController>();
            if (boss != null)
            {
                boss.TakeDamage(_damage);
                Debug.Log($"{_damage} 데미지 → Boss HP: {boss.BossHP}");
            }

            Despawn();
            return;
        }
    }

    void Despawn()
    {
        if (_aaPool != null) _aaPool.Return(gameObject);
        else gameObject.SetActive(false);
    }
}
