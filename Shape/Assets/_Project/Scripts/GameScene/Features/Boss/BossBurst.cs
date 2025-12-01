using UnityEngine;
using System.Collections;

public class BossBurst : MonoBehaviour
{
    private float _damage;

    public void InitBurst(float damage, float duration)
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
