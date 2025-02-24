using System.Collections;
using UnityEngine;

public class MeleeEnemy : EnemyController
{
    protected override void Move()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;
    }

    protected override void Attack()
    {
        if (Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            Debug.Log("때림 " + attackDamage + " 의 데미지");
        }
        // 플레이어에게 데미지를 주는 코드
    }
}
