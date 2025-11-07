using System;
using UnityEngine;

public class Damageable : MonoBehaviour
{

    public float maxHealth = 100;
    public float currentHealth;

    public float invulnerablePeriod = 0.5f;
    public float lastInvulnerableTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        lastInvulnerableTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        if (Time.time >= lastInvulnerableTime + invulnerablePeriod) {
            currentHealth -= damage;
            lastInvulnerableTime = Time.time;
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        if (gameObject.tag == "Player")
        {
            currentHealth = maxHealth;
            PlayerController pc = GetComponent<PlayerController>();
            pc.Respawn();
        } else
        {
            Destroy(this.gameObject);
        }
    }
}
