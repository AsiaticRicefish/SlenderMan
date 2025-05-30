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
        Time.timeScale = 1f; // Ȥ�� ���� ������ ���� �־��� ��� ���

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
        paperCounterText.text = $"������ ����  {collectPaperCount} / {totalPaperCount}";
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
        Time.timeScale = 1f; // ����� �ð� �ǵ�����
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // ���� �� �ٽ� �ε�
    }

    public void Ending()
    {
        Debug.Log("��� ������ ��ҽ��ϴ�!");
    }

}
