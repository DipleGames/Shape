using UnityEngine;

public class SkillProj : MonoBehaviour
{
    private SkillContext _ctx;
    private float _speed;
    private Vector3 _dir;

    // Update is called once per frame
    public void Init(SkillContext ctx, float speed)
    {
        _ctx = ctx;
        _speed = speed;
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
            enemyController.TakeDamage(damage); // 플레이어의 공격력 만큼 데미지;
        }
    }
}
