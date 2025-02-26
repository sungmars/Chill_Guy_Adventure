using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawnObject : MonoBehaviour
{
    GameObject baseController;
    [SerializeField] Image timerImage;

    private bool isReady; // Init 실행 후 Update 실행 될 수 있도록 함
    private float duration; // 최대 생존 시간
    private float currentDuration; // 현재 생존 시간

    private void Update()
    {
        if (!isReady)
        {
            return;
        }

        currentDuration += Time.deltaTime;

        timerImage.fillAmount = 1 - currentDuration / duration;
        if (currentDuration > duration)
        {
            // 시간 지나면 물체 파괴
            DestroyObject(transform.position, createFx: false);
        }
    }

    // 초기 방향 값
    public void Init(GameObject baseController, float duration)
    {
        this.baseController = baseController;
        currentDuration = 0; // 생존 시간 0으로 초기화

        this.duration = duration;

        isReady = true; // Update 실행 될 수 있도록 함
    }

    // Destroy 전에 적 생성
    private void DestroyObject(Vector3 position, bool createFx)
    {
        if (createFx)
        {
            // ParticleManager.Instance.CreateImpactParticlesAtPostion(position);
        }
        Instantiate(baseController, position, Quaternion.identity);

        Destroy(this.gameObject);
    }
}
