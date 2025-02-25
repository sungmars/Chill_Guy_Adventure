
using UnityEngine;

public class RangeSkillObject : MonoBehaviour
{
    [SerializeField] private LayerMask wallLayer; // 벽 Layer
    [SerializeField] private LayerMask targetLayer; // 적 Layer
    private BaseController ownerBase;

    private SpriteRenderer spriteRenderer; // 스프라이트 색상
    private Transform pivot; // 피벗 회전 값

    private bool isReady; // Init 실행 후 Update 실행 될 수 있도록 함
    private float duration; // 투사체 최대 생존 시간
    private float currentDuration; // 현재 생존 시간
    private Rigidbody2D _rigidbody; // 투사체 물리 적용 velocity
    private Vector2 direction; // 투사체가 날아갈 방향
    private float speed; // 투사체의 스피드


    private float knockbackPower; // 넉백 파워
    private float knockbackDuration; // 넉백 시간


    public bool fxOnDestroy = true;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _rigidbody = gameObject.GetOrAddComponent<Rigidbody2D>();
        pivot = transform.GetChild(0);
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
            DestroyObject(transform.position, createFx: false);
        }

        _rigidbody.velocity = direction * speed;
    }

    // 초기 방향 값
    public void Init(BaseController ownerBase, Vector2 direction, Color color, float duration, float speed, float knockbackPower, float knockbackDuration)
    {
        this.ownerBase = ownerBase;
        this.direction = direction; // 날라갈 방향 설정
        currentDuration = 0; // 생존 시간 0으로 초기화

        transform.right = this.direction; // 방향 설정

        spriteRenderer.color = color; // 스프라이트 색상

        if (this.direction.x < 0) // 피벗 회전 값
            pivot.localRotation = Quaternion.Euler(180, 0, 0);
        else
            pivot.localRotation = Quaternion.Euler(0, 0, 0);

        this.duration = duration;
        this.knockbackPower = knockbackPower;
        this.knockbackDuration = knockbackDuration;
        this.speed = speed;
        isReady = true; // Update 실행 될 수 있도록 함
    }

    // 어딘가의 Trigger에 충돌 시
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (wallLayer.Contain(collision.gameObject.layer)) // 벽과 충돌 시 그냥 파괴
        {
            DestroyObject(collision.ClosestPoint(transform.position) - direction * .2f, fxOnDestroy);
        }
        else if (targetLayer.Contain(collision.gameObject.layer)) // 적과 충돌 시 기능 작동 후 파괴 
        {
            // TODO : 적 공격

            BaseController enemy = collision.GetComponent<BaseController>();
            if (enemy != null)
            {
                enemy.TakeDamage((int)ownerBase.attack);
                enemy.ApplyKnockback(transform, knockbackPower, knockbackDuration);
            }
            // 적 공격 이후 물체 파괴
            DestroyObject(collision.ClosestPoint(transform.position), fxOnDestroy);
        }
    }

    // Destroy 전 파티클 생성
    private void DestroyObject(Vector3 position, bool createFx)
    {
        if (createFx)
        {
            ParticleManager.Instance.CreateImpactParticlesAtPostion(position);
        }

        Destroy(this.gameObject);
    }
}