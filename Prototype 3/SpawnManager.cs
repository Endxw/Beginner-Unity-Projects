using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject obstaclePrefab;
    private Vector3 spawnPos = new Vector3(30, 0, 0);
    private float startDelay = 2.0f;
    public float repeatRate = 2.0f;
    private PlayerController playerControllerScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the PlayerController script from the Player game object
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        
        // Repeatedly call the SpawnObstacle method
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // SpawnObstacle spawns an obstacle
    void SpawnObstacle()
    {
        // Check if the game is not over
        if (playerControllerScript.gameOver == false)
        {
            // Create an obstacle at the spawn position
            Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
        }
        
    }
}
