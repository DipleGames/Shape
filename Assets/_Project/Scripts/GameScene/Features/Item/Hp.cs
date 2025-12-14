using UnityEngine;

public class Hp : Item
{
    [SerializeField] private float _value;
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log($"hp {_value}만큼 획득");
            PlayerManager.Instance.playerController.Hp += _value;
            isDrainArea = false;
            ItemManager.Instance.ReturnHeal(gameObject);
        }
    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();    
    }
}
