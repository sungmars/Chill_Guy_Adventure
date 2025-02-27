using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class IntroCameraController : MonoBehaviour
{
    public IEnumerator MoveCamera(GameObject player, GameObject boss, IEnumerator next)
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10f);
        float x = transform.position.x;
        float y = transform.position.y;
        float z = -10f;

        while (Vector2.Distance(transform.position, Vector2.zero) > 0.1f)
        {
            x = Mathf.Lerp(transform.position.x, boss.transform.position.x, Time.deltaTime);
            transform.position = new Vector3(x, y, z);
            yield return new WaitForSeconds(0f);
        }
        transform.position = new Vector3(0, 0, -10);
        StartCoroutine(next);
    }
}
