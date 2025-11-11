using UnityEngine;

public class HealthBarUIScript : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public int width = 295;
    public int height = 35;

    public RectTransform healthBar;

    private void Start()
    {
        SetMaxHealth(100);
        SetHealth(maxHealth);
    }

    public void SetMaxHealth(float max)
    {
        maxHealth = max;
    }

    public void SetHealth(float healthNew)
    {
        health = healthNew;
        float healthPercent = health / maxHealth;
        float adjustedWidth = healthPercent * width;

        healthBar.sizeDelta = new Vector2(adjustedWidth, height);
    }
}
