using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static EnemySpawner Instance;

    [Header("Enemy spawn settings")]

    public GameObject enemyPrefab;
    public Transform [] spawnPoints;

    [Header("Enemy spawn time settings")]
    public int enemySpawnCount;
    public float enemySpawnTime;
    
    [Header("Enemy count settings")]
    public int enemyCountMax;
    public int enemyCurrentCount;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        SpawnEnemy();

        //Repite el metodo, despues de x cantidad de segundos iniciales (2do param) y luego en un invertalo de x segundos (3er param)
        InvokeRepeating("SpawnEnemy", enemySpawnTime, 10);
    }

    void SpawnEnemy()
    {
        for (int i = 0; i < enemySpawnCount; i++)
        {
            if (enemyCurrentCount >= enemyCountMax)
            {
                return;
            }

            Transform targetSpawnPos = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(enemyPrefab, targetSpawnPos.position, targetSpawnPos.rotation);

            enemyCurrentCount++;
        }
    }

    public void EnemyDeath()
    { 
        enemyCurrentCount--;
    }

}
