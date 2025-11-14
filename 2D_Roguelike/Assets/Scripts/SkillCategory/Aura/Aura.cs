using UnityEngine;

public class Aura : MonoBehaviour
{
    private SkillContext _ctx;

    // Update is called once per frame
    public void Init(SkillContext ctx)
    {
        _ctx = ctx;
    }

    void Update()
    {
        transform.position = _ctx.caster.transform.position;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            Debug.Log("닿음");
            EnemyController enemyController = collision.GetComponent<EnemyController>();
            float damage = PlayerManager.Instance.playerStat.Stat[StatType.Attack] * _ctx.skillDamage; // 플레이어의 공격력 스탯 * 퓨어 스킬데미지
            enemyController.TakeDamage(damage); // 플레이어의 공격력 만큼 데미지;
        }
    }
}
