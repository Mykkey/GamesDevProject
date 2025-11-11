using UnityEngine;

public class ScoreMultiplier : MonoBehaviour
{
    private PlayerScoreAndStats playerScoreAndStats;
    double timeUntilReset = 0;
    double duration = 5;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (Time.time <= timeUntilReset || collision.gameObject.tag != "Player") return;

        playerScoreAndStats.scoreMultiplier += 1;
        playerScoreAndStats.scoreMultiplierTimeUntilReset = Time.time + duration;
        playerScoreAndStats.isActive = true;

        Destroy(this.gameObject);

    }

    private void Start()
    {
        GameObject player = GameObject.Find("Player");
        playerScoreAndStats = player.GetComponent<PlayerScoreAndStats>();
    }
}
