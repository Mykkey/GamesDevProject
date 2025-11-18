using System;
using TMPro;
using UnityEngine;

public class PlayerScoreAndStats : MonoBehaviour
{
    public int level = 1;
    public int xpToNextLevel = 100;

    public int score;
    public float scoreMultiplier = 1;
    public float scoreMultiplierFromPickup = 0;
    public double scoreMultiplierTimeUntilReset = 0;
    public bool isActive = false;
    public GameManager gameManager;
    public EnemySpawner enemySpawner;

    public XpBarUIScript gameUI;

    public Canvas ingameui;
    public DisplayActivePickups displayActivePickups;

    public int damageTaken = 0;
    public int shotsFired = 0;
    public int enemiesKilled = 0;

    private void Start()
    {
        score = 0;
        gameUI = GameObject.Find("XpBarXp").GetComponent<XpBarUIScript>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        ingameui = GameObject.Find("InGameUI").GetComponent<Canvas>();
        displayActivePickups = ingameui.GetComponent<DisplayActivePickups>();
    }

    public void AddScore(int points)
    {
        score += (int)((points / 10) * (scoreMultiplier + scoreMultiplierFromPickup));
        gameUI.SetXp(score);
        TMP_Text scoreText = ingameui.transform.Find("Score").GetComponent<TMP_Text>();
        scoreText.text = "Score: " + score.ToString();
    }

    private void Update()
    {
        CheckScorePickup();
        CheckLevelUp();
    }

    private void CheckLevelUp()
    {
        if (score >= xpToNextLevel)
        {
            gameManager.ShowUpgradeMenu();
            level += 1;
            CalculateXpToNextLevel();
            gameUI.SetMaxXp(xpToNextLevel);
            gameUI.SetXp(score);
            enemySpawner.GetPlayerLevel();
        }
    }

    private void CalculateXpToNextLevel()
    {
        xpToNextLevel = xpToNextLevel + 100 + (10 * level);
    }

    private void CheckScorePickup()
    {
        if (isActive && Time.time >= scoreMultiplierTimeUntilReset)
        {
            scoreMultiplier -= 1;
            isActive = false;

            displayActivePickups.HideScorePickupIcon();
        }
    }





    // ------------- Stats tracker ------------- //

    public void AddDamageTaken(float damage)
    {
        damageTaken += (int) damage;
    }
    public void AddEnemyKilled()
    {
        enemiesKilled++;
    }
    public void AddShotFired()
    {
        shotsFired++;
    }
}
