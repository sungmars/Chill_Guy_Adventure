using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoSingleton<EnemySpawnManager>
{
    public PlayerController player;

    [SerializeField]
    private List<GameObject> enemyPrefabs; // 생성할 적 프리팹 리스트

    [SerializeField]
    private List<Rect> spawnAreas; // 적을 생성할 영역 리스트

    [SerializeField]
    private Color gizmoColor = new Color(1, 0, 0, 0.3f); // 기즈모 색상

    [SerializeField]
    private GameObject enemySpawnObject; // 생성할 적 프리팹 리스트

    private List<EnemyController> activeEnemies = new List<EnemyController>(); // 현재 활성화된 적들

    public bool enemySpawnComplite;

    [SerializeField] private EnemySpawnData enemySpawnData;
    private bool isWaveEnd = false;

    [SerializeField] private float timeBetweenSpawns = 0.2f;
    [SerializeField] private float timeBetweenWaves = 1f;

    public void Start()
    {
        StartWave();
    }

    public void StartWave()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        StartCoroutine(SpawnWave(enemySpawnData));
    }

    private IEnumerator SpawnWave(EnemySpawnData spawnData)
    {
        var waveDatas = spawnData.waveDatas;

        // 웨이브 하나하나 생성
        foreach (var waveData in waveDatas)
        {
            isWaveEnd = false;
            // 모든 몹 생성
            for (int i = 0; i < waveData.enemyCount; i++)
            {
                int randomMob = Random.Range(waveData.MobRange.x, waveData.MobRange.y);
                SpawnRandomEnemy(randomMob);
            }

            // 웨이브가 끝날 때 까지 대기
            while (!isWaveEnd)
            {
                yield return new WaitForSeconds(1f);
            }
        }

        // TODO 모든 웨이브가 끝났을 때 다음 스테이지로
    }

    private void SpawnRandomEnemy(int _mobNum)
    {
        if (enemyPrefabs.Count == 0 || spawnAreas.Count == 0)
        {
            Debug.LogWarning("Enemy Prefabs 또는 Spawn Areas가 설정되지 않았습니다.");
            return;
        }

        // 랜덤한 적 프리팹 선택
        GameObject randomPrefab = enemyPrefabs[_mobNum];

        // 랜덤한 영역 선택
        Rect randomArea = spawnAreas[Random.Range(0, spawnAreas.Count)];

        // Rect 영역 내부의 랜덤 위치 계산
        Vector2 randomPosition = new Vector2(
            Random.Range(randomArea.xMin, randomArea.xMax),
            Random.Range(randomArea.yMin, randomArea.yMax)
        );

        // 적 생성 및 리스트에 추가
        EnemySpawnObject spawnObject = Instantiate(enemySpawnObject, new Vector3(randomPosition.x, randomPosition.y), Quaternion.identity).GetComponent<EnemySpawnObject>();
        spawnObject.Init(randomPrefab, 2f);
    }

    // 기즈모를 그려 영역을 시각화 (선택된 경우에만 표시)
    private void OnDrawGizmosSelected()
    {
        if (spawnAreas == null) return;

        Gizmos.color = gizmoColor;
        foreach (var area in spawnAreas)
        {
            Vector3 center = new Vector3(area.x + area.width / 2, area.y + area.height / 2);
            Vector3 size = new Vector3(area.width, area.height);
            Gizmos.DrawCube(center, size);
        }
    }

    // 적이 생성 될 때 무조건 실행
    public void CreateEnemyOnInit(EnemyController enemy)
    {
        activeEnemies.Add(enemy);
    }

    // 적이 죽을 때 무조건 실행
    public void RemoveEnemyOnDeath(EnemyController enemy)
    {
        activeEnemies.Remove(enemy);
        if (activeEnemies.Count == 0)
            isWaveEnd = true; // 라운드가 끝났다고 알림(새 라운드 시작)
    }
}
