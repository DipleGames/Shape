using TMPro;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.UI;

public class ShapeGrowthCard : MonoBehaviour
{

    public Button Btn;
    public TextMeshProUGUI shapeGrowth_Text;


    public StatType statType;

    private void Start()
    {
        Btn.onClick.AddListener(OnClickedBtn);
    }


    public void OnClickedBtn()
    {
        ShapeGrowthManager.Instance.shapeGrowth.OnShapeGrowth(statType, this);
    }
}
