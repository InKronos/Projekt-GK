using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GamePaused = false;

    public GameObject PauseMenuUI;
    public GameObject BoreSight, MouseAim;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        BoreSight.SetActive(true);
        MouseAim.SetActive(true);
        GamePaused = false;
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1;
    }

    void Pause()
    {
        BoreSight.SetActive(false);
        MouseAim.SetActive(false);
        GamePaused = true;
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
