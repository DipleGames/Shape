using UnityEngine;

public class AreaFollower : MonoBehaviour
{
    [SerializeField] private Transform player;

    void Start()
    {
        player = PlayerManager.Instance.player.transform;
    }

    void LateUpdate()
    {
        if (player == null) return;

        transform.position = player.position;
        transform.rotation = Quaternion.identity; // 항상 회전 0 유지
    }
}

