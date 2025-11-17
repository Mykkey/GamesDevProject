using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Gun gun;
    public Rigidbody2D rb;
    public float bulletSpeed;
    public float bulletDamage;
    private float damageMultiplier;

    public void Initialize(Gun gunRef, float dmgMult)
    {
        gun = gunRef;
        bulletSpeed = gun.bulletVelocity;
        bulletDamage = gun.bulletDamage;
        damageMultiplier = dmgMult;
    }
    
    void Start()
    {
        rb.AddForce(transform.up * bulletSpeed);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Damageable damageable = collision.gameObject.GetComponent<Damageable>();
        if (damageable != null) damageable.TakeDamage(bulletDamage * damageMultiplier);
        Destroy(this.gameObject);
    }
}
