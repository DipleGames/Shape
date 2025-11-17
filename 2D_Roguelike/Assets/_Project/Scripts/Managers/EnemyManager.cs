using UnityEngine;

public class EnemyManager : SingleTon<EnemyManager>
{
    [Header("적 목록")]
    public Enemy[] enemyList;

    public Enemy SelectEnemy()
    {
        int ran = Random.Range(0, enemyList.Length);

        return enemyList[ran];
    } 
}
