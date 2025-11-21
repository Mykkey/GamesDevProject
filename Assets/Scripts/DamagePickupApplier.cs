using UnityEngine;

public class DamagePickupApplier : MonoBehaviour
{
    public GameObject bullet;
    private Bullet bulletScript;
    double timeUntilReset = 0;
    double duration = 5;
    public GameObject player;
    private Gun gunScript;
    public Canvas ingameui;
    public DisplayActivePickups displayActivePickups;

    public AudioClip pickupSound;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (Time.time <= timeUntilReset || collision.gameObject.tag != "Player") return;

        gunScript.damageMultiplierFromPickup += 1;
        gunScript.damagePickupTimeUntilReset = Time.time + duration;
        gunScript.damagePickupActive = true;

        AudioManager.instance.PlaySound(pickupSound, this.transform, 0.5f);

        displayActivePickups.DisplayDamagePickupIcon();

        Destroy(this.gameObject);

    }

    private void Start()
    {
        player = GameObject.Find("Player");
        gunScript = player.GetComponent<Gun>();
        bulletScript = bullet.GetComponent<Bullet>();
        ingameui = GameObject.Find("InGameUI").GetComponent<Canvas>();
        displayActivePickups = ingameui.GetComponent<DisplayActivePickups>();
    }
}
