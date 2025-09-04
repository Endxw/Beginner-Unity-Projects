using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public float rotationSpeed = 50.0f;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        // Get horizontal input (A/D keys or Left/Right arrows) and rotate the camera accordingly 
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.down, horizontalInput * rotationSpeed * Time.deltaTime);

    }
}
