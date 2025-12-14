using UnityEngine;

public class AAShop : MonoBehaviour
{
    public int rangeUpgradeCost = 50;
    public int penetrationUpgradeCost = 50;

    public int rangeUgCount = 1;
    public int penetrationUgCount = 1;
    public void UpgradeRange() // 사정거리를 5%씩 증가시킴. 라이프타임을 5퍼증가시키면 자연스럽게 거리도 5퍼 증가될것.
    {
        rangeUgCount++;
    }

    public void UpgradePenetration()
    {
        penetrationUgCount++;
    }
    
}
