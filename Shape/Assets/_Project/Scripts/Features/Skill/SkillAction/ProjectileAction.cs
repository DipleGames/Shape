using UnityEngine;
using System.Collections;
using System;
using Unity.Collections;

[CreateAssetMenu(menuName = "Skill/Actions/Projectile")]
public class ProjectileAction : SkillAction
{
    [Serializable]
    public class ProjectileParams : ActionParams
    {
        public GameObject projectilePrefab;
        public float speed = 0f;
        public float lifeTime = 0f;
        public bool isPenetrate = false;
    }

    public override Type ParamsType => typeof(ProjectileParams);

    public override ActionParams CreateDefaultParams() => new ProjectileParams();

    public override IEnumerator Execute(SkillContext ctx, ActionParams p)
    {
        var prm = (ProjectileParams)p;

        // 투사체 생성
        var proj = Instantiate(prm.projectilePrefab, ctx.castOrigin, Quaternion.identity);
        SkillProj skillProj= proj.GetComponent<SkillProj>();
        skillProj.Init(ctx, prm.speed, prm.isPenetrate);
        yield return new WaitForSeconds(prm.lifeTime);
        Destroy(proj);
    }
}
