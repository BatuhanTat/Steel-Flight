using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("General")]
    [SerializeField] float speed;
    [SerializeField] float health = 2.0f;
    [SerializeField] float basefiringRate = 0.6f;
    private bool isAlive = true;

    [Header("AI")]
    [SerializeField] float firingRateVariance = 0.2f;
    [SerializeField] float minimumFiringRate = 0.4f;

    public GameObject[] projectilePrefabs;

    [field: SerializeField]
    public float enemy_scoreValue { get; private set; }
    private Rigidbody enemyRb;
    private PlayerController playerScript;
    private bool canShoot = true;
    //private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        //gameManager = GameObject.FindGameObjectWithTag("GameManager").gameObject.GetComponent<GameManager>();
        if (GameManager.instance.isGameActive)
        {
            playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }
        enemyRb = GetComponent<Rigidbody>();
        enemyRb.velocity = new Vector3(-3.0f, 0, 0);
    }

    private void Update()
    {
        Enemy_Fire();
    }

    void FixedUpdate()
    {
        if (isAlive)
        {
            enemyRb.AddForce(Vector3.left * speed * Time.deltaTime);
        }
    }

    private void Enemy_Fire()
    {
        if (canShoot)
        {
            GameObject instance = null;
            soundFX_Manager.instance.PlayShootingBullet_Clip(gameObject.tag);
            instance = Instantiate(projectilePrefabs[0], transform.position, projectilePrefabs[0].transform.rotation);
            StartCoroutine(Enemy_FireDelay());
            canShoot = false;
        }
    }

    IEnumerator Enemy_FireDelay()
    {
        float timeToNextProjectile = Random.Range(basefiringRate - firingRateVariance,
                                                   basefiringRate + firingRateVariance);
        timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minimumFiringRate, float.MaxValue);
        yield return new WaitForSeconds(timeToNextProjectile);
        canShoot = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Rocket") || other.gameObject.CompareTag("Bullet"))
        {
            if (other.gameObject.CompareTag("Bullet"))
            {
                soundFX_Manager.instance.PlayBulletCollision_Clip();
                VisualFX_Manager.instance.PlayHitEffect(transform.position);
            }
            else if( other.gameObject.CompareTag("Rocket"))
            {
                soundFX_Manager.instance.PlayRocketCollision_Clip();
                VisualFX_Manager.instance.PlayExplosionVFX(transform.position);
            }
            DecreaseHealth(other.gameObject.GetComponent<ProjectileMovement>().damage);
            //Debug.Log("Enemy collided with Projectile");
            Destroy(other.gameObject);
            if (!isAlive)
            {
                Destroy(gameObject, 0.4f);
                soundFX_Manager.instance.PlayRocketCollision_Clip();
                VisualFX_Manager.instance.PlayExplosionVFX(transform.position);
            }
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            playerScript.isAlive = false;
            playerScript.DestroyPlayer();

            Destroy(gameObject);
            isAlive = false;
        }
    }

    void DecreaseHealth(float damage)
    {
        health -= damage;
        if (health <= 0 && isAlive)
        {
            isAlive = false;
            playerScript.IncreaseScore(enemy_scoreValue);
        }
    }
}
