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
        if (collision.CompareTag("PlayerFoot"))
        {
            BaseController player = collision.GetComponentInParent<BaseController>();
            if (player != null)
            {
                player.TakeDamage((int)damage);
                player.ApplyKnockback(transform, knockbackPower, knockbackDuration);
            }
        }
    }
}
