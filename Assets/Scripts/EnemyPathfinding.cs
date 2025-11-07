using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{

    [Header("Stats")]
    public float moveSpeed = 3f;
    public float moveMultiplier = 1f;

    public GameObject player;
    private Vector2 target;
    private Vector2 pos;

    private Rigidbody2D rb;

    void Start()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        PathfindToPlayer();
    }

    private void PathfindToPlayer()
    {
        target = player.transform.position;
        pos = transform.position;
        Vector2 direction = (target - pos).normalized;
        rb.linearVelocity = direction * moveSpeed * moveMultiplier;
    }
}
