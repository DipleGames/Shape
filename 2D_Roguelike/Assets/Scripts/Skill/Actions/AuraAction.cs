using UnityEngine;
using System.Collections;
using System;

[CreateAssetMenu(menuName = "Skill/Actions/Aura")]
public class AuraAction : SkillAction
{
    [Serializable]
    public class AuraParams : ActionParams
    {
        public GameObject auraPrefab;
        public float lifeTime = 0f;
    }

    public override Type ParamsType => typeof(AuraParams);

    public override ActionParams CreateDefaultParams() => new AuraParams();

    public override IEnumerator Execute(SkillContext ctx, ActionParams p)
    {
        var prm = (AuraParams)p;
        var auraGO = Instantiate(prm.auraPrefab,ctx.castOrigin, Quaternion.identity);
        Aura aura = auraGO.GetComponent<Aura>();
        aura.Init(ctx);
        yield return new WaitForSeconds(prm.lifeTime);
        Destroy(auraGO);
    }
}
