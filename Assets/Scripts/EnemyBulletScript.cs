using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyBulletScript : MonoBehaviour
{
    public float bulletSpeed = 1000f;
    public float bulletDamage = 5;
    private Transform pos;
    private Transform target;
    public Damageable damageable;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        rb.rotation = 90;

        target = GameObject.FindGameObjectWithTag("Player").transform;
        pos = this.transform;

        Vector3 dir = new Vector3(
            target.position.x - pos.position.x,
            target.position.y - pos.position.y,
            0
        ).normalized;

        rb.AddForce(dir * bulletSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            damageable = collision.gameObject.GetComponent<Damageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(bulletDamage);
            }
            Destroy(this.gameObject);
        }
    }
}
