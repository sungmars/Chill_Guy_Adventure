using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "0 Stage", menuName = "Scriptable Object/EnemySpawnData", order = int.MaxValue)]
public class EnemySpawnData : ScriptableObject
{
    public WaveData[] waveDatas;
}

[System.Serializable]
public class WaveData
{
    public string name;
    public Vector2Int MobRange;
    public int enemyCount;
}