using System.Collections;
using UnityEngine;

public abstract class EnemyController : BaseController
{
    public float attackRange;

    protected Transform player;
    protected bool isChasing;
    protected Rigidbody2D rb;
    private float lastAttackTime;


    protected override void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        base.Start();
    }
    
    protected override void FixedUpdate()
    {
        if (player != null)
        {
            Move();
            TryAttack();
        }
    }

    protected abstract void Move(); // 이동 패턴 


    protected void TryAttack()
    {
        if (Time.time >= lastAttackTime + attackSpeed)
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }

    public override void Die()
    {
        //플레이어에게 경험치와 골드 주기
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.ReceiveExp(exp);
            playerController.ReceiveGold(gold);
        }
        base.Die();
    }
}