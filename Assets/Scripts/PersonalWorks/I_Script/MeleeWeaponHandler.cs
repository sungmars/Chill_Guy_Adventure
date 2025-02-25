using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponHandler : WeaponHandler
{
    [Header("Melee Attack Info")]
    public Vector2 collideBoxSize = Vector2.one;

    protected override void Start()
    {
        base.Start();
        collideBoxSize = collideBoxSize * WeaponSize;
    }

    public override void Attack()
    {
        base.Attack();

        for (int j = 0; j < playerController.spawnedEnemies.Count; j++)
        {
            if (playerController.IsInClosedRange[j])
            {
                RaycastHit2D hit = Physics2D.BoxCast(transform.position + (Vector3)Controller.LookDirection * collideBoxSize.x,
            collideBoxSize, 0, playerController.PlayerToEnemyVectors[j], 0, target);
            }
        }
        
    }
}
