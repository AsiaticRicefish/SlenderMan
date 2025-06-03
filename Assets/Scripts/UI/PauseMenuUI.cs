using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuUI: MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject mouseSettingCanvas;

    private bool isPaused = false;

    private void Start()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        mouseSettingCanvas.SetActive(false);
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (GameManager._instance.isGameEnd) return;

        if (mouseSettingCanvas.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CloseMouseSetting();
            }
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            GamePause();
        }
    }

    private void GamePause()
    {
        isPaused = !isPaused;
        pauseMenu.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void GameContinue()
    {
        GamePause();
    }


    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenuScene");
    }

    public void OpenMouseSetting()
    {
        mouseSettingCanvas.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void CloseMouseSetting()
    {
        mouseSettingCanvas.SetActive(false);
        pauseMenu.SetActive(true);
    }
}
