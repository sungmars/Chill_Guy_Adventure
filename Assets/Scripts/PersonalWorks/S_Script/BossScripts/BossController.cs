using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BossController : MonoBehaviour
{
    Transform player;

    [SerializeField] private GameObject chillDogPrefab;
    [SerializeField] private GameObject enemySpawnObject;    

    public float damage = 10f;
    public float lifetime = 5f;
    public float knockbackPower = 5f;
    public float knockbackDuration = 0.2f;
    public float hp = 100f;

    public AudioClip bossAudio;

    private bool onSkill = false;

    private void Awake()
    {       
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start()
    {
        //AudioManager.Instance.PlayBossSound(bossAudio);
        //InvokeRepeating("SkillRepeat", 2f, 5f);
    }

    private void Update()
    {
        if (!onSkill)
        {
            if (player.transform.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }

    private void SkillRepeat()
    {
        //animator.SetTrigger(isAttack);
        Invoke("RandomSkill", 1.5f);
    }

    private void CreateChillDog()
    {
        EnemySpawnObject spawnObject;
        GameObject chillDog;
        for (int i = 0; i < 5; i++)
        {
            float x = Random.Range(-8f, 9f);
            float y = Random.Range(-4f, 5f);
            spawnObject = spawnObject = Instantiate(enemySpawnObject, new Vector3(x, y), Quaternion.identity).GetComponent<EnemySpawnObject>();
            chillDog = Instantiate(chillDogPrefab, transform);
            chillDog.name = $"ChillDog {i}";
            chillDog.transform.position = new Vector2(x, y);
        }
    }

    //private IEnumerator KeyBoardInputAttack()
    //{
    //    player.GetComponent<PlayerInput>().enabled = false;
    //    CancelInvoke("SkillRepeat");
    //    float time = 0;
    //    GameObject keyBoardAttackObj = Instantiate(keyBoardAttackPrefab, transform);
    //    PlayerController playerController = player.GetComponent<PlayerController>();
    //    while (time < 6f)
    //    {
    //        time += Time.deltaTime;
    //        yield return new WaitForSeconds(0);
    //    }
    //    playerController.TakeDamage((int)damage);
    //    playerController.ApplyKnockback(transform, knockbackPower, knockbackDuration);
    //    Destroy(keyBoardAttackObj);
    //    InvokeRepeating("SkillRepeat", 3f, 5f);
    //    player.GetComponent<PlayerInput>().enabled = true;
    //    yield return new WaitForSeconds(2);
    //}

}
