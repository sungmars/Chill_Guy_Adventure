using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BossController : MonoBehaviour
{
    Transform player;

    public float damage = 10f;
    public float lifetime = 5f;
    public float knockbackPower = 5f;
    public float knockbackDuration = 0.2f;
    public float hp = 100f;

    [SerializeField] private AudioClip bossBgm;
    [SerializeField] private AudioClip lookAround;    

    private bool filp = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start()
    {
        AudioManager.Instance.PlayBGM(bossBgm);
    }

    private void Update()
    {
        if (player.transform.position.x < transform.position.x)
        {
            if (filp)
            {
                transform.localScale = new Vector3(1, 1, 1);
                AudioManager.Instance.PlayBossSound(lookAround);
                filp = false;
            }
        }
        else
        {
            if (!filp)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                AudioManager.Instance.PlayBossSound(lookAround);
                filp = true;
            }
        }
    }
}
