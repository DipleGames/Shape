using System.Collections;
using UnityEngine;

[DefaultExecutionOrder(-100)]// 얘가 젤 먼저 실행되야함
public class PlayerManager : SingleTon<PlayerManager> 
{
    [Header("캐릭터 목록")]
    [SerializeField] private Character[] characterList;


    [Header("세팅된 플레이어 객체 / 컴퍼넌트")]
    public GameObject player;
    public PlayerController playerController;
    public BattleSystem battleSystem;
    public LevelSystem levelSystem; 
    public PlayerStats playerStats;
    public StatCalculator statCalculator;
    public SpriteRenderer spriteRenderer;

    [Header("현재 선택된 캐릭터")]
    public Character character; // 얘가 중요한거임 일단.

    void Start()
    {
        // 1. 씬에 "Player"라는 태그가 부착된 오브젝트를 넣는다.
        player = GameObject.FindGameObjectWithTag("Player");

        // 2. 그 객체의 컴퍼넌트를 담는다.
        playerController = player.GetComponent<PlayerController>();
        battleSystem = player.GetComponent<BattleSystem>();
        levelSystem = player.GetComponent<LevelSystem>();
        playerStats = player.GetComponent<PlayerStats>();
        statCalculator = player.GetComponent<StatCalculator>();
        spriteRenderer = player.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // ========= 임시 코드
        KeyCode key = KeyCode.None;
        if (Input.GetKeyDown(KeyCode.Alpha1)) key = KeyCode.Alpha1;
        else if (Input.GetKeyDown(KeyCode.Alpha2)) key = KeyCode.Alpha2;

        switch (key)
        {
            case KeyCode.Alpha1:
                character = characterList[0];
                InitPlayer(character);
                break;

            case KeyCode.Alpha2:
                character = characterList[1];
                InitPlayer(character);
                break;
        }
        // =========
    }

    public void InitPlayer(Character character)
    {
        // 캐릭터 기본 선택
        playerStats.character = character;
        battleSystem.aaPool.SetAAPool(character);
        spriteRenderer.sprite = playerStats.character.sprite;
        statCalculator.DefaultCalculate();
    }
}
