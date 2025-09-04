using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatWidth;


    void Start()
    {
        // Record the starting position of the background
        startPos = transform.position;
        repeatWidth = GetComponent<BoxCollider>().size.x / 2; // Get half the width of the BoxCollider
    }

    void Update()
    {
        // If the background has moved left past the repeat width, reset its position
        if (transform.position.x < startPos.x - repeatWidth)
        {
            transform.position = startPos;
        }
    }
}
