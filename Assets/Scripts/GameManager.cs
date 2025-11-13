using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private bool isPaused = true;
    public Canvas startMenu;
    public Canvas ingameUI;
    public Canvas upgradeMenu;

    [Header("Upgrade System")]
    public Button moveSpeedUpgrade;
    public Button fireRateUpgrade;
    public Button damageUpgrade;
    public Button healthUpgrade;
    public Button scoreUpgrade;
    public Button bigScoreUpgrade;
    private Button[] upgradeButtons;
    private Button[] chosenButtons;

    [Header("Player Upgrade References")]
    public PlayerController playerController;   // For move speed and fire rate
    public Gun gun;                     // For damage
    public Damageable damageable;           // For health
    public PlayerScoreAndStats playerScoreAndStats; // For score

    private void Start()
    {
        OpenMenu();
        ingameUI.enabled = false;
        upgradeMenu.enabled = false;

        upgradeButtons = new Button[] {
            moveSpeedUpgrade,
            fireRateUpgrade,
            damageUpgrade,
            healthUpgrade,
            scoreUpgrade,
            bigScoreUpgrade
        };

        chosenButtons = new Button[3];
    }

    public void OpenMenu()
    {
        isPaused = true;
        Time.timeScale = 0f;
    }
    public void CloseMenu()
    {
        isPaused = false;
        Time.timeScale = 1f;
    }

    public void CloseStartMenu()
    {
        isPaused = false;
        Time.timeScale = 1f;
        startMenu.enabled = false;
        ingameUI.enabled = true;
    }

    public void GenerateUpgrades()
    {
        foreach (Button btn in chosenButtons)
        {
            if (btn != null) Destroy(btn.gameObject);
        }

        RectTransform upgradeMenuTransform = upgradeMenu.GetComponent<RectTransform>();
        float menuWidth = upgradeMenuTransform.rect.width;
        float spacing = menuWidth / 4;
        float startX = -menuWidth / 2 + spacing;

        int[] decisions = new int[3];
        int decision;
        for (int i = 0; i < 3; i++)
        {
            do
            {
                decision = Random.Range(0, i == 1 ? 6 : 5); // If this is the second button, allow all 6 options (making bigscore rarer)
            } while (decisions.Take(i).Contains(decision));
            decisions[i] = decision;
            if (i < 2) decisions[i] = decision;
            Button newButton = Instantiate(upgradeButtons[decision], upgradeMenu.transform);
            string buttonName = newButton.name.Replace("(Clone)", "").Trim();
            newButton.onClick.AddListener(() => ApplyUpgrade(buttonName));

            float xPos = startX + (i * spacing);
            newButton.transform.localPosition = new Vector3(xPos, 0, 0);

            newButton.gameObject.SetActive(true);

            chosenButtons[i] = newButton;
        }

    }

    public void ShowUpgradeMenu()
    {
        upgradeMenu.enabled = true;
        Time.timeScale = 0f;
        GenerateUpgrades();
    }

    public void CloseUpgradeMenu()
    {
        foreach (Button btn in upgradeButtons)
        {
            btn.gameObject.SetActive(false);
        }
        upgradeMenu.enabled = false;
        Time.timeScale = 1f;
    }

    public void ApplyUpgrade(string upgrade)
    {

        switch (upgrade)
        {
            case "MoveSpeedUpgrade":
                playerController.moveMultiplier += 0.2f;
                break;
            case "FireRateUpgrade":
                playerController.fireRateMuliplierFromPickup += 0.2f;
                break;
            case "DamageUpgrade":
                gun.AddUpgradeDamage();
                break;
            case "HealthUpgrade":
                if (damageable.currentHealth + 20 <= damageable.maxHealth) damageable.currentHealth += 20;
                else damageable.currentHealth = damageable.maxHealth;
                damageable.UpdateUI();
                break;
            case "ScoreUpgrade":
                playerScoreAndStats.scoreMultiplierFromPickup += 0.2f;
                break;
            case "BigScoreUpgrade":
                playerScoreAndStats.scoreMultiplierFromPickup += 0.5f;
                break;
            default:
                Debug.Log("Unknown upgrade: " + upgrade);
                break;

        }

        CloseUpgradeMenu();
    }
}
