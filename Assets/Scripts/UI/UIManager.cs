using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject howToPlayPanel;
    [SerializeField] GameObject gameSelectPanel;

    public void Update()
    {
        if (howToPlayPanel.activeSelf)
        {
            HideHowToPlay();
        }

        if (gameSelectPanel.activeSelf) 
        {
            HideGameSelect();   
        }

    }

    public void SecletGhostVillage()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void SelectChurch()
    {
        SceneManager.LoadScene("GameScene2");
    }


    public void GameStart()
    {
        gameSelectPanel.SetActive(true);
    }

    public void ShowHowToPlay()
    {
        howToPlayPanel.SetActive(true);
    }

    public void HideGameSelect()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameSelectPanel.SetActive(false);
        }
    }

    public void HideHowToPlay()
    {
        if (Input.anyKeyDown || Input.GetKeyDown(KeyCode.Escape))
        {
            howToPlayPanel.SetActive(false);
        }
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
