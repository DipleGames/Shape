using UnityEngine;

public class EnemyManager : SingleTon<EnemyManager>
{
    [Header("적 목록")]
    public Enemy[] enemyList;

    public Enemy SelectEnemy()
    {
        int stage = GameManager.Instance.Stage;

        return enemyList[stage - 1];
    } 
}
