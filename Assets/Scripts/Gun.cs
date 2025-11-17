using System;
using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public enum GunType
    {
        AssaultRifle,
        BurstRifle,
        Shotgun,
        Sniper
    }

    public GunType gun;
    public GameObject bullet;

    public float fireRate;
    public float bulletDamage;
    public float bulletCount;
    public float bulletRange;
    public int penetration;
    public int bulletVelocity;

    bool isFiring;

    public Transform shootPosition;

    public bool damagePickupActive = false;
    public double damagePickupTimeUntilReset = 0;

    public PlayerScoreAndStats playerScoreAndStats;

    public float damageMultiplier = 1;
    public float damageMultiplierFromPickup = 0;


    void SetGunType(GunType g)
    {
        switch (g)
        {
            case GunType.AssaultRifle:
                SetAssaultRifle(); break;
            case GunType.BurstRifle:
                SetBurstRifle(); break;
            case GunType.Shotgun:
                SetShotgun(); break;
            case GunType.Sniper:
                SetSniper(); break;
            default:
                SetAssaultRifle(); break;
        }
    }


    public void SetAssaultRifle() {
        fireRate = 0.3f;
        penetration = 1;
        bulletCount = 1;
        bulletRange = 15;
        bulletDamage = 10;
        bulletVelocity = 1000;
    }
    public void SetBurstRifle()
    {
        fireRate = 0.8f;
        penetration = 1;
        bulletCount = 3;
        bulletRange = 15;
        bulletDamage = 10;
        bulletVelocity = 1000;
    }
    public void SetShotgun()
    {
        fireRate = 2;
        penetration = 1;
        bulletCount = 5;
        bulletRange = 7.5f;
        bulletDamage = 20;
        bulletVelocity = 1000;
    }
    public void SetSniper()
    {
        fireRate = 3;
        penetration = 100;
        bulletCount = 1;
        bulletRange = 100;
        bulletDamage = 100;
        bulletVelocity = 2000;
    }

    public void Fire()
    {
        if (!isFiring)
        {
            switch (gun)
            {
                case GunType.AssaultRifle:
                    StartCoroutine(SingleFireRoutine()); break;
                case GunType.BurstRifle:
                    StartCoroutine(BurstFireRoutine()); break;
                case GunType.Shotgun:
                    ShotgunFire();  break;
                case GunType.Sniper:
                    StartCoroutine(SingleFireRoutine()); break;
            }
        }
    }
    private IEnumerator SingleFireRoutine()
    {
        isFiring = true;

        GameObject bul = Instantiate(bullet, shootPosition.position, shootPosition.rotation);
        Bullet bulScript = bul.GetComponent<Bullet>();
        bulScript.Initialize(this, damageMultiplier + damageMultiplierFromPickup);

        isFiring = false;
        yield return null;
        playerScoreAndStats.AddShotFired();
    }

    private IEnumerator BurstFireRoutine()
    {
        isFiring = true;

        for (int i = 0; i < bulletCount; i++)
        {
            GameObject bul = Instantiate(bullet, shootPosition.position, shootPosition.rotation);
            Bullet bulScript = bul.GetComponent<Bullet>();
            bulScript.Initialize(this, damageMultiplier + damageMultiplierFromPickup);

            playerScoreAndStats.AddShotFired();

            if (i < bulletCount - 1)
            {
                yield return new WaitForSeconds(0.1f);
            }
        }

        isFiring = false;
    }

    private void ShotgunFire()
    {
        GameObject[] shotgunBullets = new GameObject[(int)bulletCount];

        isFiring = true;

        float spreadAngle = 30f;
        float angleStep = spreadAngle / (bulletCount - 1);
        float startingAngle = -spreadAngle / 2;

        for (int i = 0; i < bulletCount; i++)
        {
            float currentAngle = startingAngle + (angleStep * i);
            Quaternion rotation = shootPosition.rotation * Quaternion.Euler(0, 0, currentAngle);
            GameObject bul = Instantiate(bullet, shootPosition.position, rotation);
            Bullet bulScript = bul.GetComponent<Bullet>();
            bulScript.Initialize(this, damageMultiplier + damageMultiplierFromPickup);

            shotgunBullets[i] = bul;

            // Make bullets ignore collisions with each other because they are initialised at the same position
            for (int j = 0; j < i; j++)
            {
                if (shotgunBullets[j] != null)
                {
                    Collider2D bulColldier = bul.GetComponent<Collider2D>();
                    Collider2D otherBulletCollider = shotgunBullets[j].GetComponent<Collider2D>();

                    if (bulColldier != null && otherBulletCollider != null)
                    {
                        Physics2D.IgnoreCollision(bulColldier, otherBulletCollider);
                    }
                }
            }
            playerScoreAndStats.AddShotFired();
        }


        isFiring = false;
    }


    void Start()
    {
        gun = GunType.AssaultRifle;
        SetGunType(gun);

        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            playerScoreAndStats = player.GetComponent<PlayerScoreAndStats>();
        }
    }

    private void Update()
    {
        CheckDamagePickup();
    }

    private void CheckDamagePickup()
    {
        if (damagePickupActive == true && Time.time >= damagePickupTimeUntilReset)
        {
            damageMultiplier -= 1;
            damagePickupActive = false;
        }
    }
    public void AddUpgradeDamage()
    {
        damageMultiplierFromPickup += 0.2f;
    }
}
