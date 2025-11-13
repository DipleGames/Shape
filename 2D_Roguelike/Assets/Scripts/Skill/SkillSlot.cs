using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    [Header("스킬 객체")]
    public SkillInstance skillInstance;
    
    [Header("스킬 정보 SO")]
    public Skill skill;
    public Image icon;


    public void InitSkill()
    {
        skillInstance = new SkillInstance { skill = skill };
        icon = GetComponentInChildren<Image>();
        icon.sprite = skill.skillIcon;
    }
}
