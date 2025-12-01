using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireField : MonoBehaviour
{
    public Queue<GameObject> q;
    
    public void StartLifeCoroutine()
    {
        StartCoroutine(LifeCoroutine());
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyController>().TakeDamage(3f, false);
        }
    }

    IEnumerator LifeCoroutine()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
        q.Enqueue(gameObject);
    }
}
