using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _mainCoin_Text;

    public void OnUpdateCoinText(int coin)
    {
        int value = coin;
        string formatted = value.ToString("N0"); 
        _mainCoin_Text.text = formatted;
    }

}
