using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

[CreateAssetMenu(menuName = "Boss/Patterns/Burst")]
public class BurstPattern : BossPattern
{
    [Serializable]
    public class BurstParams : PatternParams
    {
        public GameObject burstPrefab;
        public GameObject burstChargePrefab;
        public float damage;
        public float radius;
        public float duration;
    }

    public override Type ParamsType => typeof(BurstParams);

    public override PatternParams CreatePatternParams() => new BurstParams();

    public override IEnumerator ExecutePattern(BossController boss, PatternParams p)
    {
        var prm = (BurstParams)p;

        // 프리팹 생성 (플레이어 위치 기준)
        Vector3 spawnPos = boss.target.transform.position;
        GameObject burstObj = Instantiate(prm.burstPrefab, spawnPos, Quaternion.identity); // 버스트 존 생성
        BossBurst bossBurst = burstObj.GetComponent<BossBurst>();
        CircleCollider2D cc = burstObj.GetComponent<CircleCollider2D>();
        bossBurst.InitBurst(prm.damage, prm.duration);

        GameObject chargeObj = Instantiate(prm.burstChargePrefab, spawnPos, Quaternion.identity); // 차지 존 생성
        Transform chargeObjTr = chargeObj.transform;

        // 초기 scale = 0
        chargeObjTr.localScale = Vector3.zero;

        float t = 0f;

        // 0 → 1 로 스케일 증가
        while (t < prm.duration)
        {
            t += Time.deltaTime;

            float normalized = t / prm.duration;  // 0~1

            // 스케일 보간
            float scale = Mathf.Lerp(0f, prm.radius, normalized);
            chargeObjTr.localScale = new Vector3(scale, scale, 1f);

            yield return null;
        }

        // 스케일 최대치 보정
        chargeObjTr.localScale = new Vector3(prm.radius, prm.radius, 1f);
        cc.enabled = true;

        yield return new WaitForSeconds(0.1f);
        Destroy(burstObj);
        Destroy(chargeObj);
    }

}
