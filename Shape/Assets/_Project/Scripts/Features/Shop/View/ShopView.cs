using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class ShopView : MonoBehaviour
{
    [Header("상점 UI")]
    public GameObject weaponShopUI;
    public GameObject skillShopUI;
    public GameObject aaShopUI; 

    public void SwitchShopUI(GameObject shopUI, bool activeSelf)
    {
        shopUI.SetActive(!activeSelf);
    }
}
