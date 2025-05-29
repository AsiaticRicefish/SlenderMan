using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor.Rendering;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    [SerializeField] GameObject[] itemPrefabs;
    [SerializeField] Transform[] spawnPoints;
    public void SpawnItemCount(int count)
    {
        if (itemPrefabs.Length == 0 || spawnPoints.Length == 0) return;

        List<Transform> points = new List<Transform>(spawnPoints);
        Shuffle(points);

        int spawnCount = Mathf.Min(count, points.Count); // 방어코드 : 혹시나 잘못 설정할 경우를 대비 (없어도 무관함)

        for (int i = 0; i < spawnCount; i++)
        {
            int itemIndex = Random.Range(0, itemPrefabs.Length);
            Instantiate(itemPrefabs[itemIndex], points[i].position, Quaternion.identity);
        }
    }

    private void Shuffle(List<Transform> list) // Fisher-Yates 셔플 알고리즘
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
}
