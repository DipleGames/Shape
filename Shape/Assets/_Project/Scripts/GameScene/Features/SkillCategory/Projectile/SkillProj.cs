using UnityEngine;

public class SkillProj : MonoBehaviour
{
    private SkillContext _ctx;
    private Vector3 _dir;
    private float _speed;
    private bool _isPenetrate;

    // Update is called once per frame
    public void Init(SkillContext ctx, float speed, bool isPenetrate)
    {
        _ctx = ctx;
        _speed = speed;
        _isPenetrate = isPenetrate;
        _dir = _ctx.targetPoint - _ctx.castOrigin;
        _dir.z = 0;
        
        float angle = Mathf.Atan2(_dir.y, _dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    void Update()
    {
        transform.position += _dir.normalized * _speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            Debug.Log("닿음");
            EnemyController enemyController = collision.GetComponent<EnemyController>();
            float damage = PlayerManager.Instance.playerStat.Stat[StatType.Attack] * _ctx.skillDamage; // 플레이어의 공격력 스탯 * 퓨어 스킬데미지
            enemyController.TakeDamage(damage, false); // 플레이어의 공격력 만큼 데미지;
            if(!_isPenetrate) Destroy(gameObject);
            return;
        }

        if(collision.CompareTag("Boss"))
        {
            Debug.Log("닿음");
            BossController bossController = collision.GetComponent<BossController>();
            float damage = PlayerManager.Instance.playerStat.Stat[StatType.Attack] * _ctx.skillDamage; // 플레이어의 공격력 스탯 * 퓨어 스킬데미지
            bossController.TakeDamage(damage); // 플레이어의 공격력 만큼 데미지;
            if(!_isPenetrate) Destroy(gameObject);
            return;
        }
    }
}
