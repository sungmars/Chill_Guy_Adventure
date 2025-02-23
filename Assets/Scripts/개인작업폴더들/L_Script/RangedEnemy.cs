using System.Collections;
using System.Collections.Generic;
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
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    protected override void Attack()
    {
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
