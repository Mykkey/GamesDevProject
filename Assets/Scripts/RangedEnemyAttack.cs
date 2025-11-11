using System;
using UnityEngine;

public class RangedEnemyAttack : MonoBehaviour
{
    public GameObject bullet;

    private double lastFireTime = 0;
    private double fireRate = 1.5f;
    public Damageable damageable;

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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            damageable.TakeDamage(5);
        }
    }
}
