using System;
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

    public float distanceToStop = 1f;

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

        if (checkDistance(target, pos))
        {
            Vector2 direction = (target - pos).normalized;
            rb.linearVelocity = direction * moveSpeed * moveMultiplier;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    private bool checkDistance(Vector2 target, Vector2 pos)
    {
        return Vector2.Distance(target, pos) > distanceToStop;
    }
}
