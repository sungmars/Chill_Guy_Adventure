using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public static BossManager Instance { get; private set; }

    RushAttackScripts rushAttack;
    SoundWaveAttackController soundWaveAttackController;
    ChillAttackController chillAttackController;
    KeyBoardAttackController keyBoardAttackController;
    ChillDogAttackController chillDogAttackController;

    [SerializeField] Transform boss;
    [SerializeField] Animator animator;
    private static readonly int isAttack = Animator.StringToHash("IsAttack");

    [SerializeField] Transform player;

    public AudioClip bossEye;   

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            rushAttack = GetComponent<RushAttackScripts>();
            soundWaveAttackController = GetComponent<SoundWaveAttackController>();
            chillAttackController = GetComponent<ChillAttackController>();
            keyBoardAttackController = GetComponent<KeyBoardAttackController>();
            chillDogAttackController = GetComponent<ChillDogAttackController>();
        }
    }

    private void Start()
    {
        InvokeRepeating("SkillRepeat", 2f, 5f);
    }

    private void SkillRepeat()
    {
        animator.SetTrigger(isAttack);
        AudioManager.Instance.PlayBossSound(bossEye);
        Invoke("RandomSkill", 1.5f);
    }

    private void RandomSkill()
    {
        int idxSkill = Random.Range(0, 5);
        switch (idxSkill)
        {
            case 0:
                rushAttack.PublicRushAttack(boss, player);
                break;
            case 1:
                soundWaveAttackController.PublicSoundWaveAttack(player);
                break;
            case 2:
                chillAttackController.PublicChillAttack(player);
                break;
            case 3:
                chillDogAttackController.PublicCreateChillDog();
                break;
            case 4:
                CancelInvoke("SkillRepeat");
                keyBoardAttackController.PublicKeyBoardAttack();
                InvokeRepeating("SkillRepeat", 5f, 5f);
                break;
        }
    }
}
