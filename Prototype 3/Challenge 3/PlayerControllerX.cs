using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver = false;

    private float floatForce = 10.0f;
    private float gravityModifier = 1.5f;
    private Rigidbody playerRb;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;
    public AudioClip bounceSound;
    private float heightLimit = 15.0f;
    private float gameHeightLimit = 20.0f;
    private bool allowFloat;
    private float outOfBoundsLower = -0.5f;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();

        // Apply a small upward force at the start of the game
        playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > heightLimit)
        {
            allowFloat = true;
        } else
        {
            allowFloat = false;
        }
        // While space is pressed and player is low enough, float up
        if (Input.GetKey(KeyCode.Space) && !gameOver && allowFloat == false)
        {
            playerRb.AddForce(Vector3.up * floatForce);
        }

        if (transform.position.y < outOfBoundsLower)
        {
            transform.position = new Vector3(transform.position.x, outOfBoundsLower + 1, transform.position.z);
            playerRb.AddForce(Vector3.up * floatForce * 100.0f);
            playerAudio.PlayOneShot(bounceSound, 1.0f);
        }

        if (transform.position.y > gameHeightLimit && !gameOver)
        {
            gameOver = true;
            Debug.Log("Game Over! You flew too high!");
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
        } 

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);

        }

    }

}
