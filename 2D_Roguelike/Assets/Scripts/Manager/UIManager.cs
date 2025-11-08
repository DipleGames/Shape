using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class UIManager : SingleTon<UIManager>
{

    [Header("Status Slider")]
    public Slider hpBar;
    public Slider mpBar;
    public Slider expBar;

    [Header("레벨 관련")]
    public Text level_Text;

    [Header("증강 관련")]
    public GameObject augument_Panel;

    [Header("스탯 관련")]
    public GameObject stat_panel;
    public GameObject statText_panel;
    [SerializeField] private List<Text> statTexts = new List<Text>();


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
                break;
            default:
                break;
        }
    }

    public void UpdateUIOnChangePlayerStat(Dictionary<StatType, float> stat)
    {
        if (statTexts.Count != stat.Count)
        {
            for (int i = 0; i < stat.Count; i++)
            {
                statTexts.Add(statText_panel.transform.GetChild(i).GetComponent<Text>());
            }
            PlayerManager pm = PlayerManager.Instance;
            for (int i = 0; i < pm.playerStat.StatList.Count; i++)
            {
                statTexts[i].text = $"[{pm.playerStat.StatList[i].type}] : {pm.playerStat.StatList[i].value}";
            }
        }
        else
        {
            PlayerManager pm = PlayerManager.Instance;
            for (int i = 0; i < pm.playerStat.StatList.Count; i++)
            {
                statTexts[i].text = $"[{pm.playerStat.StatList[i].type}] : {pm.playerStat.StatList[i].value}";
            }
        }
    }

    void OnToggleStat()
    {
        bool isActive = !stat_panel.activeSelf;
        stat_panel.SetActive(isActive);
    }

    public void UpdateUIOnLevelUp(LevelSystem levelSystem)
    {
        level_Text.text = $"LV : {levelSystem.Level}";
        UpdateAgumentBtnUI();
        SwitchUI(augument_Panel);
        GameManager.Instance.SwitchGame();
    }

    void UpdateAgumentBtnUI()
    {
        GameObject[] agumentBtns = AgumentManager.Instance.agumentBtns;
        for(int i=0; i<agumentBtns.Length; i++)
        {
            AgumentData data = agumentBtns[i].GetComponent<AgumentData>();
            Text agumentName = agumentBtns[i].transform.GetChild(0).GetComponent<Text>();
            Text agumentDesc = agumentBtns[i].transform.GetChild(1).GetComponent<Text>();
            Image agumentImg = agumentBtns[i].transform.GetChild(2).GetComponent<Image>();

            agumentName.text = data.agument.agumentName;
            agumentDesc.text = data.agument.agumentDesc;
        }
    }

    public void SwitchUI(GameObject ui)
    {
        bool b = ui.activeSelf ? false : true;
        ui.SetActive(b); 
    }
}
