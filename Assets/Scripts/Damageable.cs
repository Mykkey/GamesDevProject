using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Damageable : MonoBehaviour
{

    public float maxHealth = 100;
    public float currentHealth;

    public float invulnerablePeriod = 0.5f;
    public float lastInvulnerableTime;

    public PlayerScoreAndStats playerScoreAndStats;

    public HealthBarUIScript gameUI;

    void Start()
    {
        currentHealth = maxHealth;
        lastInvulnerableTime = Time.time;

        gameUI = GameObject.Find("HealthBarHealth").GetComponent<HealthBarUIScript>();
        if (gameObject.CompareTag("Player")) UpdateUI();

        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            playerScoreAndStats = player.GetComponent<PlayerScoreAndStats>();
        }
    }

    public void TakeDamage(float damage)
    {
        if (gameObject.CompareTag("Player")) playerScoreAndStats.AddDamageTaken(damage);
        if (Time.time >= lastInvulnerableTime + invulnerablePeriod) {
            currentHealth -= damage;
            lastInvulnerableTime = Time.time;
            if (currentHealth <= 0)
            {
                Die();
            }
            if (gameObject.CompareTag("Player")) UpdateUI();
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
            playerScoreAndStats.AddScore((int)maxHealth);
            Destroy(this.gameObject);
        }
    }

    public void UpdateUI()
    {
        gameUI.SetHealth(currentHealth);
    }
}
