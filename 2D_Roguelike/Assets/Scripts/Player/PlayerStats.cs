// PlayerStats.cs
using UnityEngine;
using System;

[Serializable]
public struct Stat
{
    public float MaxHp;
    public float MaxMp;
    public float Attack;
    public float Speed;
}

[DefaultExecutionOrder(-40)]
public class PlayerStats : MonoBehaviour
{
    [Header("참조")]
    public Character character;
    public StatCalculator statCalc;

    [Header("플레이어 스탯")]
    [SerializeField] private Stat _stat;
    public Stat Stat => _stat; // 읽기 전용

    public event Action<Stat> OnStatChanged;

    private void Awake()
    {
        // 스탯이 변화할때마다 적용해줘야하잖아? 스탯이 변할떄마다 스탯을 적용시켜삐는 메서드 구독!!!
        if (statCalc != null)
        {
            statCalc.OnDefaultCalculated += OnApplyStat; // 1. 캐릭터 선택시 "스텟 계산기"에서 기초 스텟을 적용한 뒤 스텟결과창에 적용 
            statCalc.OnRecalculated += OnApplyStat; // 2. 스텟이 변화할때 "스텟 계산기"에서 계산이 완료되면 스텟결과창에 적용
        }
    }

    private void Start()
    {
        OnApplyStat(_stat); // 초기 1회
    }

    private void OnApplyStat(Stat newStat)
    {
        _stat = newStat;
        OnStatChanged?.Invoke(_stat);
    }

    private void OnDestroy()
    {
        if (statCalc != null)
        {
            statCalc.OnDefaultCalculated -= OnApplyStat;
            statCalc.OnRecalculated -= OnApplyStat;
        }
    }

}
