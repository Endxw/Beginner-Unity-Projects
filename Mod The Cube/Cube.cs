using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public MeshRenderer Renderer;

    private Material material;
    private Color baseColor;

    void Start()
    {
        transform.position = new Vector3(3, 4, 1);
        transform.localScale = Vector3.one * 1.3f;

        material = Renderer.material;

        // Pick a random color at start
        baseColor = new Color(Random.value, Random.value, Random.value, 0.4f);
        material.color = baseColor;
    }

    void Update()
    {
        transform.Rotate(100.0f * Time.deltaTime, 50.0f * Time.deltaTime, 75.0f * Time.deltaTime);
    }
}
