using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class EnemyController : BaseController
{
    public float attackRange;
    protected Animator animator;
    protected Transform player;
    protected bool isChasing;
    private float lastAttackTime;
    protected bool isStopped = false;
    // 스프라이트 뒤집기를 위한 SpriteRenderer 추가
    [SerializeField] private SpriteRenderer _spriteRenderer;

    // 맞았을 때
    private float timeSinceLastChange = float.MaxValue;
    private float healthChangeDelay = .5f; // 맞았을 때 0.5초 동안 빨간색

    protected override void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        if (_spriteRenderer == null)
            _spriteRenderer = GetComponent<SpriteRenderer>();
        base.Start();
    }

    public void Update()
    {
        // 맞았을 때 빨간색
        if (timeSinceLastChange < healthChangeDelay)
        {
            timeSinceLastChange += Time.deltaTime;
            if (timeSinceLastChange >= healthChangeDelay)
            {
                animator.SetBool("isDamage", false);
            }
        }
    }

    protected override void FixedUpdate()
    {
        // base.FixedUpdate();
        if (knockbackDuration > 0.0f)
        {
            knockbackDuration -= Time.fixedDeltaTime;
            KnockbackMovement();
        }
        else if (!isStopped && player != null)
        {
            Move();
            TryAttack();
        }
    }


    private void KnockbackMovement()
    {
        Vector2 direction = knockback;
        _rigidbody2D.velocity = direction;
    }

    protected virtual void Move()
    {
        animator.SetBool("IsRun", true);
        //플레이어 바라보기
        if (player.position.x < transform.position.x)
            _spriteRenderer.flipX = true;
        else
            _spriteRenderer.flipX = false;

        if (isStopped)
        {
            _rigidbody2D.velocity = Vector2.zero;
            return;
        }
        else if (Vector2.Distance(transform.position, player.position) > attackRange)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            _rigidbody2D.velocity = direction * speed;
        }
        else
        {
            _rigidbody2D.velocity = Vector2.zero;
        }


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
        // animator.SetBool("IsKnockback", true);
        // StartCoroutine(ResetKnockbackCoroutine());

        timeSinceLastChange = 0f;
        animator.SetBool("isDamage", true);
    }

    // private IEnumerator ResetKnockbackCoroutine()
    // {
    //     yield return new WaitForSeconds(0.5f);
    //     // animator.SetBool("IsKnockback", false);
    // }
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
