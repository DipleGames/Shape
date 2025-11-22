using UnityEngine;

public class WeaponShop : MonoBehaviour
{
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
        }
    }

}
