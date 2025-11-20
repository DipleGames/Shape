using UnityEngine;

public class AAProj : Proj
{
    private BattleSystem.AAPool _aaPool;

    public void InitAAProj(BattleSystem.AAPool pool, Vector3 direction)
    {
        _aaPool = pool;
        _dir = direction.normalized;
        _t = 0f;
        gameObject.SetActive(true);
    }

    protected override void Update()
    {
        base.Update();
        _t += Time.deltaTime;
        if (_t >= _lifetime)
            Despawn();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        _damage = PlayerManager.Instance.playerStat.stat[StatType.Attack];

        // 1) 일반 적
        if (collision.CompareTag("Enemy"))
        {
            var enemy = collision.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.TakeDamage(_damage);
                Debug.Log($"{_damage} 데미지 → Enemy HP: {enemy.EnemyHP}");
            }

            Despawn();
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
