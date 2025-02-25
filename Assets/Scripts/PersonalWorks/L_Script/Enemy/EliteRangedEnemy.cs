using UnityEngine;

public class EliteRangedEnemy : RangedEnemy
{
    // 특수공격 쿨
    [SerializeField] private float powerAttackCooldown = 3.0f;
    // 느리게 발사
    [SerializeField] private float eliteProjectileSpeed = 7f;
    // 투사체 크기 키우기
    [SerializeField] private float projectileScaleMultiplier = 1.5f;

    private float nextPowerAttackTime;

    protected override void Attack()
    {
        if (player == null)
            return;

        // 일정 시간마다 쌈@뽕한 공격 실행
        if (Time.time >= nextPowerAttackTime)
        {
            ShootPowerProjectile();
            nextPowerAttackTime = Time.time + powerAttackCooldown;
        }
    }

    private void ShootPowerProjectile()
    {
        Debug.Log("엘리트 원딜 몹의 강력한 공격 발사");
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        projectile.transform.localScale *= projectileScaleMultiplier;
        Vector2 direction = (player.position - firePoint.position).normalized;
        projectile.transform.right = direction;

        // 리지드바디를 통해 발사체에 느린 속도 부여
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * eliteProjectileSpeed;
        }
    }
}
