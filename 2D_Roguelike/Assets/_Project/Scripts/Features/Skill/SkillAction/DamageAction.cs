using UnityEngine;
using System.Collections;
using System;

[CreateAssetMenu(menuName = "Skill/Actions/Damage")]
public class DamageAction : SkillAction
{
    [Serializable]
    public class DamageParams : ActionParams
    {
        public float damage;
    }

    public override Type ParamsType => typeof(DamageParams);

    public override ActionParams CreateDefaultParams() => new DamageParams();

    public override IEnumerator Execute(SkillContext ctx, ActionParams p)
    {
        var prm = (DamageParams)p;
        ctx.skillDamage = prm.damage;
        yield break;
    }
}
