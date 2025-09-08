using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed = 5.0f;
    private float leftBound = -15.0f;


    void Start()
    {
        
    }


    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed);

        if (transform.position.x < leftBound)
        {
            Destroy(gameObject);
        }
    }
}
