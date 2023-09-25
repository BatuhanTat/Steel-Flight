using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private float length;
    private Vector3 startPos;
    public float parallaxEffect;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MoveBackground();
        RepeatBackground();
    }

    private void MoveBackground()
    {
        transform.Translate(Vector3.left * Time.deltaTime * parallaxEffect);
    }

    private void RepeatBackground()
    {
        if (transform.position.x < startPos.x - 51) 
        {
            transform.position = startPos;
        }
    }
}
