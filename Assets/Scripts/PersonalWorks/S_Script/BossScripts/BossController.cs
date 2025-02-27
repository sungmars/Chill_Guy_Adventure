using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BossController : BaseController
{
    Transform player;
    [SerializeField] private AudioClip bossBgm;
    [SerializeField] private AudioClip lookAround;    

    private bool filp = false;

    protected override void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected override void Start()
    {
        AudioManager.Instance.PlayBGM(bossBgm);
    }

    protected override void FixedUpdate()
    {

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
