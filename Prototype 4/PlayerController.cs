using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 3.0f;
    private float powerupStrength = 15.0f;
    private Rigidbody playerRb;
    private GameObject focalPoint;
    public GameObject powerupIndicator;
    private Vector3 powerupOffset = new Vector3(0, -0.5f, 0);
    public bool hasPowerup = false;

    void Start()
    {
        // Get the Rigidbody component attached to the player and find the focal point object in the scene
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    void Update()
    {
        // Get vertical input (W/S keys or Up/Down arrows) and move the player accordingly
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * forwardInput * moveSpeed);
        powerupIndicator.transform.position = transform.position + powerupOffset;
    }

    IEnumerator PowerUpCountdown()
    {
        // Wait for 7 seconds before turning off the powerup
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player collides with a powerup
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            powerupIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            Debug.Log("Powerup collected!");
            StartCoroutine(PowerUpCountdown());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the player collides with an enemy while having a powerup
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            // Gets the Rigidbody component of the enemy and applies an impulse force away from the player
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position).normalized;

            enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            Debug.Log("Collided with '" + collision.gameObject.name + "'. With powerup set to " + hasPowerup + "with a strength of " + powerupStrength);
        }
    }
}
    
