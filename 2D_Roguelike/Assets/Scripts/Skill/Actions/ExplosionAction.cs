using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization;

[CreateAssetMenu(menuName = "Skill/Actions/Explosion")]
public class ExplosionAction : SkillAction
{
    [Serializable]
    public class ExplosionParams : ActionParams
    {
        public GameObject explosionPrefab;
        public float lifeTime = 0f;
    }

    public override Type ParamsType => typeof(ExplosionParams);

    public override ActionParams CreateDefaultParams() => new ExplosionParams();

    public override IEnumerator Execute(SkillContext ctx, ActionParams p)
    {
        var prm = (ExplosionParams)p;
        var exGO = Instantiate(prm.explosionPrefab,ctx.targetPoint, Quaternion.identity);
        Explosion ex = exGO.GetComponent<Explosion>();
        ex.Init(ctx);
        yield return new WaitForSeconds(prm.lifeTime);
        Destroy(exGO);
    }
}
