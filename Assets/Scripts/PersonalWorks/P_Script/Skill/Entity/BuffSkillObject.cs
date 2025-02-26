
using UnityEngine;

public class BuffSkillObject : MonoBehaviour
{
    // [SerializeField] private LayerMask wallLayer; // 벽 Layer
    // [SerializeField] private LayerMask targetLayer; // 적 Layer

    private SpriteRenderer spriteRenderer; // 스프라이트 색상
    private BaseController ownerBase;

    private bool isReady; // Init 실행 후 Update 실행 될 수 있도록 함
    private float duration; // 최대 생존 시간
    private float currentDuration; // 현재 생존 시간


    private float knockbackPower; // 넉백 파워
    private float knockbackDuration; // 넉백 시간


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
    public void Init(BaseController ownerBase, Color color, float duration, float knockbackPower, float knockbackDuration)
    {
        this.ownerBase = ownerBase;
        currentDuration = 0; // 생존 시간 0으로 초기화
        spriteRenderer.color = color; // 스프라이트 색상

        this.duration = duration;
        this.knockbackPower = knockbackPower;
        this.knockbackDuration = knockbackDuration;
        isReady = true; // Update 실행 될 수 있도록 함
        // TODO : 버프적용
    }

    // Destroy 전 파티클 생성
    private void DestroyObject(Vector3 position, bool createFx)
    {
        // TODO : 버프꺼짐
        if (createFx)
        {
            // ParticleManager.Instance.CreateImpactParticlesAtPostion(position);
        }

        Destroy(this.gameObject);
    }
}