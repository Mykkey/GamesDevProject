using UnityEngine;

public class DisplayActivePickups : MonoBehaviour
{
    public GameObject damagePickupIcon;
    public GameObject fireRatePickupIcon;
    public GameObject scorePickupIcon;

    public bool isDamagePickupActive = false;
    public bool isFireRatePickupActive = false;
    public bool isScorePickupActive = false;

    private Vector2 basePosition = new Vector2(-7.5f, 4.0f);

    public void DisplayDamagePickupIcon()
    {
        isDamagePickupActive = true;
    }
    public void DisplayFireRatePickupIcon()
    {
        isFireRatePickupActive = true;
    }
    public void DisplayScorePickupIcon()
    {
        isScorePickupActive = true;
    }

    public void HideDamagePickupIcon()
    {
        isDamagePickupActive = false;
    }
    public void HideFireRatePickupIcon()
    {
        isFireRatePickupActive = false;
    }
    public void HideScorePickupIcon()
    {
        isScorePickupActive = false;
    }

    private int GetAmountOfActivePickups()
    {
        int activePickupCount = 0;
        if (isDamagePickupActive)
        {
            activePickupCount++;
        }
        if (isFireRatePickupActive)
        {
            activePickupCount++;
        }
        if (isScorePickupActive)
        {
            activePickupCount++;
        }
        return activePickupCount;
    }
}
