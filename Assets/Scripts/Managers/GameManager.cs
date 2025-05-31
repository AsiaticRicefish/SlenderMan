using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;   

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    private int collectPaperCount = 0;
    [SerializeField] private int totalPaperCount = 5;

    [SerializeField] private TextMeshProUGUI paperCounterText;

    [SerializeField] ItemSpawn paperSpawner;
    [SerializeField] ItemSpawn betterySpawner;

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject gameClearPanel;

    [SerializeField] private GameObject playerUI;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Time.timeScale = 1f; // 혹시 이전 씬에서 멈춰 있었을 경우 대비

        UpdateNoteUI();
        paperSpawner.SpawnItemCount(5);
        betterySpawner.SpawnItemCount(2);
    }

    public void CollectNote()
    {
        collectPaperCount++;
        UpdateNoteUI();
        
        if (collectPaperCount >= totalPaperCount)
        {
            Ending();
        }
    }

    private void UpdateNoteUI()
    {
        paperCounterText.text = $"수집한 쪽지  {collectPaperCount} / {totalPaperCount}";
    }

    public void GameOver()
    {
        Time.timeScale = 0f;

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            playerUI.SetActive(false);
        }        
    }

    public void RetryGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Ending()
    {
        Time.timeScale = 0f;

        if (gameClearPanel != null)
        {
            gameClearPanel.SetActive(true);
            playerUI.SetActive(false);
        }
    }
}
