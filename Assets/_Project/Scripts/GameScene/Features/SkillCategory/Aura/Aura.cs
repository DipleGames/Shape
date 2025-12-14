using System.Collections.Generic;
using UnityEngine;

public class Aura : MonoBehaviour
{
    private SkillContext _ctx;

    [SerializeField] private float _tickInterval = 0.25f;

    // collider별 다음 데미지 가능 시간
    private readonly Dictionary<Collider2D, float> _tickTimeDic = new();

    public void Init(SkillContext ctx)
    {
        _ctx = ctx;
    }

    private void Update()
    {
        if (_ctx?.caster != null)
            transform.position = _ctx.caster.transform.position;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_ctx == null) return;

        // Enemy/Boss만 처리
        bool isEnemy = collision.CompareTag("Enemy");
        bool isBoss  = collision.CompareTag("Boss");
        if (!isEnemy && !isBoss) return;

        float now = Time.time;

        // 이 콜라이더의 다음 틱 시간 확인
        if (_tickTimeDic.TryGetValue(collision, out float nextTime) && now < nextTime)
            return;

        // 다음 틱 시간 갱신
        _tickTimeDic[collision] = now + _tickInterval;

        float damage = PlayerManager.Instance.statModel.Stat[StatType.Attack] * _ctx.skillDamage;

        if (isEnemy)
        {
            if (collision.TryGetComponent<EnemyController>(out var enemy))
                enemy.TakeDamage(damage, false);
        }
        else // Boss
        {
            if (collision.TryGetComponent<BossController>(out var boss))
                boss.TakeDamage(damage);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 영역에서 빠져나가면 쿨타임 기록 삭제(깔끔)
        _tickTimeDic.Remove(collision);
    }

    private void OnDisable()
    {
        // 오라 꺼질 때도 정리
        _tickTimeDic.Clear();
    }
}
