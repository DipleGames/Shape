using UnityEngine;

public class SkillSystem : MonoBehaviour
{
    [System.Serializable]
    public class SkillModifier // 런타임 강화치(예: 쿨감, 위력 등)
    {
        public float cooldownMult = 1f;
        public float damageMult = 1f;
        // 필요 시 더 추가
    }

    public class QSKill
    {
    }
}
