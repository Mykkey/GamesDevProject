using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float bulletSpeed = 1000f;
    public float bulletDamage = 10f;
    public Transform target;
    private Transform pos;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        target = GameObject.FindGameObjectWithTag("Player").transform;
        pos = this.transform;

        Vector3 dir = new Vector3(
            target.position.x - pos.position.x,
            target.position.y - pos.position.y,
            0
        );

        rb.AddForce(dir * bulletSpeed);
    }
}
