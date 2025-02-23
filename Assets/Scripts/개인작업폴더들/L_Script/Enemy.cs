using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    public float moveSpeed;
    public int health;
    public int attackDamage;
    public float attackRange;
    public float attackCooldown;

    protected Transform player;
    protected bool isChasing;
    protected Rigidbody2D rb;
    private float lastAttackTime;

    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void FixedUpdate()
    {
        if (player != null)
        {
            Move();
            TryAttack();
        }
    }

    protected abstract void Move(); // 이동 패턴 (추적 또는 순찰 등)
    protected abstract void Attack(); // 공격 패턴

    protected void TryAttack()
    {
        if (Time.time >= lastAttackTime + attackCooldown && Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}