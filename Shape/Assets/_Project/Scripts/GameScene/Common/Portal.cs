using UnityEngine;

public class Portal : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {    
            if(GameManager.Instance.Stage < EnemyManager.Instance.enemyList.Length)
                GameManager.Instance.StartPreparePhase(); // 준비페이즈 시작
            else if(GameManager.Instance.Stage == EnemyManager.Instance.enemyList.Length)
                GameManager.Instance.OnGameOver();
            ShapeGrowthManager.Instance.shapeGrowth.AddShapePoint(GameManager.Instance.Stage + 1);
            Destroy(gameObject);
        }
    }
}
