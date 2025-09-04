using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    private AudioSource playerAudio;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    private float jumpForce = 830.0f;
    private float gravityModifier = 2.0f;
    private bool isOnGround = true;
    public bool gameOver = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRb = GetComponent<Rigidbody>(); // Get the Rigidbody component
        playerAnim = GetComponent<Animator>(); // Get the Animator component
        Physics.gravity *= gravityModifier; // Modify the gravity
        playerAudio = GetComponent<AudioSource>(); // Get the AudioSource component

    }

    // Update is called once per frame
    void Update()
    {
        // Jump when space is pressed and the player is on the ground
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Apply an upward force to the Rigidbody
            playerAudio.PlayOneShot(jumpSound, 1.0f); // Play the jump sound
            isOnGround = false; // Set isOnGround to false
            playerAnim.SetTrigger("Jump_trig"); // Trigger the jump animation
            dirtParticle.Stop(); // Stop the dirt particle effect
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the player collides with the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true; // Set isOnGround to true
            dirtParticle.Play(); // Play the dirt particle effect
        }

        // Check if the player collides with an obstacle
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over!"); // Log game over message
            playerAudio.PlayOneShot(crashSound, 1.0f); // Play the crash sound
            dirtParticle.Stop(); // Stop the dirt particle effect
            explosionParticle.Play(); // Play the explosion particle effect
            playerAnim.SetBool("Death_b", true); // Trigger the death animation
            playerAnim.SetInteger("DeathType_int", 1); // Set the death type
            gameOver = true; // Set gameOver to true
        }
    }
}
