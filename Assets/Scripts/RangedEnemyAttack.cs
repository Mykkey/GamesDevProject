using System;
using UnityEngine;

public class RangedEnemyAttack : MonoBehaviour
{
    public GameObject bullet;

    private double lastFireTime = 0;
    private double fireRate = 1.5f;

    private void Start()
    {
    }

    private void Update()
    {
        if (Time.time >= lastFireTime + fireRate)
        {
            Fire();
            lastFireTime = Time.time;
        }
    }

    private void Fire()
    {
        if (Vector2.Distance(transform.position, GameObject.Find("Player").transform.position) < 15)
            Instantiate(bullet, transform.position, Quaternion.identity);
    }
}
