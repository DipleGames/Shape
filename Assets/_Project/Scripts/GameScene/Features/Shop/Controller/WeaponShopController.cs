using UnityEngine;
using System;
public class WeaponShopController : Shop
{
    [Header("Model")]
    [SerializeField] private WeaponShop _weaponShop;

    [Header("View")]
    [SerializeField] private ShopView _shopView;

    public event Action<int> OnWeaponUpgradeCostChanged;

    void Start()
    {
        OnWeaponUpgradeCostChanged += UIManager.Instance.shopView.OnUpdateWeaponCostText;
        OnWeaponUpgradeCostChanged.Invoke(_weaponShop.weaponUpgradeCost); //처음 초기화
    }

    public override void Interact()
    {
        if(isInteract)
        {
            _shopView.SwitchShopUI(_shopView.weaponShopUI, _shopView.weaponShopUI.activeSelf);
        }
    }
    
    public void OnClickedUpgradeBtn() // 업그레이드 버튼을 누르는 인풋 -> 모델로 전달
    {
        if(CoinManager.Instance.Coin < _weaponShop.weaponUpgradeCost) return;

        _weaponShop.UpgradeWeapon(PlayerManager.Instance);
        CoinManager.Instance.SubtractCoin(_weaponShop.weaponUpgradeCost);
        _weaponShop.weaponUpgradeCost += 10;
        OnWeaponUpgradeCostChanged.Invoke(_weaponShop.weaponUpgradeCost);
        AudioManager.Instance.PlayUpgradeSFX(); // 효과음 재생
    }
}
