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

    public UpdateEnemyHealthBar enemyHealthBar;

    public AudioClip playerHurt;

    void Start()
    {
        currentHealth = maxHealth;
        lastInvulnerableTime = Time.time;

        gameUI = GameObject.Find("HealthBarHealth").GetComponent<HealthBarUIScript>();
        if (gameObject.CompareTag("Player")) UpdateUI();
        else if (gameObject.CompareTag("Enemy")) enemyHealthBar = GetComponentInChildren<UpdateEnemyHealthBar>();

        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            playerScoreAndStats = player.GetComponent<PlayerScoreAndStats>();
        }
    }

    public void TakeDamage(float damage)
    {
        if (Time.time >= lastInvulnerableTime + invulnerablePeriod)
        {
            currentHealth -= damage;
            lastInvulnerableTime = Time.time;
            if (gameObject.CompareTag("Player"))
            {
                UpdateUI();
                playerScoreAndStats.AddDamageTaken(damage);
                AudioManager.instance.PlaySound(playerHurt, this.transform, 0.5f);
            }
            else if (gameObject.CompareTag("Enemy"))
            {
                enemyHealthBar.setHealth(enemyHealthBar.getHealth() - damage);
                enemyHealthBar.UpdateHealthBar();
            }
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
        }
        else
        {
            playerScoreAndStats.AddEnemyKilled();
            playerScoreAndStats.AddScore((int)maxHealth);
            Destroy(this.gameObject);
        }
    }

    public void UpdateUI()
    {
        gameUI.SetHealth(currentHealth);
    }
}
