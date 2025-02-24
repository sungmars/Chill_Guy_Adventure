
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSkillObject : MonoBehaviour
{
    // [SerializeField] private LayerMask wallLayer; // 벽 Layer
    [SerializeField] private LayerMask targetLayer; // 적 Layer

    private SpriteRenderer spriteRenderer; // 스프라이트 색상

    private bool isReady; // Init 실행 후 Update 실행 될 수 있도록 함
    private float duration; // 최대 생존 시간
    private float currentDuration; // 현재 생존 시간
    private float damageInterval; // 데미지 입히는 간격 (0.5초?)

    private Dictionary<Collider, Coroutine> activeCoroutines = new Dictionary<Collider, Coroutine>(); // 장판안에 들어 온 몬스터 목록들


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
    public void Init(Color color, float duration)
    {
        currentDuration = 0; // 생존 시간 0으로 초기화
        spriteRenderer.color = color; // 스프라이트 색상

        this.duration = duration;
        isReady = true; // Update 실행 될 수 있도록 함
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (targetLayer.Contain(collision.gameObject.layer)) // 적과 충돌 시 기능 작동 후 파괴 
        {

            if (!activeCoroutines.ContainsKey(collision))
            {
                Coroutine damageCoroutine = StartCoroutine(ApplyDamage(collision));
                activeCoroutines.Add(collision, damageCoroutine);
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (targetLayer.Contain(collision.gameObject.layer)) // 적과 충돌 시 기능 작동 후 파괴 
        {

            if (activeCoroutines.TryGetValue(collision, out Coroutine coroutine))
            {
                StopCoroutine(coroutine);
                activeCoroutines.Remove(collision);
            }
        }
    }

    private IEnumerator ApplyDamage(Collider target)
    {
        while (true)
        {
            // TODO : 적 공격
            Debug.Log($"Todo: {damageInterval}초 마다 적 공격!");
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