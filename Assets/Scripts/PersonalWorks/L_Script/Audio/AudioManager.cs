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

    private const string BGMVolumeKey = "BGMVolume";
    private const string SFXVolumeKey = "SFXVolume";

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

    private void Start()
    {
        //저장된 볼륨값을 불러오고 데이터 없으면 0.5가 기본값
        float bgmVolume = PlayerPrefs.GetFloat(BGMVolumeKey, 0.5f);
        float sfxVolume = PlayerPrefs.GetFloat(SFXVolumeKey, 0.5f);
        SetBGMVolume(bgmVolume);
        SetSFXVolume(sfxVolume);
    }

    // BGM 전용 볼륨 업데이트
    public void SetBGMVolume(float volume)
    {
        bgmController.SetVolume(volume);
        PlayerPrefs.SetFloat(BGMVolumeKey, volume);
        PlayerPrefs.Save();
    }

    // 효과음(SFX) 전용 볼륨 업데이트
    public void SetSFXVolume(float volume)
    {
        playerAudioController.SetVolume(volume);
        bossAudioController.SetVolume(volume);
        enemyAudioController.SetVolume(volume);
        PlayerPrefs.SetFloat(SFXVolumeKey, volume);
        PlayerPrefs.Save();
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
