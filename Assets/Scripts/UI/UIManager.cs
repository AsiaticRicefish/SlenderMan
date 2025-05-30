using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject howToPlayPanel;

    public void Update()
    {
        if (howToPlayPanel.activeSelf)
        {
            HideHowToPlay();
        }
    }

    public void GameStart()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ShowHowToPlay()
    {
        howToPlayPanel.SetActive(true);
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
