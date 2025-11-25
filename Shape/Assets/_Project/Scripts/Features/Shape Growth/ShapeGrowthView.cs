using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShapeGrowthView : MonoBehaviour
{
    public GameObject shapeGrowthUI;
    public TextMeshProUGUI shapePoint_Text;

    public void OnUpdateShapeGrowthUI(ShapeGrowthCard shapeGrowthCard)
    {
        shapeGrowthCard.slider.value = ShapeGrowthManager.Instance.shapeGrowth.shapeGrowthDic[shapeGrowthCard.statType] / 5f;
        OnUpdateShapeGrowthText();
    }

    public void OnUpdateShapeGrowthText()
    {
        shapePoint_Text.text = $"SP:{ShapeGrowthManager.Instance.shapeGrowth.currentShapePoint}";
    }
}
