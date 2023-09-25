using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public float speed = 20.0f;
    [SerializeField] public float damage = 1.0f;
    
    [field: SerializeField] public bool isEnemyProjectile{get; private set;}

    private Rigidbody projectileRb;

    // Start is called before the first frame update
    void Start()
    {
        projectileRb = GetComponent<Rigidbody>();
        if (gameObject.CompareTag("Bullet"))
        {
            if(isEnemyProjectile)
            {
                projectileRb.velocity = new Vector3(-15.0f, 0, 0);
            }
            else
            {
                projectileRb.velocity = new Vector3(15.0f, 0, 0);
            }
        }
    }

    void FixedUpdate()
    {
        if (gameObject.CompareTag("Rocket"))
        {
            projectileRb.AddForce(Vector3.right * speed * Time.deltaTime, ForceMode.Impulse);
        }
    }
}
