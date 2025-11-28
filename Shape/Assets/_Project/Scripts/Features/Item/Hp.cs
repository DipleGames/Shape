using UnityEngine;

public class Hp : Item
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log($"hp {value}만큼 획득");
            PlayerManager.Instance.playerController.Hp += value;
            isDrainArea = false;
            ItemManager.Instance.ReturnHp(gameObject);
        }
    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();    
    }
}
