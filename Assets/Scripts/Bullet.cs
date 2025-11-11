using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Gun gun;
    public Rigidbody2D rb;
    public float bulletSpeed = 1000f;
    public float bulletDamage;
    public float damageMultiplier = 1;
    public float damageMultiplierFromPickup = 0;

    public void Initialize(Gun gunRef)
    {
        gun = gunRef;
        bulletDamage = gun.bulletDamage;
    }
    
    void Start()
    {
        rb.AddForce(transform.up * bulletSpeed);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Damageable damageable = collision.gameObject.GetComponent<Damageable>();
        if (damageable != null) damageable.TakeDamage(bulletDamage * (damageMultiplier + damageMultiplierFromPickup));
        Destroy(this.gameObject);
    }
}
