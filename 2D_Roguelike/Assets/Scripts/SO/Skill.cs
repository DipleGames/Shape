using UnityEngine;

public enum SkillType { Passive, Active, Utile }

[CreateAssetMenu(fileName = "Skill", menuName = "Skill/Skill")]
public class Skill : ScriptableObject
{
    public SkillType skillType;
    public KeyCode key;
    public string skillName;
    public float cooldown;
}
