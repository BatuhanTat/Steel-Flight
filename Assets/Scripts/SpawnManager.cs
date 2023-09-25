using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject[] powerupPrefabs;
    private int enemyIndex;
    private float spawnRangeVertical = 9.5f;
    private float spawnPosX = 24;

    private float xRangePowerup = 15.0f;
    private float yRangePowerup = 10.0f;

    //private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        //gameManager = GameObject.FindGameObjectWithTag("GameManager").gameObject.GetComponent<GameManager>();
        if (GameManager.instance.isGameActive)
        { 
            InvokeRepeating("SpawnRandomEnemy", 0.5f, 2.5f);
            InvokeRepeating("SpawnRandomPowerup", 2f, 8.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnRandomEnemy()
    {
        if (GameManager.instance.isGameActive)
        {
            int enemyIndex = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[enemyIndex],
                        GenerateSpawnPosition(),
                        enemyPrefabs[enemyIndex].transform.rotation);
        }
    }

    void SpawnRandomPowerup()
    {
        if (GameManager.instance.isGameActive)
        {
            int powerupIndex = Random.Range(0, powerupPrefabs.Length);
            Instantiate(powerupPrefabs[powerupIndex],
                        GenerateSpawnPositionPowerup(),
                        powerupPrefabs[powerupIndex].transform.rotation);
        }
    }

    private Vector3 GenerateSpawnPositionPowerup()
    {
        float spawnPosX = Random.Range(-xRangePowerup, xRangePowerup);
        float spawnPosY = Random.Range(-yRangePowerup, yRangePowerup);

        Vector3 randomPos = new Vector3(spawnPosX, spawnPosY, -5);
        return randomPos;
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosY = Random.Range(-spawnRangeVertical, spawnRangeVertical);

        Vector3 randomPos = new Vector3(spawnPosX, spawnPosY, -5);
        return randomPos;
    }
}
