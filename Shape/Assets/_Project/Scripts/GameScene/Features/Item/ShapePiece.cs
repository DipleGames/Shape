using UnityEngine;
using System;

public class ShapePiece : Item
{
    public StatType statType;
    public float statValue = 0f;

    void OnEnable()
    {
        SetRamdomType();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ShapePieceManager.Instance.shapePieceList.Add(this);
        }
    }

    void SetRamdomType()
    {
        int ran = UnityEngine.Random.Range(0, 7);

        switch(ran)
        {
            case 0:
                statType = StatType.MaxHp;
                statValue = UnityEngine.Random.Range(30, 50) / 100f; // 0.3 ~ 0.5
                break;
            case 1:
                statType = StatType.MaxMp;
                statValue = UnityEngine.Random.Range(30, 50) / 100f; // 0.3 ~ 0.5
                break;
            case 2:
                statType = StatType.MaxStamina;
                statValue = UnityEngine.Random.Range(30, 50) / 100f; // 0.3 ~ 0.5
                break;
            case 3:
                statType = StatType.Attack;
                statValue = UnityEngine.Random.Range(30, 50) / 100f; // 0.3 ~ 0.5
                break;
            case 4:
                statType = StatType.Speed;
                statValue = UnityEngine.Random.Range(1, 6) / 100f; // 0.01 ~ 0.05
                break;
            case 5:
                statType = StatType.DrainArea;
                statValue = UnityEngine.Random.Range(1, 6) / 100f; // 0.01 ~ 0.05
                break;
            case 6:
                statType = StatType.CriticalValue;
                statValue = UnityEngine.Random.Range(30, 50) / 100f; // 0.3 ~ 0.5
                break;
        }
    }

    protected override void Update()
    {
        base.Update();
    }
}
