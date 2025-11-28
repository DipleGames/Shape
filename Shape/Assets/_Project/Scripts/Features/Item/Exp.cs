using UnityEngine;

public class Exp : Item
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log($"경험치 {value}만큼 획득");
            PlayerManager.Instance.levelSystem.AddExp(value);
            isDrainArea = false;
            ItemManager.Instance.ReturnExp(gameObject);
        }
    }
    
    protected override void Update()
    {
        base.Update();
    }
}
