using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public GameObject hud;
    public GameObject pauseMenu;
    public GameObject[] otherMenus;

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (pauseMenu.activeSelf)
            {
                TogglePauseMenu();
            }
            else if (hud.activeSelf && !AreOtherMenusActive())
            {
                TogglePauseMenu();
            }
        }
    }

    public void TogglePauseMenu()
    {
        isPaused = !isPaused;

        pauseMenu.SetActive(isPaused);
        hud.SetActive(!isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
    }

    private bool AreOtherMenusActive()
    {
        foreach (GameObject menu in otherMenus)
        {
            if (menu.activeSelf)
            {
                return true;
            }
        }
        return false;
    }
}
