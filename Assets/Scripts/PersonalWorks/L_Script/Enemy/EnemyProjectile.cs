using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float damage = 10f;
    public float lifetime = 5f;
    public float knockbackPower = 5f;
    public float knockbackDuration = 0.2f;
    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            BaseController player = collision.GetComponent<BaseController>();
            if (player != null)
            {
                player.TakeDamage((int)damage);
                player.ApplyKnockback(transform, knockbackPower, knockbackDuration);
            }
            if(transform.name == "SoundWave")
            {
                BossController boss = transform.GetComponentInParent<BossController>();
                boss.StopSoundWaveCoroutine();
            }
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
