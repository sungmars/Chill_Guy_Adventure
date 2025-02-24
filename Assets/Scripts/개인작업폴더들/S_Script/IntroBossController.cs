using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroBossController : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    protected readonly int findbool = Animator.StringToHash("FindBool");   

    public IEnumerator BossIn()
    {
        this.gameObject.SetActive(true);
        transform.position = new Vector3(0, 10f, 0);
        float x = transform.position.x;
        float y = transform.position.y;
        float z = 0;
        float speed = 0.1f;

        while ((int)transform.position.y != 0)
        {
            y = transform.position.y - speed;
            Debug.Log(y);
            transform.position = new Vector3(x, y, z);
            speed += 0.005f;
            yield return new WaitForSeconds(0f);
        }

        animator.SetBool(findbool, true);
    }
}
