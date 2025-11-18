using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public GameObject square;
    public GameObject diamond;
    public GameObject triangle;

    private GameObject[] enemies = new GameObject[3];

    public float minSpawnTime;
    public float maxSpawnTime;
    private float timeUntilSpawn;

    private float level;

    private void Awake()
    {
        enemies[0] = square;
        enemies[1] = diamond;
        enemies[2] = triangle;
    }

    private void Start()
    {
        GetPlayerLevel();
        minSpawnTime = 2;
        maxSpawnTime = 4;
        SetTimeUntilSpawn();
    }

    private void Update()
    {
        timeUntilSpawn -= Time.deltaTime;

        if (timeUntilSpawn <= 0)
        {
            GetSpawnPos();
            Instantiate(enemies[Random.Range(0, enemies.Length)], transform.position, Quaternion.identity);
            SetTimeUntilSpawn();
        }
    }

    private void GetPlayerLevel()
    {
        level = GameObject.Find("Player").GetComponent<PlayerScoreAndStats>().level;
    }

    private void GetSpawnPos()
    {
        transform.position = new Vector3(
            Random.Range(-75, 75),
            Random.Range(-35, 35),
            0);
        CheckIfValidSpawnPos();
    }

    private void CheckIfValidSpawnPos()
    {
        Vector3 cam = Camera.main.WorldToViewportPoint(transform.position);
        float camX = cam.x;
        float camY = cam.y;
        if (camX > 0 && camX < 1 && camY > 0 && camY < 1)
        {
            GetSpawnPos();
        }
    }

    private void SetTimeUntilSpawn()
    {
        timeUntilSpawn = Random.Range(minSpawnTime / level, maxSpawnTime / level);
    }
}
