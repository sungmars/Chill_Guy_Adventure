using System.Collections;
using UnityEngine;

public class MeleeEnemy : EnemyController
{
    public AudioClip attackaudio;
    public AudioClip damageAudio; // 데미지 받을 때 소리
    public AudioClip deathAudio; // 죽을 때 소리

    protected override void Attack()
    {
        if (Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            Debug.Log("때림 " + attack + " 의 데미지");
            AudioManager.Instance.PlayEnemySound(attackaudio);
            animator.SetTrigger("AttackTrigger");
            animator.SetBool("IsRun", false);
            BaseController playerController = player.GetComponent<BaseController>();
            if (playerController != null)
            {
                // 플레이어에게 데미지 적용
                playerController.TakeDamage((int)attack);
                // 플레이어에게 넉백 효과
                playerController.ApplyKnockback(transform, knockbackPower, knockbackTime);
            }
        }
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        AudioManager.Instance.PlayEnemySound(damageAudio); // 데미지 받을 때 소리 재생
    }

    public override void Die()
    {
        base.Die();
        AudioManager.Instance.PlayEnemySound(deathAudio); // 죽을 때 소리 재생
    }
}
