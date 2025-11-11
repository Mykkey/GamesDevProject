using UnityEngine;

public class PlayerScoreAndStats : MonoBehaviour
{
    public int score;
    float scoreMultiplier = 1;


    private void Start()
    {
        score = 0;
    }

    public void AddScore(int points)
    {
        score += (int)((points / 10) * scoreMultiplier);

    }
}
