using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    public Slider slider;    // Min 0, Max 1
    public Skill skill;
    public bool isCooldown = false;

    void Start()
    {
        slider = GetComponentInChildren<Slider>();
    }
}
