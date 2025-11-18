using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private bool gameIsPaused = false;
    public Canvas startMenu;
    public Canvas ingameUI;
    public Canvas upgradeMenu;
    public Canvas weaponScreen;
    public Canvas gameOverScreen;
    public Canvas tutorialScreen;
    public Canvas pauseScreen;
    public Canvas countdownScreen;

    private TMP_Text damageTakenText;
    private TMP_Text shotsFiredText;
    private TMP_Text enemiesKilledText;
    private TMP_Text scoreText;

    private TMP_Text livesText;

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


    private int seconds = 3;

    private void Start()
    {
        damageTakenText = gameOverScreen.transform.Find("DamageTakenText").GetComponent<TMP_Text>();
        shotsFiredText = gameOverScreen.transform.Find("ShotsFiredText").GetComponent<TMP_Text>();
        enemiesKilledText = gameOverScreen.transform.Find("EnemiesKilledText").GetComponent<TMP_Text>();
        scoreText = gameOverScreen.transform.Find("ScoreText").GetComponent<TMP_Text>();
        livesText = ingameUI.transform.Find("Lives").GetComponent<TMP_Text>();

        DeactivateAllCanvases();
        OpenMenu();

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

    private void Update()
    {
        CheckForPause();
    }

    private void CheckForPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gameIsPaused)
        {
            PauseGame();
        } else if (Input.GetKeyDown(KeyCode.Escape) && gameIsPaused)
        {
            UnpauseGame();
        }
    }

    private void PauseGame()
    {
        DeactivateAllCanvases();
        ingameUI.enabled = true;
        pauseScreen.enabled = true;
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    private void UnpauseGame()
    {
        DeactivateAllCanvases();
        ingameUI.enabled = true;
        countdownScreen.enabled = true;
        Time.timeScale = 0f;
        StartCoroutine(UnpauseCountdown());
    }

    private IEnumerator UnpauseCountdown()
    {
        TMP_Text countdownText = countdownScreen.GetComponentInChildren<TMP_Text>();
        while (seconds > 0)
        {
            if (countdownText != null) countdownText.text = seconds.ToString();
            yield return new WaitForSecondsRealtime(1);
            seconds--;
        }
        ResumeGameAfterCountdown();
    }

    private void ResumeGameAfterCountdown()
    {
        DeactivateAllCanvases();
        ingameUI.enabled = true;
        Time.timeScale = 1.0f;
        seconds = 3;
        gameIsPaused = false;
    }

    public void OpenMenu()
    {
        startMenu.enabled = true;
        ingameUI.enabled = false;
        upgradeMenu.enabled = false;
        weaponScreen.enabled = false;
        gameOverScreen.enabled = false;
        tutorialScreen.enabled = false;
        Time.timeScale = 0f;
    }
    public void CloseMenu()
    {
        Time.timeScale = 1f;
    }

    public void CloseStartMenu()
    {
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

    public void ShowChooseWeaponScreen()
    {
        Time.timeScale = 0f;
        ingameUI.enabled = false;
        upgradeMenu.enabled = false;
        startMenu.enabled = false;
        weaponScreen.enabled = true;
    }

    public void CloseChooseWeaponScreen()
    {
        weaponScreen.enabled = false;
    }

    public void AssaultRifle()
    {
        gun.gun = Gun.GunType.AssaultRifle;
        gun.SetAssaultRifle();
    }    
    public void BurstRifle()
    {
        gun.gun = Gun.GunType.BurstRifle;
        gun.SetBurstRifle();
    }
    public void Sniper()
    {
        gun.gun = Gun.GunType.Sniper;
        gun.SetSniper();
    }
    public void Shotgun()
    {
        gun.gun = Gun.GunType.Shotgun;
        gun.SetShotgun();
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        DeactivateAllCanvases();
        gameOverScreen.enabled = true;
        UpdateGameOverStats(playerScoreAndStats.damageTaken < 300 ? 300 : playerScoreAndStats.damageTaken, playerScoreAndStats.shotsFired, playerScoreAndStats.enemiesKilled, playerScoreAndStats.score);
    }

    public void UpdateGameOverStats(int damageTaken, int shotsFired, int enemiesKilled, int score)
    {
        scoreText.text = "Score: " + score;
        damageTakenText.text = "Damage Taken: " + damageTaken;
        shotsFiredText.text = "Shots Fired: " + shotsFired;
        enemiesKilledText.text = "Enemies Killed: " + enemiesKilled;
    }

    public void ShowTutorialScreen()
    {
        Time.timeScale = 0f;
        DeactivateAllCanvases();
        tutorialScreen.enabled = true;
    }

    public void Restart()
    {
        OpenMenu();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void DeactivateAllCanvases()
    {
        startMenu.enabled = false;
        ingameUI.enabled = false;
        upgradeMenu.enabled = false;
        weaponScreen.enabled = false;
        gameOverScreen.enabled = false;
        tutorialScreen.enabled = false;
        pauseScreen.enabled = false;
        countdownScreen.enabled = false;
    }

    public void UpdateLivesCount(int lives)
    {
        livesText.text = "Lives: " + lives.ToString();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
