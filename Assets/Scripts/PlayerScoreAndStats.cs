using System;
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

    public XpBarUIScript gameUI;

    private int deaths = 0;
    private int shotsFired = 0;
    private int enemiesKilled = 0;

    private void Start()
    {
        score = 0;
        gameUI = GameObject.Find("XpBarXp").GetComponent<XpBarUIScript>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void AddScore(int points)
    {
        score += (int)((points / 10) * (scoreMultiplier + scoreMultiplierFromPickup));
        gameUI.SetXp(score);
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
            xpToNextLevel = (int)(xpToNextLevel + (xpToNextLevel + (level * 10)));
            gameUI.SetMaxXp(xpToNextLevel);
            gameUI.SetXp(score);
        }
    }

    private void CheckScorePickup()
    {
        if (isActive && Time.time >= scoreMultiplierTimeUntilReset)
        {
            scoreMultiplier -= 1;
            isActive = false;
        }
    }





    // ------------- Stats tracker ------------- //

    public void AddDeath()
    {
        deaths++;
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
