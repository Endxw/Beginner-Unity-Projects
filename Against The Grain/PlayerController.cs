using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public bool hasPowerup = false;
    public bool isGameOver = false;
    public int powerupDuration = 10;
    public float rotationSpeed = 180f; // Degrees per second

    private Rigidbody playerRb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRb = GetComponent<Rigidbody>(); // Get the Rigidbody component attached to the player
    }

    // Update is called once per frame
    void Update()
    {
        // call methods that handle player movement and check for out-of-bounds
        MovePlayer();
        OutOfBounds();

        // Always rotate forward
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(powerupDuration); // Wait for 7 seconds
        hasPowerup = false; // Set hasPowerup to false after the countdown
    }

    private void MovePlayer()
    {
        // Handle player movement based on input
        float speed = 10.0f;
        float jumpForce = 10.0f;

        float verticalInput = Input.GetAxis("Vertical"); // Get vertical input (W/S or Up/Down arrow keys)
        Vector3 velocity = playerRb.linearVelocity; // Get current velocity
        velocity.z = verticalInput * speed; // Set z-axis velocity based on input and speed
        playerRb.linearVelocity = velocity; // Apply the modified velocity back to the Rigidbody
    }

    private void OutOfBounds()
    {
        // Reset player position to the center if out of bounds
        float outOfBoundsZ = 5.0f;

        if (transform.position.z < -outOfBoundsZ)
        {
            Vector3 pos = transform.position;
            pos.z = -(outOfBoundsZ) + 0.5f;
            transform.position = pos;
        }
        else if (transform.position.z > outOfBoundsZ)
        {
            Vector3 pos = transform.position;
            pos.z = outOfBoundsZ - 0.5f;
            transform.position = pos;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (hasPowerup)
            {
                Destroy(collision.gameObject); // Destroy the enemy object
                Debug.Log("Enemy destroyed with powerup!");
                return; // Exit the method to avoid destroying the player
            }
            else
            {
                Destroy(gameObject); // Destroy the player object
                Debug.Log("Game Over!");
                isGameOver = true; // Set isGameOver to true
            }
        }

        if (collision.gameObject.CompareTag("Powerup"))
        {
            Destroy(collision.gameObject); // Destroy the powerup object
            hasPowerup = true; // Set hasPowerup to true
            Debug.Log("Powerup collected!");
            StartCoroutine(PowerupCountdownRoutine()); // Start the powerup countdown
        }
    }
}
