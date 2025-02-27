using UnityEngine;

public class RangedEnemy : EnemyController
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    private float nextFireTime;
    [SerializeField] private float projectileSpeed = 10f; // 발사체 속도
    public AudioClip attackaudio;
    public AudioClip damageAudio; // 데미지 받을 때 소리
    public AudioClip deathAudio; // 죽을 때 소리

    new void Update()
    {
        base.Update();
        if (player != null)
        {
            Vector2 direction = player.position - firePoint.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            firePoint.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }

    protected override void Attack()
    {
        Debug.Log("사거리 안입니다.");
        if (Time.time >= nextFireTime)
        {
            ShootProjectile();
            nextFireTime = Time.time + 1f / attackSpeed; // 공격속도 반영하여 쿨다운 적용
        }
    }

    private void ShootProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        var projectileController = projectile.GetComponent<EnemyProjectile>();
        projectileController.Init(GetComponent<BaseController>());
        // 플레이어 방향 계산
        Vector2 direction = (player.position - firePoint.position).normalized;
        // 발사체의 회전을 플레이어 방향에 맞게 조정
        projectile.transform.right = direction;
        // 속도 부여
        Rigidbody2D arrowRb = projectile.GetComponent<Rigidbody2D>();
        if (arrowRb != null)
        {
            arrowRb.velocity = direction * projectileSpeed;
        }
        AudioManager.Instance.PlayEnemySound(attackaudio); // 공격 소리 재생
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
