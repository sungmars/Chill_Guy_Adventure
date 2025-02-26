using UnityEngine;

public class RandomItemSpawner : MonoBehaviour
{
    public GameObject itemPrefab; // ������ ������ ������
    public int numberOfItems = 10; // ������ ������ ����
    public Vector3 spawnAreaMin; // ������ ���� ������ �ּ� ��ǥ
    public Vector3 spawnAreaMax; // ������ ���� ������ �ִ� ��ǥ

    void Start()
    {
        SpawnItems();
    }

    void SpawnItems()
    {
        for (int i = 0; i < numberOfItems; i++)
        {
            // ������ ��ġ ����
            float x = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
            float y = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
            float z = Random.Range(spawnAreaMin.z, spawnAreaMax.z);
            Vector3 randomPosition = new Vector3(x, y, z);

            // ������ ����
            Instantiate(itemPrefab, randomPosition, Quaternion.identity);
        }
    }
}