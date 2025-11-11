using System;
using UnityEngine;

public class RangedEnemyAttack : MonoBehaviour
{
    public EnemyBullet bullet;

    private double lastFireTime = 0;
    private double fireRate = 1.5f;

    private void Start()
    {
        bullet = GetComponent<EnemyBullet>();
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
        Instantiate(bullet, transform.position, transform.rotation);
    }
}
