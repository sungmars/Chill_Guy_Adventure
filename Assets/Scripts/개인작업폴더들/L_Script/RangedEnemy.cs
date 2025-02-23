using UnityEngine;

public class RangedEnemy : EnemyController
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    private float nextFireTime;
 
    protected override void Move()
    {
        if (Vector2.Distance(transform.position, player.position) > attackRange)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * moveSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    protected override void Attack()
    {
        Debug.Log("사거리 안입니다.");
        if (Time.time >= nextFireTime)
        {
            ShootProjectile();
            nextFireTime = Time.time + 1f / attackCooldown; //공격속도 방영하여 쿨다운 적용
        }
    }

    private void ShootProjectile()
    {
        Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Debug.Log("사거리 밖입니다.");
    }
}