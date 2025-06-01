using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChaserSpawner : MonoBehaviour
{
    [SerializeField] private GameObject chaser2Prefab;
    [SerializeField] private float spawnInterval = 15f;
    [SerializeField] private int maxChaserCount = 7;
    [SerializeField] private Transform[] spawnPoints;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI spawnText;

    private float timer = 0f;
    private int currentCount = 0;

    private void Update()
    {
        if (currentCount >= maxChaserCount) return;

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnChaser();
            timer = 0f;
        }
    }

    private void SpawnChaser()
    {
        int index = Random.Range(0, spawnPoints.Length);
        Transform point = spawnPoints[index];

        Instantiate(chaser2Prefab, point.position, Quaternion.identity);
        currentCount++;

        if (spawnText != null)
        {
            StartCoroutine(ShowBlinkingText("�׵��� ��Ȱ�մϴ�..."));
        }
    }


    private IEnumerator ShowBlinkingText(string message)
    {
        spawnText.text = message;
        spawnText.enabled = true;

        for (int i = 0; i < 12; i++) // �� 6�� ������ (On-Off 12ȸ)
        {
            spawnText.enabled = !spawnText.enabled;
            yield return new WaitForSeconds(0.2f); // ������ ����
        }
        spawnText.enabled = false;
    }
}
