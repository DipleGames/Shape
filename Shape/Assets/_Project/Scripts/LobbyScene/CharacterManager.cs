using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : SingleTon<CharacterManager>
{
    public int secletCharacterID = 0;
    public Character[] characterList;

    public Image character_Img;

    protected override void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SetCharacterUI()
    {
        Debug.Log("이벤트발생");
        character_Img.sprite = characterList[secletCharacterID].sprite;
    }
}

