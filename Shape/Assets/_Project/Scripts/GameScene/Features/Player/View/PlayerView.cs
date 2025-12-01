using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using TMPro;


public class PlayerView : MonoBehaviour
{
    [Header("Status Slider")]
    public Slider hpBar;
    public Slider mpBar;
    public Slider staminaBar;
    public Slider expBar;

    [Header("레벨 관련")]
    public Text level_Text;

    [Header("스탯 관련")]
    public GameObject stat_panel;
    public GameObject statText_panel;

    [Header("Shape 성장 시스템 UI")]
    public GameObject shapeGrowth_UI;
    [SerializeField] private List<Text> _statTexts = new List<Text>();

    [Header("웨폰 관련 UI")]
    public TextMeshProUGUI weaponLv_Text;

    [Header("크로스 헤어")]
    [SerializeField] private GameObject _aim;


    public void UpdateUIOnChangePlayerVital<T>(T t)
    {
        switch (t)
        {
            case LevelSystem levelSystem:
                expBar.value = levelSystem.Exp / levelSystem.RequiredExp;
                break;
            case PlayerController playerController:
                hpBar.value = playerController.Hp / playerController.pm.playerStat.stat[StatType.MaxHp];
                mpBar.value = playerController.Mp / playerController.pm.playerStat.stat[StatType.MaxMp];
                staminaBar.value = playerController.Stamina / playerController.pm.playerStat.stat[StatType.MaxStamina];
                break;
            default:
                break;
        }
    }

    public void UpdateUIOnChangePlayerStat(Dictionary<StatType, float> stat)
    {
        if (_statTexts.Count != stat.Count)
        {
            for (int i = 0; i < stat.Count; i++)
            {
                _statTexts.Add(statText_panel.transform.GetChild(i).GetComponent<Text>());
            }
            PlayerManager pm = PlayerManager.Instance;
            for (int i = 0; i < pm.playerStat.StatList.Count; i++)
            {
                float displayedValue = Mathf.Round(pm.playerStat.StatList[i].value * 10) / 10f;
                _statTexts[i].text = $"{displayedValue}";
            }
        }
        else
        {
            PlayerManager pm = PlayerManager.Instance;
            for (int i = 0; i < pm.playerStat.StatList.Count; i++)
            {
                float displayedValue = Mathf.Round(pm.playerStat.StatList[i].value * 10) / 10f;
                _statTexts[i].text = $"{displayedValue}";
            }
        }
    }

    void OnToggleStat()
    {
        bool isActive = !stat_panel.activeSelf;
        stat_panel.SetActive(isActive);
    }

    void OnToggleShapeGrowth()
    {   
        bool isActive = !shapeGrowth_UI.activeSelf;
        shapeGrowth_UI.SetActive(isActive);
    }

    public void UpdateUIOnLevelUp(LevelSystem levelSystem)
    {
        level_Text.text = $"LV : {levelSystem.Level}";
        UIManager.Instance.SwitchUI(UIManager.Instance.agumentView.augument_Panel);
        GameManager.Instance.SwitchGame();
    }

    public void UpdateUIOnWeaponLevelUp(int lv)
    {
        weaponLv_Text.text = $"{lv}";
        if(lv < 5) weaponLv_Text.color = Color.white;
        else if(lv < 10) weaponLv_Text.color = Color.yellow;
        else if(lv < 15) weaponLv_Text.color = Color.green;
        else if(lv < 20) weaponLv_Text.color = Color.magenta;
        else if(lv < 25) weaponLv_Text.color = Color.red;
    }
}
