    using UnityEngine;
using System;
using System.Collections.Generic;

public class ItemManager : SingleTon<ItemManager>
{
    [Header("expPool")]
    [SerializeField] private GameObject _expPrefab;
    [SerializeField] private int _expSize = 256;
    public Queue<GameObject> expPool = new();

    void Start()
    {
        BuildExpPool();
        BuildHpPool();
    }

    public void BuildExpPool()
    {
        for (int i = 0; i < _expSize; i++)
        {
            var go = Instantiate(_expPrefab, transform);
            go.SetActive(false);
            expPool.Enqueue(go);
        }

    }

    public GameObject GetExp(float xpValue)
    {
        GameObject go;
        if(expPool.Count == 0)
        {
            go = Instantiate(_expPrefab, transform);
            go.SetActive(false);
            expPool.Enqueue(go);
        }

        go = expPool.Dequeue();
        go.GetComponent<Exp>().value = xpValue;
        go.SetActive(true);
        return go;
    }

    public void ReturnExp(GameObject go)
    {
        go.SetActive(false);
        go.transform.SetParent(transform);
        expPool.Enqueue(go);
    }

    [Header("hpPool")]
    [SerializeField] private GameObject _hpPrefab;
    [SerializeField] private int _hpSize = 256;
    public Queue<GameObject> hpPool = new();

    public void BuildHpPool()
    {
        for (int i = 0; i < _hpSize; i++)
        {
            var go = Instantiate(_hpPrefab, transform);
            go.SetActive(false);
            hpPool.Enqueue(go);
        }

    }

    public GameObject GetHp()
    {
        GameObject go;
        if(hpPool.Count == 0)
        {
            go = Instantiate(_hpPrefab, transform);
            go.SetActive(false);
            hpPool.Enqueue(go);
        }

        go = hpPool.Dequeue();
        go.SetActive(true);
        return go;
    }

    public void ReturnHp(GameObject go)
    {
        go.SetActive(false);
        go.transform.SetParent(transform);
        hpPool.Enqueue(go);
    }

    public void DropExp(Vector3 pos, float xpValue)
    {
        var go = GetExp(xpValue); 
        go.transform.position = pos;

        // 360도 랜덤 방향
        Vector2 randomDir = UnityEngine.Random.insideUnitCircle.normalized;

        // 튀는 힘
        float force = UnityEngine.Random.Range(1.5f, 3f);

        // 튕겨 나가는 연출
        Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
        rb.AddForce(randomDir * force, ForceMode2D.Impulse);
    }    

    public void DropHp(Vector3 pos)
    {
        var go = GetHp(); 
        go.transform.position = pos;

        // 360도 랜덤 방향
        Vector2 randomDir = UnityEngine.Random.insideUnitCircle.normalized;

        // 튀는 힘
        float force = UnityEngine.Random.Range(1.5f, 3f);

        // 튕겨 나가는 연출
        Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
        rb.AddForce(randomDir * force, ForceMode2D.Impulse);
    }    
}
