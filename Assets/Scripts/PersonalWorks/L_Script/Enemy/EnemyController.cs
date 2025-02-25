using System.Collections;
using UnityEngine;

public abstract class EnemyController : BaseController
{
    public float attackRange;
    protected Animator animator;
    protected Transform player;
    protected bool isChasing;
    protected Rigidbody2D rb;
    private float lastAttackTime;
    protected bool isStopped = false;
    // 스프라이트 뒤집기를 위한 SpriteRenderer 추가
    [SerializeField] private SpriteRenderer _spriteRenderer;

    protected override void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        if (_spriteRenderer == null)
            _spriteRenderer = GetComponent<SpriteRenderer>();
        base.Start();
    }

    protected override void FixedUpdate()
    {
        if (!isStopped && player != null)
        {
            Move();
            TryAttack();
        }
    }

    protected virtual void Move()
    {
        animator.SetBool("IsRun",true);
        //플레이어 바라보기
        if (player.position.x < transform.position.x)
            _spriteRenderer.flipX = true;
        else
            _spriteRenderer.flipX = false;

        if (isStopped)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        else if (Vector2.Distance(transform.position, player.position) > attackRange)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * speed;
        }
        else
            rb.velocity = Vector2.zero;

    }

    protected void TryAttack()
    {
        if (Time.time >= lastAttackTime + attackSpeed)
        {
            Attack();
            lastAttackTime = Time.time;
            StartCoroutine(StopMoveCoroutine(0.5f));
        }
    }

    private IEnumerator StopMoveCoroutine(float duration)
    {
        isStopped = true;
        yield return new WaitForSeconds(duration);
        isStopped = false;
    }
    public override void TakeDamage(int damage)
    {
        Debug.Log("플레이어에게 공격당함");
        base.TakeDamage(damage);
        bool isRun = animator.GetBool("IsRun");
        animator.SetBool("IsRun", false);
        if (!isRun)
            animator.SetTrigger("KnockbackTrigger");
    }
    public override void Die()
    {
        // 플레이어에게 경험치와 골드 주기
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.ReceiveExp(exp);
            playerController.ReceiveGold(gold);
        }
        base.Die();
    }
}
