using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public GameObject[] enemyTypes = new GameObject[3]; 

    [Header("Spawn Points")]
    public Transform[] spawnPoints = new Transform[3]; 

    [Header("Spawn Settings")]
    public float spawnInterval = 2f;
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnEnemies();
            timer = 0f;
        }
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < 3; i++) // 3 slots
        {
            if (enemyTypes[i] != null && spawnPoints[i] != null)
            {
                Instantiate(enemyTypes[i], spawnPoints[i].position, Quaternion.identity);
            }
        }
    }
}
