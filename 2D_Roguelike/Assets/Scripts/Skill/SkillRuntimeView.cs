using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;


public class SkillRuntimeView : SingleTon<SkillRuntimeView>
{
    Character ch;

    [Header("Utile")]
    public SkillSlot D_skillSlot;   // D 슬롯
    [SerializeField] private Slider _D_slider;         // D 슬롯의 슬라이더 (참조 연결)


    [Header("Active")]
    public SkillSlot Q_skillSlot;   // D 슬롯
    [SerializeField] private Slider _Q_slider;         // D 슬롯의 슬라이더 (참조 연결)
    public SkillSlot W_skillSlot;   // D 슬롯
    [SerializeField] private Slider _W_slider;         // D 슬롯의 슬라이더 (참조 연결)
    public SkillSlot E_skillSlot;   // D 슬롯
    [SerializeField] private Slider _E_slider;         // D 슬롯의 슬라이더 (참조 연결)
    public SkillSlot R_skillSlot;   // D 슬롯
    [SerializeField] private Slider _R_slider;         // D 슬롯의 슬라이더 (참조 연결)



    void Start()
    {
        _D_slider.minValue = 0f;
        _D_slider.maxValue = 1f;

        _Q_slider.minValue = 0f;
        _Q_slider.maxValue = 1f;

        _W_slider.minValue = 0f;
        _W_slider.maxValue = 1f;

        _E_slider.minValue = 0f;
        _E_slider.maxValue = 1f;

        _R_slider.minValue = 0f;
        _R_slider.maxValue = 1f;
    }

    void Update() // 나중에 코루틴으로 .. 
    {
        if (D_skillSlot.skillInstance.skill == null) return;
        if (Q_skillSlot.skillInstance.skill == null) return;
        if (W_skillSlot.skillInstance.skill == null) return;
        if (E_skillSlot.skillInstance.skill == null) return;
        if (R_skillSlot.skillInstance.skill == null) return;
        // 채워졌다가 줄어드는 느낌이면 그대로:
        _D_slider.value = D_skillSlot.skillInstance.Normalized; // 1 → 0
        _Q_slider.value = Q_skillSlot.skillInstance.Normalized; // 1 → 0
        _W_slider.value = W_skillSlot.skillInstance.Normalized; // 1 → 0
        _E_slider.value = E_skillSlot.skillInstance.Normalized; // 1 → 0
        _R_slider.value = R_skillSlot.skillInstance.Normalized; // 1 → 0

    }

    // 캐릭터 선택시 스킬창에 스킬 세팅
    public void SetSkill(Character ch)
    {
        D_skillSlot.skill = ch.D_Skill;
        D_skillSlot.InitSkill();

        Q_skillSlot.skill = ch.Q_Skill;
        Q_skillSlot.InitSkill();

        W_skillSlot.skill = ch.W_Skill;
        W_skillSlot.InitSkill();

        E_skillSlot.skill = ch.E_Skill;
        E_skillSlot.InitSkill();

        R_skillSlot.skill = ch.R_Skill;
        R_skillSlot.InitSkill();
    }
}

