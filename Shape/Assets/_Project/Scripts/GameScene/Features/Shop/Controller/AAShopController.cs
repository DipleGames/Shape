using UnityEngine;
using System;

public class AAShopController : Shop
{
    [Header("Model")]
    [SerializeField] private AAShop _aaShop;

    [Header("View")]
    [SerializeField] private ShopView _shopView;

    public event Action<int> OnRangeUpgradeCostChanged;
    public event Action<int> OnPenetrationUpgradeCostChanged;

    void Start()
    {
        OnRangeUpgradeCostChanged += UIManager.Instance.shopView.OnUpdateRangeCostText;
        OnPenetrationUpgradeCostChanged += UIManager.Instance.shopView.OnUpdatePenetrationCostText;

        //처음 초기화
        OnRangeUpgradeCostChanged.Invoke(_aaShop.rangeUpgradeCost); 
        OnPenetrationUpgradeCostChanged.Invoke(_aaShop.penetrationUpgradeCost);
    }


    public override void Interact()
    {
        if(isInteract)
        {
            _shopView.SwitchShopUI(_shopView.aaShopUI, _shopView.aaShopUI.activeSelf);
        }
    }

    public void OnClikedUpgradeRangeBtn()
    {
        if(CoinManager.Instance.Coin < _aaShop.rangeUpgradeCost) return;
        _aaShop.UpgradeRange();
        CoinManager.Instance.SubtractCoin(_aaShop.rangeUpgradeCost);
        _aaShop.rangeUpgradeCost += 50;
        OnRangeUpgradeCostChanged.Invoke(_aaShop.rangeUpgradeCost);
        AudioManager.Instance.PlayUpgradeSFX(); // 효과음 재생
    }

    public void OnClikedUpgradePenetrationBtn()
    {
        if(CoinManager.Instance.Coin < _aaShop.penetrationUpgradeCost) return;
        _aaShop.UpgradePenetration();
        CoinManager.Instance.SubtractCoin(_aaShop.penetrationUpgradeCost);
        _aaShop.penetrationUpgradeCost += 50;
        OnPenetrationUpgradeCostChanged.Invoke(_aaShop.penetrationUpgradeCost);
        AudioManager.Instance.PlayUpgradeSFX(); // 효과음 재생
    }
    
}
