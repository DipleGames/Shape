using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Boss/Patterns/Barrage")]
public class BarragePattern : BossPattern
{
    [Serializable]
    public class BarrageParams : PatternParams
    {
        public GameObject proj;
        public float damage;
        public int count;
        public float lifeTime = 0f;
        public float speed;
        public float spreadAngle;
    }

    public override Type ParamsType => typeof(BarrageParams);

    public override PatternParams CreatePatternParams() => new BarrageParams();

    public override IEnumerator ExecutePattern(BossController boss, PatternParams p)
    {
        var prm = (BarrageParams)p;

        // 1) 보스 → 플레이어 방향
        Vector3 dir = (boss.target.transform.position - boss.transform.position).normalized;
        float baseAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        // 2) 부채꼴 각도 계산
        float spread = prm.spreadAngle;
        float startAngle = baseAngle - (spread * 0.5f);
        float angleStep = (prm.count > 1) ? spread / (prm.count - 1) : 0f;

        // 3) 생성한 총알들 저장할 리스트
        var projs = new List<Transform>();

        // 4) 총알 생성
        for (int i = 0; i < prm.count; i++)
        {
            float angle = startAngle + angleStep * i;
            Quaternion rot = Quaternion.Euler(0f, 0f, angle);
            var go = Instantiate(prm.proj, boss.transform.position, rot);

            Vector3 direction = rot * Vector3.right;   // ← 바로 발사 방향 계산
            BossProj bp = go.GetComponent<BossProj>();
            bp.InitBossProj(prm.lifeTime, prm.speed, prm.damage, direction);
            projs.Add(go.transform);
        }
        yield return null; 
    }
}
