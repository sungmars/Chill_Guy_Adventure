using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    //종류마다 나눠진 사운드 컨트롤러들
    private PlayerAudioController playerAudioController;
    private BossAudioController bossAudioController;
    private BGMController bgmController;
    private EnemyAudioController enemyAudioController;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            //각 컨트롤러 초기화
            playerAudioController = gameObject.AddComponent<PlayerAudioController>();
            bossAudioController = gameObject.AddComponent<BossAudioController>();
            bgmController = gameObject.AddComponent<BGMController>();
            enemyAudioController = gameObject.AddComponent<EnemyAudioController>();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // BGM 전용 볼륨 업데이트
    public void SetBGMVolume(float volume)
    {
        bgmController.SetVolume(volume);
    }

    // 효과음(SFX) 전용 볼륨 업데이트 (플레이어, 보스, 몹 효과음 모두 포함)
    public void SetSFXVolume(float volume)
    {
        playerAudioController.SetVolume(volume);
        bossAudioController.SetVolume(volume);
        enemyAudioController.SetVolume(volume);
    }
    public void PlayPlayerSound(AudioClip clip)
    {
        playerAudioController.PlaySound(clip);
    }
    public void PlayBossSound(AudioClip clip)
    {
        bossAudioController.PlaySound(clip);
    }
    public void PlayBGM(AudioClip clip)
    {
        bgmController.PlayMusic(clip);
    }
    public void PlayEnemySound(AudioClip clip)
    {
        enemyAudioController.PlaySound(clip);
    }
}