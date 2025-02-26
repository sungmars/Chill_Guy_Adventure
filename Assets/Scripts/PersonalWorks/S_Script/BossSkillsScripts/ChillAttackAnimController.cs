using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChillAttackAnimController : MonoBehaviour
{
    Transform chill;
    Transform bottom;
    Transform bottomPivot;

    private void Awake()
    {
        chill = transform.GetChild(0);
        bottom = transform.GetChild(1);
        bottomPivot = transform.GetChild(1).GetChild(0);
    }

    private void Start()
    {
        Invoke("ChillDrop", 2f);
    }


    private void ChillDrop()
    {
        StartCoroutine(ChillAttackAnim());
    }


    private IEnumerator ChillAttackAnim()
    {
        float y = chill.position.y;
        while (Vector2.Distance(chill.position, bottomPivot.position) > 0.15f)
        {
            Debug.Log(Vector2.Distance(chill.position, bottomPivot.position));
            if (Vector2.Distance(chill.position, bottomPivot.position) < 2f)
            {
                bottom.GetComponent<BoxCollider2D>().enabled = true;
            }
            y -= Time.deltaTime * 13f;
            chill.position = new Vector2(chill.position.x, y);
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}
