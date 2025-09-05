using UnityEngine;

public class SpinPropeller : MonoBehaviour
{
    private float rotationSpeed = 360f; // Degrees per second

    // Update is called once per frame
    void Update()
    {
        // Rotate around Z axis
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
