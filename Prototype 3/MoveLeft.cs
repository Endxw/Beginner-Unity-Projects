using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed = 20.0f;
    private float leftBound = -15;
    private PlayerController playerControllerScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the PlayerController script from the Player game object
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the game is not over
        if (playerControllerScript.gameOver == false)
        {
            // Move the object to the left
            transform.Translate(Vector3.left * speed * Time.deltaTime); 
        }

        // Check if the object has moved out of bounds
        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            // Destroy the obstacle if it goes out of bounds
            Destroy(gameObject);
        }

    }
}
