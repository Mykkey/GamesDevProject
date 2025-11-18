using UnityEngine;

public class DisplayActivePickups : MonoBehaviour
{
    public GameObject damagePickupIcon;
    public GameObject fireRatePickupIcon;
    public GameObject scorePickupIcon;

    public bool isDamagePickupActive = false;
    public bool isFireRatePickupActive = false;
    public bool isScorePickupActive = false;

    private Vector3 firstPosition = new Vector3(-880, 320, 0);
    private int iconYSpacing = -120;
    Vector3 offscreenPosition = new Vector3(-2000, -2000, 0);

    private void Start()
    {
        damagePickupIcon.SetActive(false);
        fireRatePickupIcon.SetActive(false);
        scorePickupIcon.SetActive(false);
        MovePickupsAway();
    }
    private void Update()
    {
        CheckIconLocations();
    }

    public void DisplayDamagePickupIcon()
    {
        if (isDamagePickupActive) return;
        damagePickupIcon.SetActive(true);
        damagePickupIcon.transform.localPosition = GetIconLocation();
        isDamagePickupActive = true;
    }
    public void DisplayFireRatePickupIcon()
    {
        if (isFireRatePickupActive) return;
        fireRatePickupIcon.SetActive(true);
        fireRatePickupIcon.transform.localPosition = GetIconLocation();
        isFireRatePickupActive = true;
    }
    public void DisplayScorePickupIcon()
    {
        if (isScorePickupActive) return;
        scorePickupIcon.SetActive(true);
        scorePickupIcon.transform.localPosition = GetIconLocation();
        isScorePickupActive = true;
    }

    public void HideDamagePickupIcon()
    {
        isDamagePickupActive = false;
        damagePickupIcon.SetActive(false);
        damagePickupIcon.transform.localPosition = offscreenPosition;
        CheckIconLocations();
    }
    public void HideFireRatePickupIcon()
    {
        isFireRatePickupActive = false;
        fireRatePickupIcon.SetActive(false);
        fireRatePickupIcon.transform.localPosition = offscreenPosition;
        CheckIconLocations();
    }
    public void HideScorePickupIcon()
    {
        isScorePickupActive = false;
        scorePickupIcon.SetActive(false);
        scorePickupIcon.transform.localPosition = offscreenPosition;
        CheckIconLocations();
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

    private Vector3 GetIconLocation()
    {
        return firstPosition + new Vector3(0, GetAmountOfActivePickups() * iconYSpacing, 0);
    }

    private void MovePickupsAway()
    {
        damagePickupIcon.transform.localPosition = offscreenPosition;
        fireRatePickupIcon.transform.localPosition = offscreenPosition;
        scorePickupIcon.transform.localPosition = offscreenPosition;
    }

    private void CheckIconLocations()
    {
        if (GetAmountOfActivePickups() > 0)
        {
            RepositionIcons();
        }
    }

    private void RepositionIcons()
    {
        int index = 0;
        if (isDamagePickupActive)
        {
            damagePickupIcon.transform.localPosition = firstPosition + new Vector3(0, index * iconYSpacing, 0);
            index++;
        }
        if (isFireRatePickupActive)
        {
            fireRatePickupIcon.transform.localPosition = firstPosition + new Vector3(0, index * iconYSpacing, 0);
            index++;
        }
        if (isScorePickupActive)
        {
            scorePickupIcon.transform.localPosition = firstPosition + new Vector3(0, index * iconYSpacing, 0);
            index++;
        }
    }
}
