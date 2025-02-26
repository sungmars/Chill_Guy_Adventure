
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AreaSkillObject : MonoBehaviour
{
    // [SerializeField] private LayerMask wallLayer; // 벽 Layer
    [SerializeField] private LayerMask targetLayer; // 적 Layer
    private BaseController ownerBase;

    private SpriteRenderer spriteRenderer; // 스프라이트 색상

    private bool isReady; // Init 실행 후 Update 실행 될 수 있도록 함
    private float duration; // 최대 생존 시간
    private float currentDuration; // 현재 생존 시간
    private float damageInterval; // 데미지 입히는 간격 (0.5초?)

    private float knockbackPower; // 넉백 파워
    private float knockbackDuration; // 넉백 시간

    private Dictionary<Collider2D, Coroutine> activeCoroutines = new Dictionary<Collider2D, Coroutine>(); // 장판안에 들어 온 몬스터 목록들


    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (!isReady)
        {
            return;
        }

        currentDuration += Time.deltaTime;

        if (currentDuration > duration)
        {
            // 시간 지나면 물체 파괴
            DestroyObject(transform.position, createFx: false);
        }


    }

    // 초기 방향 값
    public void Init(BaseController ownerBase, Color color, float duration, float damageInterval, float knockbackPower, float knockbackDuration)
    {
        this.ownerBase = ownerBase;
        currentDuration = 0; // 생존 시간 0으로 초기화
        spriteRenderer.color = color; // 스프라이트 색상

        this.duration = duration;
        this.damageInterval = damageInterval;

        this.knockbackPower = knockbackPower;
        this.knockbackDuration = knockbackDuration;

        isReady = true; // Update 실행 될 수 있도록 함
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (targetLayer.Contain(collision.gameObject.layer)) // 지역에 들어옴 (Coroutine을 통해 damageInterval간격마다 데미지를 입힘)
        {
            if (!activeCoroutines.ContainsKey(collision))
            {
                Coroutine damageCoroutine = StartCoroutine(ApplyDamage(collision));
                activeCoroutines.Add(collision, damageCoroutine);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (targetLayer.Contain(collision.gameObject.layer)) // 지역에서 나감
        {
            if (activeCoroutines.TryGetValue(collision, out Coroutine coroutine))
            {
                StopCoroutine(coroutine);
                activeCoroutines.Remove(collision);
            }
        }
    }

    private IEnumerator ApplyDamage(Collider2D collision)
    {
        while (true)
        {
            // TODO : 적 공격

            if (collision == null || collision.gameObject == null)
            {
                yield break;
            }

            if (collision.TryGetComponent(out BaseController enemy))
            {
                enemy.TakeDamage((int)ownerBase.attack);
                enemy.ApplyKnockback(transform, knockbackPower, knockbackDuration);
            }
            yield return new WaitForSeconds(damageInterval);
        }
    }


    // Destroy 전 파티클 생성
    private void DestroyObject(Vector3 position, bool createFx)
    {
        if (createFx)
        {
            // ParticleManager.Instance.CreateImpactParticlesAtPostion(position);
        }

        Destroy(this.gameObject);
    }
}