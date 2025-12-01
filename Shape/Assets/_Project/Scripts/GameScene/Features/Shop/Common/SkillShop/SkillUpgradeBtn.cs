using UnityEngine;
using UnityEngine.UI;

public class SkillUpgradeBtn : MonoBehaviour
{
    public Skill skill;
    public Image skill_Img;

    public void InitSkillUpgradeBtn()
    {
        skill_Img.sprite = skill.skillDefinition.skillIcon;
    }
}
