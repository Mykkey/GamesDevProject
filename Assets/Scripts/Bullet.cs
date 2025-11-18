using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Gun gun;
    public Rigidbody2D rb;
    public float bulletSpeed;
    public float bulletDamage;
    private float damageMultiplier;
    public int bulletPenetration;
    private int penetrations = 0;

    public void Initialize(Gun gunRef, float dmgMult)
    {
        gun = gunRef;
        bulletSpeed = gun.bulletVelocity;
        bulletDamage = gun.bulletDamage;
        bulletPenetration = gun.penetration;
        damageMultiplier = dmgMult;
    }
    
    void Start()
    {
        rb.AddForce(transform.up * bulletSpeed);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.gameObject.GetComponent<Damageable>();
        if (damageable != null) damageable.TakeDamage(bulletDamage * damageMultiplier);

        penetrations++;
        if (penetrations >= bulletPenetration) Destroy(this.gameObject);
    }
}
