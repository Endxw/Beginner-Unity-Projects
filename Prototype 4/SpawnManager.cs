using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;
    private float spawnRange= 9.0f;
    private int enemyCount;
    private int waveNumber = 1;



    void Start()
    {
        SpawnEnemyWave(waveNumber);
        Instantiate(powerupPrefab, GenerateSpawnPos(), powerupPrefab.transform.rotation);
    }

        
    void Update()
    {
        // Check the number of enemies currently in the scene and spawn a new wave if there are none left
        enemyCount = FindObjectsByType<Enemy>(FindObjectsSortMode.None).Length;

        if (enemyCount == 0)
        {
            // Increment the wave number and spawn a new wave of enemies along with a powerup
            waveNumber++;
            SpawnEnemyWave(waveNumber);
            Instantiate(powerupPrefab, GenerateSpawnPos(), powerupPrefab.transform.rotation);
        }
    }

    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        // Spawn 3 enemies at random positions within the spawn range
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPos(), enemyPrefab.transform.rotation);
        }
    }

    private Vector3 GenerateSpawnPos()
    {
        // Generate a random position within the defined spawn range
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        // Create a Vector3 for the spawn position, keeping the y-coordinate at 0
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);

        return randomPos; // Return the generated random position
    }
}
