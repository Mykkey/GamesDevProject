using UnityEngine;

public class DamagePickupApplier : MonoBehaviour
{
    public GameObject bullet;
    private Bullet bulletScript;
    double timeUntilReset = 0;
    double duration = 5;
    public GameObject player;
    private Gun gunScript;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (Time.time <= timeUntilReset || collision.gameObject.tag != "Player") return;

        bulletScript.damageMultiplier += 1;
        gunScript.damagePickupTimeUntilReset = Time.time + duration;
        gunScript.damagePickupActive = true;

        Destroy(this.gameObject);

    }

    private void Start()
    {
        player = GameObject.Find("Player");
        gunScript = player.GetComponent<Gun>();
        bulletScript = bullet.GetComponent<Bullet>();
    }
}
