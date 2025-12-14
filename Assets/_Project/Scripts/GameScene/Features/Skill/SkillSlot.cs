using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    [Header("스킬 객체")]
    public SkillInstance skillInstance;
    
    [Header("스킬 정보 SO")]
    public Skill skill; // 본사본이 들어올거임
    public Image icon;


    [Header("스킬 업그레이드 버튼")]
    [SerializeField] private SkillUpgradeBtn skillUpgradeBtn;

    void Awake()
    {
        icon = GetComponentInChildren<Image>();
    }

    public void InitSkill()
    {
        skillInstance = new SkillInstance { skill = skill };
        skillUpgradeBtn.skill = skillInstance.skill;
        skillUpgradeBtn.InitSkillUpgradeBtn();
        icon.sprite = skill.skillDefinition.skillIcon;
    }
}
