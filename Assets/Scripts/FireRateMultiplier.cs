using UnityEngine;

public class FireRateMultiplier : MonoBehaviour
{
    public GameObject player;
    private PlayerController playerController;
    private bool isActive = false;
    double timeUntilReset = 0;
    double duration = 5;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (Time.time <= timeUntilReset || collision.gameObject.tag != "Player" || isActive) return;
        playerController.fireRateMultiplier -= 0.5f;
        playerController.fireRateReset = Time.time + duration;
        playerController.fireRatePickupActive = true;

        Destroy(this.gameObject);
    }

    private void Start()
    {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        isActive = playerController.fireRatePickupActive;
    }

    private void Update()
    {
        isActive = playerController.fireRatePickupActive;
    }
}
