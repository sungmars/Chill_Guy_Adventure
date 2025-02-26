using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChillAttackCollisonController : MonoBehaviour
{
    public float damage = 10f;
    public float knockbackPower = 5f;
    public float knockbackDuration = 0.2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerFoot") || collision.CompareTag("Enemy"))
        {
            BaseController baseController = collision.GetComponent<BaseController>();
            if (baseController != null)
            {
                baseController.TakeDamage((int)damage);
                baseController.ApplyKnockback(transform, knockbackPower, knockbackDuration);
            }
        }
    }
}
