using System.Collections;
using UnityEngine;

public class MeleeEnemy : EnemyController
{
    [SerializeField] private float knockbackPower = 5.0f;

    protected override void Move()
    {
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
        {
            rb.velocity = Vector2.zero;
        }
    }

    protected override void Attack()
    {
        if (Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            Debug.Log("때림 " + attack + " 의 데미지");

            BaseController playerController = player.GetComponent<BaseController>();
            if (playerController != null)
            {
                // 플레이어에게 데미지 적용
                playerController.TakeDamage((int)attack);
                // 플레이어에게 넉백 효과
                playerController.ApplyKnockback(transform, knockbackPower, knockbackDuration);
            }
        }
    }
}
