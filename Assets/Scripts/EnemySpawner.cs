using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;

    public float minSpawnTime;
    public float maxSpawnTime;
    private float timeUntilSpawn;

    private void Awake()
    {
        SetTimeUntilSpawn();
    }

    private void Update()
    {
        timeUntilSpawn -= Time.deltaTime;

        if (timeUntilSpawn <= 0)
        {
            GetSpawnPos();
            Instantiate(enemy, transform.position, Quaternion.identity);
            SetTimeUntilSpawn();
        }
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
        timeUntilSpawn = Random.Range(minSpawnTime, maxSpawnTime);
    }
}
