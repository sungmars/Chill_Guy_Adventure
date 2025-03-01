using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossColliderController : MonoBehaviour
{
    public float rushAttackDamage = 10f;
    public float lifetime = 5f;
    public float knockbackPower = 5f;
    public float knockbackDuration = 0.2f;
    [SerializeField] Transform boss;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            BaseController player = collision.GetComponent<BaseController>();
            if (player != null)
            {
                player.TakeDamage((int)rushAttackDamage);
                player.ApplyKnockback(boss.transform, knockbackPower, knockbackDuration);
            }
        }
    }
}
