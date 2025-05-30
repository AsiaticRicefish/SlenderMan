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
        }        
    }

    public void RetryGame()
    {
        Time.timeScale = 1f; // 멈췄던 시간 되돌리기
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // 현재 씬 다시 로드
    }

    public void Ending()
    {
        Debug.Log("모든 쪽지를 모았습니다!");
    }

}
