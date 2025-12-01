using UnityEngine;

public class PrepareManager : SingleTon<PrepareManager>
{
    [Header("Shop(Model) 목록")]
    public WeaponShop weaponShop;
    public SkillShop skillShop;
    public AAShop aaShop; 
}
