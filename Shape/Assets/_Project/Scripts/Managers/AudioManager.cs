using UnityEngine;

public class AudioManager : SingleTon<AudioManager>
{
    public AudioSource audioSource;

    [SerializeField] private AudioClip[] _sfxs;

    [Header("브금 목록")]
    [SerializeField] private AudioClip[] _general_BGMs;
    [SerializeField] private AudioClip[] _boss_BGMs;

    void Start()
    {
        PlayGeneralBGM();
    }

    void PlaySFX()
    {

    }
    
    public void PlayGeneralBGM()
    {
        if(GameManager.Instance.gameState == GameState.General)
        {
            audioSource.clip = _general_BGMs[0];
            if (audioSource.clip != null)
                audioSource.Play();    
        }
    }

    public void PlayBossBGM()
    {
        if(GameManager.Instance.gameState == GameState.Boss)
        {
            audioSource.clip = _boss_BGMs[0];
            if (audioSource.clip != null)
                audioSource.Play();    
        }
    }
}
