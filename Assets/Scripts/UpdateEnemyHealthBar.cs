using UnityEngine;

public class UpdateEnemyHealthBar : MonoBehaviour
{
    public RectTransform healthBar;
    private float maxHealth;
    private float health;
    private float width = 0.7f;
    private float height = 0.1f;

    private void Start()
    {
        maxHealth = GetComponentInParent<Damageable>().maxHealth;
        health = maxHealth;
    }

    public void UpdateHealthBar()
    {
        float adjustedWidth = (health / maxHealth) * width;
        healthBar.sizeDelta = new Vector2(adjustedWidth, height);
    }

    public float getMaxHealth()
    {
        return maxHealth;
    }
    public float getHealth()
    {
        return health;
    }
    public void setHealth(float newHealth)
    {
        health = newHealth;
    }
}
