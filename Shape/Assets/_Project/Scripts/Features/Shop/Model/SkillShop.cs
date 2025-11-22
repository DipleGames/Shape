using UnityEngine;

public class SkillShop : MonoBehaviour
{
    [SerializeField] private SkillUpgradeBtn[] _skillUpgradeBtns;
    public void UpgradeQSkill()
    {
        Debug.Log($"{_skillUpgradeBtns[0].skill.name}");
        foreach (var entry in _skillUpgradeBtns[0].skill.actions)
        {
            if (entry != null && entry.action is DamageAction)
            {
                var prm = (DamageAction.DamageParams)entry.parameters;
                prm.damage *= (1f + 0.05f);  
                Debug.Log($"{prm.damage}");
            }
        }
    }

    public void UpgradeWSkill()
    {
        Debug.Log($"{_skillUpgradeBtns[1].skill.name}");
        foreach (var entry in _skillUpgradeBtns[1].skill.actions)
        {
            if (entry != null && entry.action is DamageAction)
            {
                var prm = (DamageAction.DamageParams)entry.parameters;
                prm.damage *= (1f + 0.05f);
                Debug.Log($"{prm.damage}");
            }
        }
    }

    public void UpgradeESkill()
    {
        Debug.Log($"{_skillUpgradeBtns[2].skill.name}");
        foreach (var entry in _skillUpgradeBtns[2].skill.actions)
        {
            if (entry != null && entry.action is DamageAction)
            {
                var prm = (DamageAction.DamageParams)entry.parameters;
                prm.damage *= (1f + 0.05f);
                Debug.Log($"{prm.damage}");
            }
        }
    }

    public void UpgradeRSkill()
    {
        Debug.Log($"{_skillUpgradeBtns[3].skill.name}");
        foreach (var entry in _skillUpgradeBtns[3].skill.actions)
        {
            if (entry != null && entry.action is DamageAction)
            {
                var prm = (DamageAction.DamageParams)entry.parameters;
                prm.damage *= (1f + 0.05f);
                Debug.Log($"{prm.damage}");
            }
        }
    }
}
