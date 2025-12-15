using UnityEngine;
using System.Collections;
using System;


[CreateAssetMenu(menuName = "Boss/Patterns/Spawn")]
public class SpawnPattern : BossPattern
{
    [Serializable]
    public class SpawnParams : PatternParams
    {
        public GameObject bossMinionPrefab;
        public int count = 2;
    }

    public override Type ParamsType => typeof(SpawnParams);

    public override PatternParams CreatePatternParams() => new SpawnParams();

    public override IEnumerator ExecutePattern(BossController boss, PatternParams p)
    {
        var prm = (SpawnParams)p;
        Instantiate(prm.bossMinionPrefab, boss.transform.position + new Vector3(-3f, -3f, 0), Quaternion.identity);
        Instantiate(prm.bossMinionPrefab, boss.transform.position + new Vector3(3f, -3f, 0), Quaternion.identity);
        yield return null;
    }

}
