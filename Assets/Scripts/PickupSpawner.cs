using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PickupSpawner : MonoBehaviour
{
    public GameObject fireRatePickup;
    public GameObject bulletDamagePickup;
    public GameObject scoreMultiplierPickup;

    private GameObject[] pickups;
    private GameObject selectedPickup;

    public float minSpawnTime;
    public float maxSpawnTime;
    private float timeUntilSpawn;

    private void Awake()
    {
        SetTimeUntilSpawn();
        pickups = new GameObject[] { fireRatePickup, bulletDamagePickup, scoreMultiplierPickup };
    }

    private void Update()
    {
        timeUntilSpawn -= Time.deltaTime;

        if (timeUntilSpawn <= 0)
        {
            GetSpawnPos();
            selectedPickup = GetSelectedPickup();
            Instantiate(selectedPickup, transform.position, Quaternion.identity);
            SetTimeUntilSpawn();
        }
    }

    private GameObject GetSelectedPickup()
    {
        return pickups[Random.Range(0, 3)];
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
