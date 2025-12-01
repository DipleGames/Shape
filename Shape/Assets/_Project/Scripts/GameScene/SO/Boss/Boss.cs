using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Boss", menuName = "Boss/Boss")]
public class Boss : ScriptableObject
{
    public Sprite sprite;
    public float bossHp;

    [Serializable]
    public class PatternEntry
    {
        public BossPattern pattern;                           // 어떤 액션을 쓸지
        [SerializeReference] public PatternParams parameters; // 액션별 파라미터(다형 직렬화)
    }

    public PatternEntry[] patterns;                            // 리스트/배열 타입이 이 엔트리여야 함

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (patterns == null) return;

        foreach (var p in patterns)
        {
            if (p == null || p.pattern == null) continue;

            var pType = p.pattern.ParamsType;
            if (p.parameters == null || p.parameters.GetType() != pType)
            {
                p.parameters = p.pattern.CreatePatternParams();    // 자동 생성
                UnityEditor.EditorUtility.SetDirty(this);
            }
        }
    }
#endif
}
