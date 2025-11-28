using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Agument", menuName = "Aguments/SpecialAgument")]
public class SpecialAgument : ScriptableObject
{
    public string agumentName;
    public string agumentDesc;

    public void ExecuteSpecialAgument()
    {
        switch(agumentName)
        {
            case "마나 젠":
                PlayerManager.Instance.playerController.manaRegen += 30f;
                break;
            case "스테미너 젠":
                PlayerManager.Instance.playerController.staminaRegen += 30f;
                break;
            case "Q쿨타임 감소":
                PlayerManager.Instance.character.Q_SkillInstance.cooldown *= 0.8f;
                break;
            case "W쿨타임 감소":
                PlayerManager.Instance.character.W_SkillInstance.cooldown *= 0.8f;
                break;
            case "E쿨타임 감소":
                PlayerManager.Instance.character.E_SkillInstance.cooldown *= 0.8f;
                break;
            case "R쿨타임 감소":
                PlayerManager.Instance.character.R_SkillInstance.cooldown *= 0.8f;
                break;

        }
    }
}
