using System;
using UnityEngine;

public class DealDamage : MonoBehaviour
{

    [Header("Stats")]
    public float damage = 10;
    public float damageMultiplier = 1;

    private bool inflictingDamage = false;
    private GameObject other;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inflictingDamage)
        {
            InflictDamage(other);
        }
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        inflictingDamage = true;
        other = collision.gameObject;
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        inflictingDamage = false;
    }

    private void InflictDamage(GameObject other)
    {
        // Return if colliding with another enemy or another player (don't do damage to own team
        if (other.gameObject.CompareTag(gameObject.tag)) return;

        Damageable damageable = other.GetComponent<Damageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage * damageMultiplier);
        }
    }
}
