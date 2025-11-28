using UnityEngine;

public class Item : MonoBehaviour
{
    public float value; 
    public float speed = 5f;
    public bool isDrainArea = false;

    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();   
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log($"{value}");
        }
    }
    
    protected virtual void Update()
    {
        _rb.linearVelocity = Vector2.Lerp(_rb.linearVelocity, Vector2.zero, 3f * Time.deltaTime);
        if (!isDrainArea) return;

        Vector3 dir = (PlayerManager.Instance.player.transform.position - transform.position).normalized;
        transform.position += dir * speed * Time.deltaTime;
    }
}
