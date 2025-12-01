using UnityEngine;

public class AudioManager : SingleTon<AudioManager>
{
    [Header("Audio Sources")]
    public AudioSource bgmSource;   // 브금 재생용
    public AudioSource sfxSource;   // 효과음 재생용

    [SerializeField] private AudioClip[] _sfxs;

    [Header("브금 목록")]
    [SerializeField] private AudioClip[] _general_BGMs;
    [SerializeField] private AudioClip[] _boss_BGMs;

    void Start()
    {
        PlayGeneralBGM();
    }

    public void PlayUpgradeSFX()
    {
        sfxSource.PlayOneShot(_sfxs[0], 0.5f);
    }

    public void PlayEnemyHitSFX()
    {
        sfxSource.PlayOneShot(_sfxs[1], 0.5f);
    }
    
    public void PlayGeneralBGM()
    {
        if(GameManager.Instance.gameState == GameState.General)
        {
            bgmSource.clip = _general_BGMs[0];
            if (bgmSource.clip != null)
                bgmSource.Play();    
        }
    }

    public void PlayBossBGM()
    {
        if(GameManager.Instance.gameState == GameState.Boss)
        {
            bgmSource.clip = _boss_BGMs[0];
            if (bgmSource.clip != null)
                bgmSource.Play();    
        }
    }
}
