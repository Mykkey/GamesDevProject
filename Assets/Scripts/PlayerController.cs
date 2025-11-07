using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 5f;
    public float moveMultiplier = 1f;

    public Rigidbody2D rb;

    private Vector2 moveDirection;

    private Vector2 mousePos;
    private Camera cam;

    public Gun gun;
    private float lastShootTime;

    private int lives = 3;
    public Damageable damageable;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        lastShootTime = Time.time;
    }

    void Update()
    {
        HandleInputs();
        LookAtMouse();
    }

    void FixedUpdate()
    {
        Move();
    }

    void HandleInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;

        if (Input.GetMouseButton(0) && Time.time >= lastShootTime + gun.fireRate)
        {
            gun.Fire();
            lastShootTime = Time.time;
        }

    }

    void Move()
    {
        rb.linearVelocity = new Vector2(moveDirection.x * moveSpeed * moveMultiplier,
                                        moveDirection.y * moveSpeed * moveMultiplier);
    }

    void LookAtMouse()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 aimDirection = mousePos - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90;
        rb.rotation = aimAngle;
    }

    public void Respawn()
    {
        lives -= 1;
        if (lives <= 0)
        {
            GameOver();
        } else
        {
            transform.position = Vector2.zero;
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
    }
}
