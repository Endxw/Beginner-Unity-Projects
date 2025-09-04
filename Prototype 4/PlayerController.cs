using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    private Rigidbody playerRb;
    private GameObject focalPoint;

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
    }
}
    