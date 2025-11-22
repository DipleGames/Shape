using UnityEngine;

public class SkillShopController : Shop
{
    [Header("Model")]
    [SerializeField] private SkillShop _skillShop;

    [Header("View")]
    [SerializeField] private ShopView _shopView;

    public override void Interact()
    {
        if(isInteract)
        {
            _shopView.SwitchShopUI(_shopView.skillShopUI, _shopView.skillShopUI.activeSelf);
        }
    }
    
    public void OnClickedUpgradeQBtn() // 업그레이드 버튼을 누르는 인풋 -> 모델로 전달
    {
        _skillShop.UpgradeQSkill();
    }

    public void OnClickedUpgradeWBtn() // 업그레이드 버튼을 누르는 인풋 -> 모델로 전달
    {
        _skillShop.UpgradeWSkill();
    }

    public void OnClickedUpgradeEBtn() // 업그레이드 버튼을 누르는 인풋 -> 모델로 전달
    {
        _skillShop.UpgradeESkill();
    }

    public void OnClickedUpgradeRBtn() // 업그레이드 버튼을 누르는 인풋 -> 모델로 전달
    {
        _skillShop.UpgradeRSkill();
    }
}
