using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public static bool GamePaused = false, gameOver = false;

    public GameObject PauseMenuUI;
    public GameObject GameOverUI;
    public GameObject BoreSight, MouseAim;
    public TextMeshProUGUI scoreText;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamePaused && !gameOver)
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

    public void GameOver()
    {
        BoreSight.SetActive(false);
        MouseAim.SetActive(false);
        GamePaused = true;
        gameOver = true;
        GameOverUI.SetActive(true);
        Time.timeScale = 0;
        scoreText.text = "SCORE: " + GameManager.Instance.points.ToString();
    }

}
