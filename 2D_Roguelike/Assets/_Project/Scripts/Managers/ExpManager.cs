using UnityEngine;
using System;
using System.Collections.Generic;

public class ExpManager : SingleTon<ExpManager>
{
    [Header("expPool")]
    [SerializeField] private GameObject _expPrefab;
    [SerializeField] private int _size = 256;
    public Queue<GameObject> expPool = new();

    void Start()
    {
        for (int i = 0; i < _size; i++)
        {
            var go = Instantiate(_expPrefab, transform);
            go.SetActive(false);
            expPool.Enqueue(go);
        }
    }

    public GameObject GetExp()
    {
        var go = expPool.Dequeue();
        go.SetActive(true);
        return go;
    }

    public void ReturnExp(GameObject go)
    {
        go.SetActive(false);
        go.transform.SetParent(transform);
        expPool.Enqueue(go);
    }
}
