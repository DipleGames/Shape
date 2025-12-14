using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopView : MonoBehaviour
{
    [Header("웨폰상점 UI")]
    public GameObject weaponShopUI;

    [Header("스킬상점 UI")]
    public GameObject skillShopUI;
    public Image selectSkill_Img;
    public TextMeshProUGUI selectSkillDesc_Text;
    public TextMeshProUGUI selectSkillUpgradeCost_Text;   

    [Header("AA상점 UI")]
    public GameObject aaShopUI; 

    public void SwitchShopUI(GameObject shopUI, bool activeSelf)
    {
        shopUI.SetActive(!activeSelf);
        GameManager.Instance.SwitchGame();
        if(shopUI == skillShopUI)
        {
            selectSkill_Img.GetComponent<Image>().enabled = false;
            selectSkillDesc_Text.GetComponent<TextMeshProUGUI>().enabled = false;
            selectSkillUpgradeCost_Text.GetComponent<TextMeshProUGUI>().enabled = false;
        }
    }

    /// <summary>
    /// 웨폰상점
    /// </summary>
    [Header("웨폰상점 Cost 텍스트")]
    [SerializeField] private TextMeshProUGUI _weaponUpgradeCost_Text;
    public void OnUpdateWeaponCostText(int cost)
    {
        _weaponUpgradeCost_Text.text = $"{cost}";
    }

    /// <summary>
    /// 스킬상점
    /// </summary>
    public void OnSelectSkill(Skill skill) // 스킬을 선택하면
    {
        selectSkill_Img.GetComponent<Image>().enabled = true;
        selectSkillDesc_Text.GetComponent<TextMeshProUGUI>().enabled = true;
        selectSkillUpgradeCost_Text.GetComponent<TextMeshProUGUI>().enabled = true;
        selectSkill_Img.sprite = skill.skillDefinition.skillIcon;
    }

    [Header("스킬상점 Cost 텍스트")]
    [SerializeField] private TextMeshProUGUI _skillUpgradeCost_Text;
    public void OnUpdateSkillCostText(int cost)
    {
        _skillUpgradeCost_Text.text = $"{cost}";
    }

    /// <summary>
    /// AA상점
    /// </summary>
    [Header("AA상점 Cost 텍스트")]
    [SerializeField] private TextMeshProUGUI _rangeUpgradeCost_Text;
    [SerializeField] private TextMeshProUGUI _penetrationUpgradeCost_Text;
    public void OnUpdateRangeCostText(int cost)
    {
        _rangeUpgradeCost_Text.text = $"{cost}";
    }

    public void OnUpdatePenetrationCostText(int cost)
    {
        _penetrationUpgradeCost_Text.text = $"{cost}";
    }
}
