using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class AAProj : MonoBehaviour
{
    public float projSpeed;
    public float lifetime = 5f;

    private float _t = 0f;
    private Vector3 _dir;
    private BattleSystem.AAPool _aaPool;

    public void InitAAProj(BattleSystem.AAPool pool, Vector3 direction)
    {
        _aaPool = pool;
        _dir = direction.normalized;
        _t = 0f;
        gameObject.SetActive(true);
    }

    void Update()
    {
        transform.position += _dir * projSpeed * Time.deltaTime;

        _t += Time.deltaTime;
        if (_t >= lifetime)
        Despawn();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;

        float damage = PlayerManager.Instance.playerStat.stat[StatType.Attack];
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
