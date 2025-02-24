
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSkillObject : MonoBehaviour
{
    // [SerializeField] private LayerMask wallLayer; // �� Layer
    [SerializeField] private LayerMask targetLayer; // �� Layer

    private SpriteRenderer spriteRenderer; // ��������Ʈ ����

    private bool isReady; // Init ���� �� Update ���� �� �� �ֵ��� ��
    private float duration; // �ִ� ���� �ð�
    private float currentDuration; // ���� ���� �ð�
    private float damageInterval; // ������ ������ ���� (0.5��?)

    private Dictionary<Collider, Coroutine> activeCoroutines = new Dictionary<Collider, Coroutine>(); // ���Ǿȿ� ��� �� ���� ��ϵ�


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
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (targetLayer.Contain(collision.gameObject.layer)) // ���� �浹 �� ��� �۵� �� �ı� 
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
        if (targetLayer.Contain(collision.gameObject.layer)) // ���� �浹 �� ��� �۵� �� �ı� 
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
            // TODO : �� ����
            Debug.Log($"Todo: {damageInterval}�� ���� �� ����!");
            yield return new WaitForSeconds(damageInterval);
        }
    }


    // Destroy �� ��ƼŬ ����
    private void DestroyObject(Vector3 position, bool createFx)
    {
        if (createFx)
        {
            // ParticleManager.Instance.CreateImpactParticlesAtPostion(position);
        }

        Destroy(this.gameObject);
    }
}