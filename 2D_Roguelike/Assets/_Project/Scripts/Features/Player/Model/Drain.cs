using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

public class Drain : MonoBehaviour
{
    [SerializeField] CircleCollider2D _cc;

    public void ChangeCircleSize(Dictionary<StatType,float> stat)
    {
        _cc.radius = stat[StatType.DrainArea];
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(_cc.IsTouching(collision))
        {
            if(collision.CompareTag("Exp"))
            {
                Exp exp = collision.GetComponent<Exp>();
                exp.isDrainArea = true;
            }
        }
    }
}
