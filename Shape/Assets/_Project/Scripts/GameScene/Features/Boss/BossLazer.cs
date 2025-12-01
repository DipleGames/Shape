using UnityEngine;

public class BossLazer : MonoBehaviour
{
    private float _damage;
    public void InitBossLazer(float damage)
    {
        _damage = damage;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            PlayerController pc = collision.GetComponent<PlayerController>();
            pc.TakeDamage(_damage);
        }
    }
}
