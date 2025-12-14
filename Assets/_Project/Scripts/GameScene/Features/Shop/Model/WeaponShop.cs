using System;
using UnityEngine;

public class WeaponShop : MonoBehaviour
{
    public int weaponUpgradeCost = 5;
    public event Action<int> OnWeaponLevelChanged;

    void Start()
    {
        OnWeaponLevelChanged += UIManager.Instance.playerView.UpdateUIOnWeaponLevelUp;
    }
    public void UpgradeWeapon(PlayerManager playerManager)
    {
        var weapon = playerManager.character.weaponInstance;

        if (weapon is RotateWeapon rotateWeapon)
        {
            // RotateWeapon 전용 강화 로직
            rotateWeapon.weaponLevel++;
            if(rotateWeapon.weaponLevel%5 == 0) rotateWeapon.count++;
            rotateWeapon.weaponDamage += 3;

            rotateWeapon.InitWeapon(PlayerManager.Instance.player);
            OnWeaponLevelChanged.Invoke(rotateWeapon.weaponLevel);
        }
    }

}
