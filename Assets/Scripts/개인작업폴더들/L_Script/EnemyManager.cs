using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject meleeEnemyPrefab;
    public GameObject rangedEnemyPrefab;
    public Transform[] spawnPoints;
    public float spawnInterval = 5f;
    private float nextSpawnTime;

    private void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    private void SpawnEnemy()
    {
        if (spawnPoints.Length == 0) return;

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject enemyPrefab = Random.Range(0, 2) == 0 ? meleeEnemyPrefab : rangedEnemyPrefab;
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    }
}