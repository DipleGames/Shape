using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] private CharacterManager _characterManager;

    [Header("게임 버튼")]
    [SerializeField] private Button _gameStart_Btn;
    [SerializeField] private Button _gameQuit_Btn;

    [Header("캐릭터 선택 버튼")]
    [SerializeField] private Button _increaseCharacterNum_Btn;
    [SerializeField] private Button _decreaseCharacterNum_Btn;

    public event Action OnChangedCharacterNum;
    

    void Awake()
    {
        _gameStart_Btn.onClick.AddListener(OnClickedGameStartBtn);
        _gameQuit_Btn.onClick.AddListener(OnClickedGameQuitBtn);

        _increaseCharacterNum_Btn.onClick.AddListener(OnClickedIncreaseCharacterNumBtn);
        _decreaseCharacterNum_Btn.onClick.AddListener(OnClickedDecreaseCharacterNumBtn);

        OnChangedCharacterNum += _characterManager.SetCharacterUI;
    }

    void Start()
    {
        SetSelectCharacterBtn(_characterManager.secletCharacterID);
        OnChangedCharacterNum.Invoke();
    }

    void OnClickedGameStartBtn()
    {
        SceneManager.LoadScene("GameScene");
    }

    void OnClickedGameQuitBtn()
    {
        Application.Quit();
    }

    void OnClickedIncreaseCharacterNumBtn()
    {
        if(_characterManager.secletCharacterID == _characterManager.characterList.Length - 1) return;
        _characterManager.secletCharacterID++;
        
        SetSelectCharacterBtn(_characterManager.secletCharacterID);

        OnChangedCharacterNum.Invoke();
    }

    void OnClickedDecreaseCharacterNumBtn()
    {
        if(_characterManager.secletCharacterID == 0) return;
        _characterManager.secletCharacterID--;

        SetSelectCharacterBtn(_characterManager.secletCharacterID);

        OnChangedCharacterNum.Invoke();
    }

    void SetSelectCharacterBtn(int secletCharacterNumber)
    {
        if(secletCharacterNumber == 0)
        {
            _increaseCharacterNum_Btn.gameObject.SetActive(true);
            _decreaseCharacterNum_Btn.gameObject.SetActive(false);
        }
        else if(secletCharacterNumber == _characterManager.characterList.Length - 1)
        {
            _increaseCharacterNum_Btn.gameObject.SetActive(false);
            _decreaseCharacterNum_Btn.gameObject.SetActive(true);
        }
        else
        {
            _increaseCharacterNum_Btn.gameObject.SetActive(true);
            _decreaseCharacterNum_Btn.gameObject.SetActive(true);
        }
                
    }
}
