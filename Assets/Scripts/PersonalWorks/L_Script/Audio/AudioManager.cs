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
            playerAudioController = new PlayerAudioController();
            bossAudioController = new BossAudioController();
            bgmController = new BGMController();
            enemyAudioController = new EnemyAudioController();
        }
        else
        {
            Destroy(gameObject);
        }
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