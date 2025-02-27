using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeaponHandler : WeaponHandler
{
    [Header("Ranged Attack Data")]
    [SerializeField] private Transform projectileSpawnPosition;

    [SerializeField] private int bulletIndex;
    public int BulletIndex { get { return bulletIndex; } }

    [SerializeField] private float bulletSize = 1f;
    public float BulletSize { get { return bulletSize; } }

    [SerializeField] private float duration;
    public float Duration { get { return duration; } }

    [SerializeField] private float spread;
    public float Spread { get { return spread; } }    

    [SerializeField] private Color projectileColor;
    public Color ProjectileColor { get { return projectileColor; } }

    private ProjectileManager projectileManager;

    public Vector2 collideBoxSize = Vector2.one;

    protected override void Start()
    {
        base.Start();
        projectileManager = ProjectileManager.Instance;

        collideBoxSize = collideBoxSize * WeaponSize;
    }

    public override void Attack()
    {
        base.Attack();

        if (playerController.AttackModeChange == false)
        {
            for (int j = 0; j < playerController.spawnedEnemies.Count; j++)
            {
                if (playerController.IsInClosedRange[j])
                {
                    RaycastHit2D hit = Physics2D.BoxCast(transform.position + (Vector3)Controller.LookDirection * collideBoxSize.x,
                collideBoxSize, 0, playerController.PlayerToEnemyVectors[j], 0, target);

                    if (hit.collider != null)
                    {
                        var enemyController = hit.collider.GetComponent<BaseController>();
                        if (enemyController != null)
                        {
                            enemyController.TakeDamage((int)playerController.attack);
                            enemyController.ApplyKnockback(transform, playerController.knockbackPower, playerController.knockbackTime);
                        }
                    }
                }
            }
        }
        else if (playerController.AttackModeChange == true)
        {
            float projectileAngleSpace = multipleProjectileAngle;
            int numberOfProjectielPerShot = numberofProjectilesPerShot;

            float minAngle = -(numberOfProjectielPerShot / 2f) * projectileAngleSpace;

            for (int i = 0; i < numberOfProjectielPerShot; i++)
            {
                float angle = minAngle + projectileAngleSpace * i;
                float randomSpread = Random.Range(-spread, spread);
                angle += randomSpread;

                for (int j = 0; j < playerController.spawnedEnemies.Count; j++)
                {
                    if (playerController.IsInLongRange[j])
                        CreateProjectile(playerController.PlayerToEnemyVectors[j], angle);
                }
            }
        }
    }

    private void CreateProjectile(Vector2 _attackDirection, float angle)
    {
        projectileManager.ShootBullet(
            this,
            projectileSpawnPosition.position,
            RotateVector2(_attackDirection, angle),
            playerController
            );
    }

    private static Vector2 RotateVector2(Vector2 v, float degree)
    {
        return Quaternion.Euler(0, 0, degree) * v;
    }

    public override void Rotate(bool isLeft)
    {
        if (playerController.AttackModeChange == false)
        {
            if (isLeft)
                transform.eulerAngles = new Vector3(0, 180, 0);
            else
                transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
}
