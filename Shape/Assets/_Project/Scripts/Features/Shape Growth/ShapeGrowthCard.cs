using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.UI;

public class ShapeGrowthCard : MonoBehaviour
{
    public Slider slider;
    public Button upBtn;
    public Button downBtn;

    public StatType statType;

    private void Start()
    {
        upBtn.onClick.AddListener(UpBtn);
        downBtn.onClick.AddListener(DownBtn);
    }


    public void UpBtn()
    {
        ShapeGrowthManager.Instance.shapeGrowth.OnShapeGrowth(statType, this);
    }

    public void DownBtn()
    {
        ShapeGrowthManager.Instance.shapeGrowth.RevertShapeGrowth(statType, this);
    }
}
