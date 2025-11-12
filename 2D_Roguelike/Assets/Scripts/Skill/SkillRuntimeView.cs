using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillRuntimeView : SingleTon<SkillRuntimeView>
{
    [SerializeField] SkillSlot skillSlot;   // D 슬롯
    [SerializeField] Slider slider;         // D 슬롯의 슬라이더 (참조 연결)


    BattleSystem.Utile utile;

    void Start()
    {
        utile = PlayerManager.Instance.battleSystem.utile;
        slider.minValue = 0f;
        slider.maxValue = 1f;
    }

    void Update()
    {
        if (utile == null) return;

        // 채워졌다가 줄어드는 느낌이면 그대로:
        slider.value = utile.DashNormalized; // 1 → 0

    }
}

