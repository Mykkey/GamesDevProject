using System;
using UnityEngine;

public class PlayerScoreAndStats : MonoBehaviour
{
    public int level = 1;
    public int xpToNextLevel = 100;

    public int score;
    public float scoreMultiplier = 1;
    public double scoreMultiplierTimeUntilReset = 0;
    public bool isActive = false;


    private void Start()
    {
        score = 0;
    }

    public void AddScore(int points)
    {
        score += (int)((points / 10) * scoreMultiplier);

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
            ChooseUpgrade();
            level += 1;
            xpToNextLevel = (int) (xpToNextLevel + ((xpToNextLevel * 0.5) * level));
        }
    }

    private void ChooseUpgrade()
    {
        // TODO: Implement upgrade selection logic
    }

    private void CheckScorePickup()
    {
        if (isActive && Time.time >= scoreMultiplierTimeUntilReset)
        {
            scoreMultiplier -= 1;
            isActive = false;
        }
    }
}
