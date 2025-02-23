using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float moveSpeed;
    public int health;
    public int attackDamage;
    public float attackRange;

    protected Transform player;
    protected bool isChasing;

    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected virtual void Update()
    {
        if(player != null)
        {
            Move();
        }
    }
    protected abstract void Move(); //이동 패턴
    protected abstract void Attack();
    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        if(health<=0)
        {
            Die();
        }
    }
    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
