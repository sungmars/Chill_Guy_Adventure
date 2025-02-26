using UnityEngine;

public class RandomItemSpawner : MonoBehaviour
{
    public GameObject itemPrefab; // 생성할 아이템 프리팹
    public int numberOfItems = 10; // 생성할 아이템 개수
    public Vector3 spawnAreaMin; // 아이템 생성 영역의 최소 좌표
    public Vector3 spawnAreaMax; // 아이템 생성 영역의 최대 좌표

    void Start()
    {
        SpawnItems();
    }

    void SpawnItems()
    {
        for (int i = 0; i < numberOfItems; i++)
        {
            // 무작위 위치 생성
            float x = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
            float y = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
            float z = Random.Range(spawnAreaMin.z, spawnAreaMax.z);
            Vector3 randomPosition = new Vector3(x, y, z);

            // 아이템 생성
            Instantiate(itemPrefab, randomPosition, Quaternion.identity);
        }
    }
}