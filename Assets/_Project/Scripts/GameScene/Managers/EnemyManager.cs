using UnityEngine;

public class EnemyManager : SingleTon<EnemyManager>
{
    [Header("적 목록")]
    public Enemy[] enemyList;

    protected override void Awake()
    {
        for(int i=0; i<enemyList.Length; i++)
        {
            enemyList[i] = Instantiate(enemyList[i]);
        }
    }

    public Enemy SelectEnemy()
    {
        int stage = GameManager.Instance.Stage;

        return enemyList[stage - 1];
    } 
}
