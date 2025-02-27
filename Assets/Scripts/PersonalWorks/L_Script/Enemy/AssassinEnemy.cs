using System.Collections;
using UnityEngine;

public class AssassinEnemy : EnemyController
{
    public AudioClip attackaudio;
    public Collider2D teleportCollider;
    private bool hasTeleported = false;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasTeleported)
        {
            Debug.Log("플레이어감지");
            hasTeleported = true;
            StartCoroutine(TeleportReady());
        }
    }
    private IEnumerator TeleportReady()
    {
        animator.SetBool("IsTeleportReady", true);
        isStopped = true; // 오브젝트를 멈추게 함
        yield return new WaitForSeconds(1f);
        isStopped = false; // 오브젝트를 다시 움직이게 함
        animator.SetBool("IsTeleportReady", false);
        hasTeleported = false;
        Teleport();
    }

    private void Teleport()
    {
        animator.SetBool("IsTeleportArrive", true);
        if (Vector2.Distance(transform.position, player.position) > attackRange)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position = (Vector2)player.position - direction * (attackRange+1f);
            Debug.Log("순간이동 완료");
        }
    }

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
    
}
