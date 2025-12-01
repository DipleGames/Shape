using UnityEngine;
using System;
public class SkillShopController : Shop
{
    [Header("Model")]
    [SerializeField] private SkillShop _skillShop;

    [Header("View")]
    [SerializeField] private ShopView _shopView;

    [SerializeField] private Skill _selectSkill;

    public event Action<int> OnSkillUpgradeCostChanged;

    void Start()
    {
        OnSkillUpgradeCostChanged += UIManager.Instance.shopView.OnUpdateSkillCostText;
        for(int i=0; i<5; i++)
        {
            OnSkillUpgradeCostChanged.Invoke(_skillShop.skillUpgradeCosts[i]); //처음 초기화
        }
    }

    public override void Interact()
    {
        if(isInteract)
        {
            _shopView.SwitchShopUI(_shopView.skillShopUI, _shopView.skillShopUI.activeSelf);
        }
    }

    private void SelectSkillByIndex(int index)
    {
        _selectSkill = _skillShop.skillUpgradeBtns[index].skill;
        _shopView.OnSelectSkill(_selectSkill);
        OnSkillUpgradeCostChanged.Invoke(_skillShop.skillUpgradeCosts[index]);
        //AudioManager.Instance.PlaySelectSFX();
    }

    
    public void OnClickedQBtn() // 버튼을 누르는 인풋 -> 모델로 전달
    {
        SelectSkillByIndex(0);
    }

    public void OnClickedWBtn() // 업그레이드 버튼을 누르는 인풋 -> 모델로 전달
    {
        SelectSkillByIndex(1);
    }

    public void OnClickedEBtn() // 업그레이드 버튼을 누르는 인풋 -> 모델로 전달
    {
        SelectSkillByIndex(2);
    }

    public void OnClickedRBtn() // 업그레이드 버튼을 누르는 인풋 -> 모델로 전달
    {
        SelectSkillByIndex(3);
    }

    public void OnClickedDBtn() // 업그레이드 버튼을 누르는 인풋 -> 모델로 전달
    {
        SelectSkillByIndex(4);
    }

    public void OnClickedUpgradeBtn()
    {
        int idx = -1;
        if(_selectSkill == _skillShop.skillUpgradeBtns[0].skill)
        {
            idx = 0;
            if(CoinManager.Instance.Coin < _skillShop.skillUpgradeCosts[idx]) return;
            _skillShop.UpgradeQSkill();
            CoinManager.Instance.SubtractCoin(_skillShop.skillUpgradeCosts[idx]);
            _skillShop.skillUpgradeCosts[idx] += 10;
            OnSkillUpgradeCostChanged.Invoke(_skillShop.skillUpgradeCosts[idx]);
        }
        else if(_selectSkill == _skillShop.skillUpgradeBtns[1].skill)
        {
            idx = 1;
            if(CoinManager.Instance.Coin < _skillShop.skillUpgradeCosts[idx]) return;
            _skillShop.UpgradeWSkill();
            CoinManager.Instance.SubtractCoin(_skillShop.skillUpgradeCosts[idx]);
            _skillShop.skillUpgradeCosts[idx] += 20;
            OnSkillUpgradeCostChanged.Invoke(_skillShop.skillUpgradeCosts[idx]);
        }
        else if(_selectSkill == _skillShop.skillUpgradeBtns[2].skill)
        {
            idx = 2;
            if(CoinManager.Instance.Coin < _skillShop.skillUpgradeCosts[idx]) return;
            _skillShop.UpgradeESkill();
            CoinManager.Instance.SubtractCoin(_skillShop.skillUpgradeCosts[idx]);
            _skillShop.skillUpgradeCosts[idx] += 20;
            OnSkillUpgradeCostChanged.Invoke(_skillShop.skillUpgradeCosts[idx]);
        }
        else if(_selectSkill == _skillShop.skillUpgradeBtns[3].skill)
        {
            idx = 3;
            if(CoinManager.Instance.Coin < _skillShop.skillUpgradeCosts[idx]) return;
            _skillShop.UpgradeRSkill();
            CoinManager.Instance.SubtractCoin(_skillShop.skillUpgradeCosts[idx]);
            _skillShop.skillUpgradeCosts[idx] += 50;
            OnSkillUpgradeCostChanged.Invoke(_skillShop.skillUpgradeCosts[idx]);
        }
        else if(_selectSkill == _skillShop.skillUpgradeBtns[4].skill)
        {
            idx = 4;
        }

        AudioManager.Instance.PlayUpgradeSFX(); // 효과음 재생
    }
}
