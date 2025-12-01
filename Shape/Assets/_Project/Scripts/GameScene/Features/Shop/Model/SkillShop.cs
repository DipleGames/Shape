using UnityEngine;

public class SkillShop : MonoBehaviour
{
    public SkillUpgradeBtn[] skillUpgradeBtns;
    public int[] skillUpgradeCosts = { 10, 20, 20, 50, 10 };
    public void UpgradeQSkill()
    {
        Debug.Log($"{skillUpgradeBtns[0].skill.name}");
        foreach (var entry in skillUpgradeBtns[0].skill.actions)
        {
            if (entry != null && entry.action is DamageAction)
            {
                var prm = (DamageAction.DamageParams)entry.parameters;
                prm.damage = prm.damage * (1f + 0.05f);  
                Debug.Log($"{prm.damage}");
            }
        }
    }

    public void UpgradeWSkill()
    {
        Debug.Log($"{skillUpgradeBtns[1].skill.name}");
        foreach (var entry in skillUpgradeBtns[1].skill.actions)
        {
            if (entry != null && entry.action is DamageAction)
            {
                var prm = (DamageAction.DamageParams)entry.parameters;
                prm.damage = prm.damage * (1f + 0.05f);  
                Debug.Log($"{prm.damage}");
            }
        }
    }

    public void UpgradeESkill()
    {
        Debug.Log($"{skillUpgradeBtns[2].skill.name}");
        foreach (var entry in skillUpgradeBtns[2].skill.actions)
        {
            if (entry != null && entry.action is DamageAction)
            {
                var prm = (DamageAction.DamageParams)entry.parameters;
                prm.damage = prm.damage * (1f + 0.05f);  
                Debug.Log($"{prm.damage}");
            }
        }
    }

    public void UpgradeRSkill()
    {
        Debug.Log($"{skillUpgradeBtns[3].skill.name}");
        foreach (var entry in skillUpgradeBtns[3].skill.actions)
        {
            if (entry != null && entry.action is DamageAction)
            {
                var prm = (DamageAction.DamageParams)entry.parameters;
                prm.damage = prm.damage * (1f + 0.05f);  
                Debug.Log($"{prm.damage}");
            }
        }
    }
}
