using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    protected override void Move()
    {
        if (Vector2.Distance(transform.position, player.position) > attackRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    protected override void Attack()
    {
        if(Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            Debug.Log("대충 데미지 메서드 호출");
        }
    }
}

