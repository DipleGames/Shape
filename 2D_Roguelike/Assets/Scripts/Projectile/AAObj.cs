using UnityEngine;

public class AAObj : MonoBehaviour
{
    public Vector3 dir;
    public float objSpeed;
    public float lifetime = 5f;

    private float _t;
    private BattleSystem.AAPool _aaPool;

    public void OnSpawn(BattleSystem.AAPool pool, Vector3 direction, float speed)
    {
        _aaPool = pool;
        dir = direction.normalized;
        objSpeed = speed;
        _t = 0f;
        gameObject.SetActive(true);
    }

    void Update()
    {
        transform.position += dir * objSpeed * Time.deltaTime;

        _t += Time.deltaTime;
        if (_t >= lifetime)
            Despawn();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;

        float damage = PlayerManager.Instance.playerStats.Stat.Attack;
        var enemyController = collision.GetComponent<EnemyController>();
        enemyController.TakeDamage(damage);
        Debug.Log($"{damage}를 입혀서 {enemyController.EnemyMaxHP}가남음");

        Despawn();
    }

    void Despawn()
    {
        if (_aaPool != null) _aaPool.Return(gameObject);
        else gameObject.SetActive(false);
    }
}
