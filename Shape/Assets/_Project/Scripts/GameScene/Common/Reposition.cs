using UnityEngine;

public class Reposition : MonoBehaviour
{
    private const float CHUNK_SIZE = 64f;
    private const int GRID_COUNT = 3;

    [SerializeField] private Transform player; // 카메라나 플레이어 Transform 넣기

    void Start()
    {
        player = PlayerManager.Instance.player.transform;
    }

    void LateUpdate()
    {
        if (player == null) return;

        float totalSize = CHUNK_SIZE * GRID_COUNT;      // 64 * 3 = 192
        float halfSpan  = totalSize * 0.5f;             // 96

        Vector3 pos = transform.position;
        Vector3 c   = player.position;

        // X축 래핑
        float dx = c.x - pos.x;
        if (dx > halfSpan)
        {
            // 타일이 너무 왼쪽에 있어서, 오른쪽으로 한 바퀴 밀어줌
            pos.x += totalSize;
        }
        else if (dx < -halfSpan)
        {
            // 타일이 너무 오른쪽에 있어서, 왼쪽으로 한 바퀴 밀어줌
            pos.x -= totalSize;
        }

        // Y축 래핑
        float dy = c.y - pos.y;
        if (dy > halfSpan)
        {
            pos.y += totalSize;
        }
        else if (dy < -halfSpan)
        {
            pos.y -= totalSize;
        }

        transform.position = pos;
    }
}
