using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChillAttackController : MonoBehaviour
{
    Transform chill;
    Transform bottom;

    private void Awake()
    {
        chill = transform.GetChild(0);
        bottom = transform.GetChild(1);
    }

    void Start()
    {
        Invoke("ChillDrop", 1f);
    }

    void ChillDrop()
    {
        StartCoroutine(ChillAttackAnim());
    }

    IEnumerator ChillAttackAnim()
    {
        float y = chill.position.y;
        Vector2 targetPos = new Vector2(bottom.position.x, bottom.position.y + 0.55f);
        while (Vector2.Distance(chill.position, targetPos) > 0.1f)
        {            
            if (Vector2.Distance(chill.position, targetPos) < 2f)
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
