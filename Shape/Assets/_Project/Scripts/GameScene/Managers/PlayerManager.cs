using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[DefaultExecutionOrder(-100)]// 얘가 젤 먼저 실행되야함
public class PlayerManager : SingleTon<PlayerManager> 
{
    [Header("플레이어")]
    public GameObject playerPrefab;
    public GameObject player;
    public Transform playerTr;

    [Header("캐릭터 목록")]
    [SerializeField] private Character[] _characterList;

    [Header("Controller")]
    public PlayerController playerController;
    public BattleController battleController;
    public LevelController levelController;
    public Drain drain;


    [Header("Model")]
    public StatModel statModel;


    [Header("Service")]
    public StatCalculator statCalculator;

    
    [Header("컴퍼넌트")]
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;

    [Header("현재 선택된 캐릭터")]
    public Character character; // 얘가 중요한거임 일단.

    protected override void Awake()
    {
        base.Awake();
        SpawnPlayer(); // 플레이어 생성

        player = GameObject.FindGameObjectWithTag("Player");

        // 2. 그 객체의 컴퍼넌트를 담는다.
        playerController = player.GetComponent<PlayerController>();
        battleController = player.GetComponent<BattleController>();
        levelController = player.GetComponent<LevelController>();
        drain = player.GetComponentInChildren<Drain>();

        statModel = player.GetComponent<StatModel>();

        statCalculator = player.GetComponent<StatCalculator>();
        
        spriteRenderer = player.GetComponent<SpriteRenderer>();
        rb = player.GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        StartCoroutine(InitPlayerCoroutine());
    }

    IEnumerator InitPlayerCoroutine()
    {
        yield return null;

        character = _characterList[CharacterManager.Instance.secletCharacterID];
        InitPlayer(character);
    }

    void SpawnPlayer()
    {
        player = Instantiate(playerPrefab);
        playerTr = player.transform;
        player.transform.position = Vector3.zero;
    }

    public void InitPlayer(Character character)
    {
        spriteRenderer.sprite = character.sprite; // 캐릭터 이미지 세팅하고

        SetSkillData();
        statCalculator.DefaultCalculate(); // 기본 스펙 세팅
        battleController.aaPool.SetAAPool(character);  // aapool 만들어서 총알장전
        playerController.OnApplyVital(statModel.stat); // 최초 체력이랑 마나 세팅
        character.weaponInstance = Instantiate(character.weapon);
        character.weaponInstance.InitWeapon(player);
        StartCoroutine(character.weaponInstance.WeaponController(player));
        StartCoroutine(playerController.AutoManaRecoverCoroutine());
        StartCoroutine(playerController.AutoStaminaRecoverCoroutine());
    }

    void SetSkillData()
    {
        // 복사본 생성
        character.D_SkillInstance = Instantiate(character.D_Skill);
        character.Q_SkillInstance = Instantiate(character.Q_Skill);
        character.W_SkillInstance = Instantiate(character.W_Skill);
        character.E_SkillInstance = Instantiate(character.E_Skill);
        character.R_SkillInstance = Instantiate(character.R_Skill);
        SkillRuntimeView.Instance.SetSkill(character); // 스킬 쿨타임 뷰 세팅
    }
}
