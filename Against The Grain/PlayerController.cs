using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float gravityModifier;
    private Rigidbody playerRb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRb = GetComponent<Rigidbody>(); // Get the Rigidbody component attached to the player
        Physics.gravity *= gravityModifier; // Modify the global gravity by the gravityModifier
    }

    // Update is called once per frame
    void Update()
    {
        // call methods that handle player movement and check for out-of-bounds
        MovePlayer();
        OutOfBounds();
    }

    private void MovePlayer()
    {
        // Handle player movement based on input
        float speed = 10.0f;
        float jumpForce = 10.0f;


        float horizontalInput = Input.GetAxis("Horizontal"); // Get horizontal input (A/D or Left/Right arrow keys)
        Vector3 velocity = playerRb.linearVelocity; // Get current velocity
        velocity.x = horizontalInput * speed; // Set horizontal velocity based on input and speed
        playerRb.linearVelocity = velocity; // Apply the modified velocity back to the Rigidbody

        // Jump when spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void OutOfBounds()
    {
        // Reset player position to the center if out of bounds
        Vector3 Center = new Vector3(0, 0.5f, -0.5f);
        float outOfBoundsX = 25.0f;

        if (transform.position.x < -outOfBoundsX || transform.position.x > outOfBoundsX)
        {
            transform.position = Center;
        }
    }
}
