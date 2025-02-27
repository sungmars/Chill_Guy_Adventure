using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChillDogAttackController : MonoBehaviour
{
    [SerializeField] private GameObject chillDogPrefab;
    [SerializeField] private GameObject enemySpawnObject;

    public void PublicCreateChillDog()
    {
        CreateChillDog();
    }

    private void CreateChillDog()
    {
        EnemySpawnObject spawnObject;
        GameObject chillDog;
        for (int i = 0; i < 5; i++)
        {
            float x = Random.Range(-7f, 7.1f);
            float y = Random.Range(-2.5f, 2.6f);
            spawnObject = Instantiate(enemySpawnObject, new Vector3(x, y), Quaternion.identity).GetComponent<EnemySpawnObject>();
            spawnObject.Init(chillDogPrefab, 2f);
            //chillDog.name = $"ChillDog {i}";
            //chillDog.transform.position = new Vector2(x, y);
        }
    }
}
