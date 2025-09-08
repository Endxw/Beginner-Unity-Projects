using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    // Array of enemy prefabs to spawn randomly
    public GameObject[] enemyPrefabs;
    // Powerup prefab to spawn
    public GameObject powerupPrefab;

    // Fixed X position for spawning objects
    private float xSpawnPos = 12.0f;
    // Range for random Z position when spawning
    private float zSpawnRange = 5;

    // List to keep track of all spawned enemy instances
    private List<GameObject> spawnedEnemies = new List<GameObject>();

    // Minimum distance between powerup and any enemy (adjust as needed)
    private float minDistance = 1.0f;

    // Reference to the PlayerController to check game over state
    private PlayerController playerController;

    void Start()
    {
        // Find the PlayerController in the scene
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        do
        {
            StartCoroutine(SpawnEnemyRoutine());
            StartCoroutine(SpawnPowerupRoutine()); // Start spawning powerups
        }
        while (!playerController.isGameOver);
    }

    // Coroutine that spawns an enemy every 3 seconds, stops if game is over
    IEnumerator SpawnEnemyRoutine()
    {
        if (playerController.isGameOver == false)
        {
            yield return new WaitForSeconds(3f); // Wait for 3 seconds
            SpawnEnemy(); // Spawn a new enemy
        }
        else
        {
            yield break; // Exit the coroutine if the game is over
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        if (playerController.isGameOver == false)
        {
            yield return new WaitForSeconds(15f); // Wait for 15 seconds
            SpawnPowerup(); // Spawn a new powerup
        }
        else
        {
            yield break; // Exit the coroutine if the game is over
        }
    }

    // Spawns a random enemy from the enemyPrefabs array
    void SpawnEnemy()
    {
        // Select a random enemy prefab and instantiate it at a generated position
        GameObject selectedEnemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        Vector3 enemyPos = SpawnGeneration();
        GameObject enemyInstance = Instantiate(selectedEnemyPrefab, enemyPos, selectedEnemyPrefab.transform.rotation);
        spawnedEnemies.Add(enemyInstance);
    }

    // Spawns a powerup at a position that does not overlap any existing enemy
    void SpawnPowerup()
    {
        Vector3 powerupPos;
        bool overlaps;

        // Try to find a position that does not overlap any enemy's Z position
        do
        {
            powerupPos = SpawnGeneration();
            overlaps = false;

            foreach (GameObject enemy in spawnedEnemies)
            {
                if (enemy == null) continue; // Skip destroyed enemies

                float enemyZ = enemy.transform.position.z;
                if (Mathf.Abs(powerupPos.z - enemyZ) < minDistance)
                {
                    overlaps = true;
                    break; // Overlap found, try another position
                }
            }
        }
        while (overlaps);

        Instantiate(powerupPrefab, powerupPos, powerupPrefab.transform.rotation);
    }

    // Generates a spawn position for an object
    Vector3 SpawnGeneration()
    {
        float xPos = xSpawnPos; // Fixed X position
        float yPos = 0.5f;      // Fixed Y position (adjust as needed)
        float zPos = Random.Range(-zSpawnRange, zSpawnRange); // Random Z position within range

        return new Vector3(xPos, yPos, zPos);
    }
}