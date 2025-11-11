using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool isPaused = true;
    public Canvas startMenu;
    public Canvas ingameUI;

    private void Start()
    {
        OpenMenu();
        ingameUI.enabled = false;
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
}
