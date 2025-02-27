using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundWaveAttackController : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private WeaponHandler WeaponPrefab;
    [SerializeField] private Transform weaponPivot;
    [SerializeField] private BaseController boss;

    private Coroutine coroutine;
    public void PublicSoundWaveAttack(Transform player)
    {
        SoundWaveAttack(player);
    }

    private void SoundWaveAttack(Transform player)
    {
        GameObject projectile = Instantiate(projectilePrefab, weaponPivot.position, weaponPivot.rotation);
        projectile.transform.SetParent(transform);

        var projectileController = projectile.GetComponent<EnemyProjectile>();
        projectileController.Init(boss);

        projectile.name = "SoundWave";
        // 플레이어 방향 계산
        Vector2 direction = (player.position - weaponPivot.position).normalized;
        // 발사체의 회전을 플레이어 방향에 맞게 조정
        projectile.transform.up = direction;
        // 속도 부여
        Rigidbody2D arrowRb = projectile.GetComponent<Rigidbody2D>();
        if (arrowRb != null)
        {
            arrowRb.velocity = direction * 3f;
        }
        coroutine = StartCoroutine(SoundWaveSetSize(projectile));
    }

    private IEnumerator SoundWaveSetSize(GameObject projectile)
    {
        BoxCollider2D boxCollider2D = projectile.GetComponent<BoxCollider2D>();
        float x = 3;
        Vector2 objSize = projectile.transform.localScale;
        Vector2 boxSize = boxCollider2D.size;
        while (projectile.transform.localScale.x < x)
        {
            objSize += objSize * Time.deltaTime;
            projectile.transform.localScale = new Vector2(objSize.x, objSize.y);

            Vector2 ratio = projectile.transform.localScale / objSize;
            boxCollider2D.size = boxSize * ratio;
            yield return null;
        }
        yield return null;
    }

    public void StopSoundWaveCoroutine()
    {
        StopCoroutine(coroutine);
    }
}
