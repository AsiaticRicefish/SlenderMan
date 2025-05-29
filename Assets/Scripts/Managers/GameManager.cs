using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    private int collectPaperCount = 0;
    [SerializeField] private int totalPaperCount = 5;

    [SerializeField] private TextMeshProUGUI paperCounterText;

    [SerializeField] ItemSpawn paperSpawner;
    [SerializeField] ItemSpawn betterySpawner;


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

    public void Ending()
    {
        Debug.Log("모든 쪽지를 모았습니다!");
    }

}
