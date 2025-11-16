using UnityEngine;
using System;
using UnityEngine.Tilemaps;
public enum GameState { General, Boss, GameOver }

public class GameManager : SingleTon<GameManager>
{
    [Header("게임 상태")]
    public GameState gameState = GameState.General;

    [Header("분노 게이지")]
    [SerializeField] private float _maxThreatGuage = 100f;
    public float MaxThreatGuage => _maxThreatGuage;
    [SerializeField] private float _threatGuage = 0f;
    public event Action<float> OnThreatGuageChanged; // 게이지 변화할때 발생하는 이벤트

    [Header("그라운드 타일맵 렌더러")]
    [SerializeField] private TilemapRenderer[] _groundTileMapRenderers;

    [Header("보스방 타일맵")]
    [SerializeField] private GameObject _bossRoomGird;

    void Start()
    {
        OnThreatGuageChanged += UIManager.Instance.threatGaugeView.OnUpdateThreatGauge;   
    }

    public float ThreatGuage
    {
        get => _threatGuage;
        set
        {
            float nv = Mathf.Clamp(value, 0f, _maxThreatGuage);
            _threatGuage = nv;
            OnThreatGuageChanged?.Invoke(_threatGuage);
            if (_threatGuage >= _maxThreatGuage) 
            {
                OnBossPhase();
            }
        }
    }

    void Update()
    {
        if(Input.GetMouseButton(0) && gameState == GameState.Boss)
        {
            OnGeneralPhase();
        }
    }

    public void SwitchGame()
    {
        Time.timeScale =Time.timeScale == 0f ? 1f : 0f;
    }

    public void IncreaseThreatGuage(float amount)
    {
        ThreatGuage += amount;
    }

    public void OnBossPhase()
    {
        Debug.Log("분노게이지 맥스 보스스테이지 입장");
        gameState = GameState.Boss;
        foreach(var gtmr in _groundTileMapRenderers)
        {
            gtmr.enabled = false;
        }
        PoolManager.Instance.enemyPools[0].ReturnAllEnemies();
        _bossRoomGird.transform.position = PlayerManager.Instance.player.transform.position;
        _bossRoomGird.SetActive(true);
        ThreatGuage = 0f;
    }

    public void OnGeneralPhase()
    {
        Debug.Log("일반 스테이지");
        gameState = GameState.General;
        _bossRoomGird.SetActive(false);
        foreach(var gtmr in _groundTileMapRenderers)
        {
            gtmr.enabled = true;
        }
    }
}
