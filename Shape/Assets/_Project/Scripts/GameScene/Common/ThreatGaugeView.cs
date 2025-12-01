using UnityEngine;
using UnityEngine.UI;

public class ThreatGaugeView : MonoBehaviour
{
    [SerializeField] private Slider _threatGaugeBar;

    public void OnUpdateThreatGauge(float threatGuage)
    {
        _threatGaugeBar.value = threatGuage / GameManager.Instance.MaxThreatGuage;
    }
}
