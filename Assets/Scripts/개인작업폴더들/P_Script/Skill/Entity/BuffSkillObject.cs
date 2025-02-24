
using UnityEngine;

public class BuffSkillObject : MonoBehaviour
{
    // [SerializeField] private LayerMask wallLayer; // �� Layer
    // [SerializeField] private LayerMask targetLayer; // �� Layer

    private SpriteRenderer spriteRenderer; // ��������Ʈ ����

    private bool isReady; // Init ���� �� Update ���� �� �� �ֵ��� ��
    private float duration; // �ִ� ���� �ð�
    private float currentDuration; // ���� ���� �ð�


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
            // �ð� ������ ��ü �ı�
            DestroyObject(transform.position, createFx: false);
        }

    }

    // �ʱ� ���� ��
    public void Init(Color color, float duration)
    {
        currentDuration = 0; // ���� �ð� 0���� �ʱ�ȭ
        spriteRenderer.color = color; // ��������Ʈ ����

        this.duration = duration;
        isReady = true; // Update ���� �� �� �ֵ��� ��
        // TODO : ��������
    }

    // Destroy �� ��ƼŬ ����
    private void DestroyObject(Vector3 position, bool createFx)
    {
        // TODO : ��������
        if (createFx)
        {
            // ParticleManager.Instance.CreateImpactParticlesAtPostion(position);
        }

        Destroy(this.gameObject);
    }
}