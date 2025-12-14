using UnityEngine;
using System;
using UnityEngine.Tilemaps;
using System.Collections;
using UnityEngine.SceneManagement;

public enum GameState { General, Boss, Prepare, GameOver } // 제네럴 -> 보스 -> 프리페어 -> 제네럴

public class GameManager : SingleTon<GameManager>
{
    [Header("게임 상태")]
    public GameState gameState = GameState.General;
    [SerializeField] private int _stage = 1;
    public int Stage => _stage;

    [Header("분노 게이지")]
    [SerializeField] private float _maxThreatGuage = 100f;
    public float MaxThreatGuage => _maxThreatGuage;
    [SerializeField] private float _threatGuage = 0f;
    public event Action<float> OnThreatGuageChanged; // 게이지 변화할때 발생하는 이벤트

    [Header("그라운드 타일맵 렌더러")]
    [SerializeField] private GameObject _groundGrid;
    [SerializeField] private GameObject[] _groundTileMap;

    [Header("보스방 타일맵 그리드")]
    [SerializeField] private GameObject _bossRoomGrid;

    [Header("상점맵 타일맵 그리드")]
    [SerializeField] private GameObject _shopGrid;

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
                UIManager.Instance.GetComponent<BossEntryDirector>().OnRageFull();
            }
        }
    }

    /// <summary>
    /// 페이즈 전환 메서드 
    /// 제네럴 / 보스 / 프리페어
    /// </summary>
    public void OnGeneralPhase()
    {
        ClearShopUI();

        Debug.Log("일반 스테이지");
        gameState = GameState.General;
        if(Time.timeScale == 0f) SwitchGame();
        _shopGrid.SetActive(false);
        foreach(var gtm in _groundTileMap)
        {
            gtm.GetComponent<TilemapRenderer>().enabled = true;
        }
        AudioManager.Instance.PlayGeneralBGM();
    }

    public void OnBossPhase()
    {
        Debug.Log("분노게이지 맥스 보스스테이지 입장");
        gameState = GameState.Boss;
        foreach(var gtm in _groundTileMap)
        {
            gtm.GetComponent<TilemapRenderer>().enabled = false;
        }
        ClearItem();
        PoolManager.Instance.enemyPools[0].ReturnAllEnemies(); // 적 다 리턴시키고
        _bossRoomGrid.transform.position = new Vector3(PlayerManager.Instance.player.transform.position.x, PlayerManager.Instance.player.transform.position.y + 6f, 0); // 보스방의 위치를 현재 플레이어의 위치로 위치시키고
        _bossRoomGrid.SetActive(true); // 보스방을 키고
        UIManager.Instance.GetComponent<BossEntryDirector>().PlayBossRoomExit(PlayerManager.Instance.player.transform.position);
        SpawnManager.Instance.SpawnBoss();
        AudioManager.Instance.PlayBossBGM();
        ThreatGuage = 0f;
    }

    public IEnumerator OnPreparePhase()
    {
        ClearBossPatterns();
        
        Debug.Log("준비 스테이지");
        gameState = GameState.Prepare;
        _stage++;

        _bossRoomGrid.SetActive(false);
        _shopGrid.SetActive(true);
        _shopGrid.transform.position = new Vector3(PlayerManager.Instance.player.transform.position.x - 0.5f, PlayerManager.Instance.player.transform.position.y, 0); 
        
        yield return new WaitForSecondsRealtime(15f);
        OnGeneralPhase();
    }

    public void StartPreparePhase()
    {
        StartCoroutine(OnPreparePhase());
    }

    public void ClearBossPatterns()
    {
        var objs = GameObject.FindGameObjectsWithTag("BossPattern");
        foreach (var obj in objs)
            Destroy(obj);
    }

    public void ClearItem()
    {
        var healObjs = GameObject.FindGameObjectsWithTag("Hp");
        foreach (var obj in healObjs)
            ItemManager.Instance.ReturnHeal(obj);

        var expObjs = GameObject.FindGameObjectsWithTag("Exp");
        foreach (var obj in expObjs)
            ItemManager.Instance.ReturnExp(obj);

        var shapePieceObjs = GameObject.FindGameObjectsWithTag("ShapePiece");
        foreach (var obj in shapePieceObjs)
            ItemManager.Instance.ReturnShapePiece(obj);
    }

    void ClearShopUI()
    {
        var objs = GameObject.FindGameObjectsWithTag("ShopUI");
        foreach (var obj in objs)
            obj.SetActive(false);
    }
    
    public void SwitchGame()
    {
        Time.timeScale =Time.timeScale == 0f ? 1f : 0f;
    }

    public void IncreaseThreatGuage(float amount)
    {
        ThreatGuage += amount;
    }

    public void OnEnding()
    {
        SceneManager.LoadScene("EndingScene");
    }

    public IEnumerator OnGameOver()
    {
        UIManager.Instance.SwitchUI(UIManager.Instance.gameover_Panel);

        yield return new WaitForSeconds(3f);
        UIManager.Instance.SwitchUI(UIManager.Instance.gameover_Panel);
        SceneManager.LoadScene("LobbyScene");
    }
}
