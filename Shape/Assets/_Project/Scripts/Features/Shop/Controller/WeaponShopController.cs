using UnityEngine;

public class WeaponShopController : Shop
{
    [Header("Model")]
    [SerializeField] private WeaponShop _weaponShop;

    [Header("View")]
    [SerializeField] private ShopView _shopView;

    public override void Interact()
    {
        if(isInteract)
        {
            _shopView.SwitchShopUI(_shopView.weaponShopUI, _shopView.weaponShopUI.activeSelf);
        }
    }
    
    public void OnClickedUpgradeBtn() // 업그레이드 버튼을 누르는 인풋 -> 모델로 전달
    {
        _weaponShop.UpgradeWeapon(PlayerManager.Instance);
    }
}
