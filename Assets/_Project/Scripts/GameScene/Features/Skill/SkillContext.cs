using UnityEngine;
using System.Collections.Generic;

public class SkillContext
{
    public GameObject caster;         // 스킬을 시전한 주체(플레이어, 몬스터 등)
    public Vector3 castOrigin;        // 발사 원점 (예: 손끝, 중심, 총구 위치 등)
    public Vector3 targetPoint;       // 클릭 위치나 조준점 등 실제 목표 지점

    public float skillDamage;
}