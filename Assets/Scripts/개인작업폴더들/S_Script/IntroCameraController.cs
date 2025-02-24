using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCameraController : MonoBehaviour
{
    public IEnumerator MoveCamera(GameObject player, GameObject boss, IEnumerator next)
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10f);
        float x = transform.position.x;
        float y = transform.position.y;
        float z = -10f;

        while ((int)transform.position.x != 0)
        {
            x = Mathf.Lerp(transform.position.x, boss.transform.position.x, Time.deltaTime);
            Debug.Log(x);
            transform.position = new Vector3(x, y, z);
            yield return new WaitForSeconds(0f);
        }
        StartCoroutine(next);
    }
}
