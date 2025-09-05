using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float speed = 1.0f;
    private Rigidbody enemyRb;
    private GameObject player;
    
    void Start()
    {
        // Get the Rigidbody component attached to the enemy and find the player object in the scene
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    
    void Update()
    {
        // Calculate the direction towards the player and move the enemy in that direction
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed);

        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}
