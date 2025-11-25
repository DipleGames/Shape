using System.Collections.Generic;
using UnityEngine;

public class ShapeGrowth : MonoBehaviour
{
    public Dictionary<StatType, int> shapeGrowthDic = new Dictionary<StatType, int>();
    public int currentShapePoint = 0;
    public int shapePoint = 0;

    public void AddShapePoint(int amount)
    {
        currentShapePoint += amount;
        shapePoint += amount;
        UIManager.Instance.shapeGrowthView.OnUpdateShapeGrowthText();
    }

    public void OnShapeGrowth(StatType statType, ShapeGrowthCard shapeGrowthCard)
    {
        if(shapeGrowthDic[statType] == 5) return;
        if(currentShapePoint == 0) return;
        shapeGrowthDic[statType]++;
        currentShapePoint--;
        UIManager.Instance.shapeGrowthView.OnUpdateShapeGrowthUI(shapeGrowthCard);
        PlayerManager.Instance.statCalculator.Recalculate(shapeGrowthDic);
    }

    public void RevertShapeGrowth(StatType statType, ShapeGrowthCard shapeGrowthCard)
    {
        if(shapeGrowthDic[statType] == 0) return;
        if(currentShapePoint == shapePoint) return;
        shapeGrowthDic[statType]--;
        currentShapePoint++;
        UIManager.Instance.shapeGrowthView.OnUpdateShapeGrowthUI(shapeGrowthCard);
        PlayerManager.Instance.statCalculator.Recalculate(shapeGrowthDic);
    }
}
