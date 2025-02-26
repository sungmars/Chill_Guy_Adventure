using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class RushAttackScripts : MonoBehaviour
{

    public void PublicRushAttack(Transform boss,Transform player)
    {
        RushAttack(boss,player);
    }

    private void RushAttack(Transform boss, Transform player)
    {
        StartCoroutine(RushAttackCoroutine(boss, player));
    }

    private IEnumerator RushAttackCoroutine(Transform boss, Transform player)
    {
        Vector2 targetPos = new Vector2(player.transform.position.x, player.transform.position.y);
        Vector2 curPos = boss.transform.position;
        Vector2 backDir = (curPos - targetPos).normalized;
        Vector2 backPos = curPos + backDir * 1f;
        Vector2 movePos;
        while (Vector2.Distance(backPos, boss.transform.position) > 0.1f)
        {
            movePos = Vector2.Lerp(boss.transform.position, backPos, Time.deltaTime * 1.5f);
            boss.transform.position = movePos;
            yield return new WaitForSeconds(0f);
        }

        while (Vector2.Distance(targetPos, boss.transform.position) > 0.1f)
        {
            movePos = Vector2.Lerp(boss.transform.position, targetPos, Time.deltaTime * 2f);
            boss.transform.position = movePos;
            yield return new WaitForSeconds(0f);
        }
        yield return new WaitForSeconds(3f);
    }
}
