using UnityEngine;
using System;
using System.Collections;

public abstract class SkillAction : ScriptableObject
{
    // 이 액션이 요구하는 파라미터 타입을 알려줌
    public abstract Type ParamsType { get; }

    // 파라미터 기본값(스킬에 붙일 때 자동 생성)
    public abstract ActionParams CreateDefaultParams();
    public abstract IEnumerator Execute(SkillContext ctx, ActionParams p);
}

// 파라미터 베이스 (다형 직렬화를 위해 class)
[Serializable]
public abstract class ActionParams { }
