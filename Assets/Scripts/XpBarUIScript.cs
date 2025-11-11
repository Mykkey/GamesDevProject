using UnityEngine;

public class XpBarUIScript : MonoBehaviour
{
    public float xp;
    public float maxXp;
    public int width = 295;
    public int height = 35;

    public RectTransform xpBar;

    private void Start()
    {
        SetMaxXp(100);
        SetXp(0);
    }

    public void SetMaxXp(float max)
    {
        maxXp = max;
    }

    public void SetXp(float xpNew)
    {
        xp = xpNew;
        float xpPercent = xp / maxXp;
        float adjustedWidth = xpPercent * width;

        xpBar.sizeDelta = new Vector2(adjustedWidth, height);
    }
}
