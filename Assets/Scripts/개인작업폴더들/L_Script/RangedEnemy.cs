using UnityEngine;

public class RangedEnemy : Enemy
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate;
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
        Debug.Log("Ranged attack! Shooting projectile.");
        if (Time.time >= nextFireTime)
        {
            ShootProjectile();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    private void ShootProjectile()
    {
        Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
    }
}