using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : EnemyController
{
    [SerializeField] SpriteRenderer bossSR;

    private bool flipX = false;
    private bool onSkill = false;

    protected override void Attack()
    {
    }

    protected override void Move()
    {
        if (!onSkill)
        {
            if (player.transform.position.x < transform.position.x)
            {
                flipX = false;
                bossSR.flipX = flipX;
            }
            else
            {
                flipX = true;
                bossSR.flipX = flipX;
            }
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void Start()
    {
        base.Start();
        StartCoroutine(Rush());
    }

    protected void RandomSkill()
    {
        int idxSkill = Random.Range(0, 5);

        switch (idxSkill)
        {
            case 0:
                StartCoroutine(Rush());
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            default:
                break;
        }
    }

    protected IEnumerator Rush()
    {
        onSkill = true;
        Vector2 backPos;
        Vector2 temPos;
        Vector2 targetPos = new Vector2(player.transform.position.x, player.transform.position.y);

        backPos = (Vector2)transform.position - targetPos.normalized;

        while (Vector2.Distance(backPos, transform.position) > 0.1f)
        {
            temPos = Vector2.Lerp(transform.position, backPos, Time.deltaTime * 1.5f);
            transform.position = temPos;
            yield return new WaitForSeconds(0f);
        }

        while (Vector2.Distance(targetPos, transform.position) > 0.1f)
        {
            temPos = Vector2.Lerp(transform.position, targetPos, Time.deltaTime * 1.5f);
            transform.position = temPos;
            yield return new WaitForSeconds(0f);
        }


        onSkill = false;
        yield return null;
    }
}
