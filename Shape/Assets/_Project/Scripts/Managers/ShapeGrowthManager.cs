using UnityEngine;

public class ShapeGrowthManager : SingleTon<ShapeGrowthManager>
{
    [Header("Model")]
    public ShapeGrowth shapeGrowth;

   void Start()
    {
        shapeGrowth.shapeGrowthDic[StatType.MaxHp] = 0;
        shapeGrowth.shapeGrowthDic[StatType.MaxMp] = 0;
        shapeGrowth.shapeGrowthDic[StatType.MaxStamina] = 0;
        shapeGrowth.shapeGrowthDic[StatType.Attack] = 0;
        shapeGrowth.shapeGrowthDic[StatType.Speed] = 0;
        shapeGrowth.shapeGrowthDic[StatType.CriticalProb] = 0;
        shapeGrowth.shapeGrowthDic[StatType.CriticalValue] = 0;
        shapeGrowth.shapeGrowthDic[StatType.DrainArea] = 0;
    }

}
