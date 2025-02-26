using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    // public float damage = 10f;
    public float lifetime = 5f;
    BaseController baseController;
    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    public void Init(BaseController baseController)
    {
        this.baseController = baseController;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            BaseController player = collision.GetComponent<BaseController>();
            if (player != null)
            {
                player.TakeDamage((int)baseController.attack);
                player.ApplyKnockback(transform, baseController.knockbackPower, baseController.knockbackTime);
            }
            if (transform.name == "SoundWave")
            {
                SoundWaveAttackController soundWaveAttackController = GetComponentInParent<SoundWaveAttackController>();
                soundWaveAttackController.StopSoundWaveCoroutine();
            }
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
