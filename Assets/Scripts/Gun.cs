using System;
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

    public Transform shootPosition;

    public bool damagePickupActive = false;
    public double damagePickupTimeUntilReset = 0;


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


    void SetAssaultRifle() {
        fireRate = 0.3f;
        penetration = 1;
        bulletCount = 1;
        bulletRange = 15;
        bulletDamage = 10;
    }
    void SetBurstRifle()
    {
        fireRate = 0.8f;
        penetration = 1;
        bulletCount = 3;
        bulletRange = 15;
        bulletDamage = 10;
    }
    void SetShotgun()
    {
        fireRate = 1;
        penetration = 1;
        bulletCount = 5;
        bulletRange = 7.5f;
        bulletDamage = 50;
    }
    void SetSniper()
    {
        fireRate = 2;
        penetration = 100;
        bulletCount = 1;
        bulletRange = 100;
        bulletDamage = 50;
    }

    public void Fire()
    {
        GameObject bul = Instantiate(bullet, shootPosition.position, shootPosition.rotation);
        Bullet bulScript = bul.GetComponent<Bullet>();
        bulScript.Initialize(this);
    }



    void Start()
    {
        gun = GunType.AssaultRifle;
        SetGunType(gun);
    }

    private void Update()
    {
        CheckDamagePickup();
    }

    private void CheckDamagePickup()
    {
        if (damagePickupActive == true && Time.time >= damagePickupTimeUntilReset)
        {
            bullet.GetComponent<Bullet>().damageMultiplier -= 1;
            damagePickupActive = false;
        }
    }
}
