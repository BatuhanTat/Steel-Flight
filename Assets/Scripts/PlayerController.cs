using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    public float speed;
    [SerializeField] float health = 10.0f;
    [SerializeField] float bulletDelay = 0.3f;
    [SerializeField] float rocketDelay = 1.0f;
    public GameObject[] projectilePrefabs;

    [Space]
    public bool canShoot;
    public bool isAlive = true;
    public PowerUpType currentPowerUp = PowerUpType.None;
    public float score { get; set; }

    private bool hasPowerup;
    private float fireDelay;
    private int powerupIndex;
    private Coroutine powerupCountdown;
    private InGame_UI ingame_UI;
    private Rigidbody playerRb;
    private BoxCollider boxCollider;

    // moveable boundary variables
    private float xRange = 18.0f;
    private float yRange = 10.0f;
    private float verticalInput;
    private float horizontalInput;

    private void Awake()
    {
        ingame_UI = GameObject.Find("Canvas").gameObject.GetComponent<InGame_UI>();
        playerRb = GetComponent<Rigidbody>();
        boxCollider = GetComponentInChildren<BoxCollider>();
        fireDelay = bulletDelay;
    }

    void Update()
    {
        if (isAlive)
        {
            Move();
            Fire();
            ConstrainPlayerMovement();
        }

    }

    private void ConstrainPlayerMovement()
    {
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }

        if (transform.position.y < -yRange)
        {
            transform.position = new Vector3(transform.position.x, -yRange, transform.position.z);
        }
        if (transform.position.y > yRange)
        {
            transform.position = new Vector3(transform.position.x, yRange, transform.position.z);
        }
    }

    private void Move()
    {
        // get the user's vertical input
        verticalInput = Input.GetAxis("Vertical");

        // get the user's horizontal input
        horizontalInput = Input.GetAxis("Horizontal");

        // move the plane on y axis
        transform.Translate(Vector3.up * speed * Time.deltaTime * verticalInput);

        // move the plane on x axis
        transform.Translate(Vector3.right * speed * Time.deltaTime * horizontalInput);
    }

    private void Fire()
    {
        if (Input.GetKey(KeyCode.Space) && canShoot)
        {
            Vector3 tempPos = transform.position;
            if (hasPowerup)
            {
                tempPos.x -= 2f;
                tempPos.y -= 0.75f;
            }
            // Playing appropiate shooting clip according to the projectile type just before instantiation of it.
            ShootingProjectile_SFX();
            Instantiate(projectilePrefabs[powerupIndex],
                        tempPos,
                        projectilePrefabs[powerupIndex].transform.rotation);
            canShoot = false;
            StartCoroutine(FireDelay());
        }
    }

    IEnumerator FireDelay()
    {
        yield return new WaitForSeconds(fireDelay);
        canShoot = true;
    }

    IEnumerator PowerUpCountdownRoutine()
    {
        yield return new WaitForSeconds(4);
        hasPowerup = false;
        currentPowerUp = PowerUpType.None;
        powerupIndex = 0;
        fireDelay = bulletDelay;
    }

    private void ShootingProjectile_SFX()
    {
        if (powerupIndex == 0)
        {
            soundFX_Manager.instance.PlayShootingBullet_Clip(gameObject.tag);
        }
        else if (powerupIndex == 1)
        {
            soundFX_Manager.instance.PlayShootingRocket_Clip();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup") && !hasPowerup)
        {
            //Debug.Log("Powerup taken");
            hasPowerup = true;
            currentPowerUp = other.gameObject.GetComponent<Powerup>().powerUpType;
            Destroy(other.gameObject);

            if (powerupCountdown != null)
            {
                StopCoroutine(powerupCountdown);
            }

            powerupCountdown = StartCoroutine(PowerUpCountdownRoutine());
            if (currentPowerUp == PowerUpType.Rocket)
            {
                powerupIndex = 1;
                fireDelay = rocketDelay;
            }
        }
        else if (other.gameObject.CompareTag("Bullet") &&
                other.gameObject.GetComponent<ProjectileMovement>().isEnemyProjectile)
        {
            soundFX_Manager.instance.PlayBulletCollision_Clip();
            VisualFX_Manager.instance.PlayHitEffect(transform.position);
            DecreaseHealth(other.gameObject.GetComponent<ProjectileMovement>().damage);
            //Debug.Log("Player collided with Projectile. Health: " + health);
            Destroy(other.gameObject);
            if (!isAlive)
            {
                DestroyPlayer();
            }
        }
    }

    public void IncreaseScore(float value)
    {
        //Debug.Log("Score :" + score);
        score += value;
        ingame_UI.Update_ScoreText(score);
    }

    void DecreaseHealth(float damage)
    {
        health -= damage;
        ingame_UI.Update_Health(health);
        if (health <= 0 && isAlive)
        {
            isAlive = false;
        }
    }
    public void DestroyPlayer()
    {
        Destroy(gameObject, 4f);
        soundFX_Manager.instance.PlayRocketCollision_Clip();
        VisualFX_Manager.instance.PlayExplosionVFX(transform.position);
        VisualFX_Manager.instance.PlayFireEffect(transform.position,gameObject.GetComponent<PlayerController>());
        ingame_UI.Update_Health(0);
        playerRb.useGravity = true;
        boxCollider.enabled = false;
        GameManager.instance.Set_IsGameActive(false);
        GameManager.instance.SetScore(score);
    }

    public Vector3 GetPlayer_Position()
    {
        return transform.position;
    }
}
