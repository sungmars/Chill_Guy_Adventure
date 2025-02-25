using UnityEngine;

public class EliteMeleeEnemy : MeleeEnemy
{
    // 엘리트 몹만의 추가 옵션: 공격력과 넉백 효과 강화
    [SerializeField] private float eliteAttackMultiplier = 1.5f;    // 기본 공격력의 1.5배
    [SerializeField] private float eliteKnockbackMultiplier = 1.2f;   // 넉백 파워의 1.2배

    // 엘리트 몹의 공격 메서드를 오버라이드
    protected override void Attack()
    {
        if (Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            // 기존 공격력에 곱셈 적용
            float totalAttack = attack * eliteAttackMultiplier;
            Debug.Log("엘리트 공격: " + totalAttack + " 의 데미지");
            //애니 관리
            animator.SetTrigger("AttackTrigger");
            animator.SetBool("IsRun", false);

            BaseController playerController = player.GetComponent<BaseController>();
            if (playerController != null)
            {
                //강력한 데미지
                playerController.TakeDamage((int)totalAttack);
                // 강력한 넉백
                playerController.ApplyKnockback(transform, knockbackPower * eliteKnockbackMultiplier, knockbackDuration);
            }
        }
    }
}
