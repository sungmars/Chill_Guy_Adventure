using System.Collections;
using UnityEngine;

public class MeleeEnemy : EnemyController
{
    [SerializeField] private float knockbackPower = 5.0f;

    

    protected override void Attack()
    {
        
        if (Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            Debug.Log("때림 " + attack + " 의 데미지");
            animator.SetTrigger("AttackTrigger");
            animator.SetBool("IsRun", false);
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
