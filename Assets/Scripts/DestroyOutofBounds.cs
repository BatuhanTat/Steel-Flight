using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutofBounds : MonoBehaviour
{
    private float boundaryX = 30.0f;
    private float boundaryY = 15.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < -boundaryX || transform.position.x > boundaryX)
        {
            Destroy(gameObject);
        }
        else if(transform.position.y < -boundaryY || transform.position.y > boundaryY)
        {
            Destroy(gameObject);
        }
        
    }
}
