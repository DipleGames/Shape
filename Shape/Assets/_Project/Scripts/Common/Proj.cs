using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class Proj : MonoBehaviour
{
    [Header("Proj 기본 세팅")]
    [SerializeField] protected float _projSpeed;
    [SerializeField] protected float _lifetime = 5f;
    [SerializeField] protected float _damage;

    protected float _t = 0f;
    protected Vector3 _dir;

    protected virtual void Update()
    {
        transform.position += _dir * _projSpeed * Time.deltaTime;
    }
}
