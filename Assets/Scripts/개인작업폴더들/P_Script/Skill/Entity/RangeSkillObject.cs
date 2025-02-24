
using UnityEngine;

public class RangeSkillObject : MonoBehaviour
{
    [SerializeField] private LayerMask wallLayer; // �� Layer
    [SerializeField] private LayerMask targetLayer; // �� Layer

    private SpriteRenderer spriteRenderer; // ��������Ʈ ����
    private Transform pivot; // �ǹ� ȸ�� ��

    private bool isReady; // Init ���� �� Update ���� �� �� �ֵ��� ��
    private float duration; // ����ü �ִ� ���� �ð�
    private float currentDuration; // ���� ���� �ð�
    private Rigidbody2D _rigidbody; // ����ü ���� ���� velocity
    private Vector2 direction; // ����ü�� ���ư� ����
    private float speed; // ����ü�� ���ǵ�

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

    // �ʱ� ���� ��
    public void Init(Vector2 direction, Color color, float duration, float speed)
    {
        this.direction = direction; // ���� ���� ����
        currentDuration = 0; // ���� �ð� 0���� �ʱ�ȭ

        transform.right = this.direction; // ���� ����

        spriteRenderer.color = color; // ��������Ʈ ����

        if (this.direction.x < 0) // �ǹ� ȸ�� ��
            pivot.localRotation = Quaternion.Euler(180, 0, 0);
        else
            pivot.localRotation = Quaternion.Euler(0, 0, 0);

        this.duration = duration;
        this.speed = speed;
        isReady = true; // Update ���� �� �� �ֵ��� ��
    }

    // ����� Trigger�� �浹 ��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (wallLayer.Contain(collision.gameObject.layer)) // ���� �浹 �� �׳� �ı�
        {
            DestroyObject(collision.ClosestPoint(transform.position) - direction * .2f, fxOnDestroy);
        }
        else if (targetLayer.Contain(collision.gameObject.layer)) // ���� �浹 �� ��� �۵� �� �ı� 
        {
            // TODO : �� ����

            // �� ���� ���� ��ü �ı�
            DestroyObject(collision.ClosestPoint(transform.position), fxOnDestroy);
        }
    }

    // Destroy �� ��ƼŬ ����
    private void DestroyObject(Vector3 position, bool createFx)
    {
        if (createFx)
        {
            ParticleManager.Instance.CreateImpactParticlesAtPostion(position);
        }

        Destroy(this.gameObject);
    }
}