using UnityEngine;

public class BossProj : Proj
{
    protected override void Update()
    {
        base.Update();
         _t += Time.deltaTime;
        if (_t >= _lifetime)
            Destroy(gameObject);

    }
    public void InitBossProj(float lifetime, float speed, float damage, Vector3 direction)
    {
        _lifetime = lifetime;
        _projSpeed = speed;
        _damage = damage;
        _t = 0f;
        _dir = direction.normalized;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // 1) 일반 적
        if (collision.CompareTag("Player"))
        {
            var player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(_damage);
            }
            Destroy(gameObject);
        }
    }
}
