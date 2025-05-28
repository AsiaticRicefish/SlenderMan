using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlendermanController : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float teleportCooldown = 10f;  // 순간이동 쿨타임
    [SerializeField] float followRange = 15f;       // 추적 시작 거리
    [SerializeField] float gameOverDistance = 1f;   // 접촉 거리

    private float timer; // 순간 이동 타이머

    private NavMeshAgent agent; // 추적 NavMesh 사용

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        timer += Time.deltaTime; // 매 프레임마다 타이머를 증가시킴

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < followRange)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            // agent.ResetPath(); // 추적 중지
        }


        // 순간이동
        if (timer > teleportCooldown)
        {
            TeleportNearPlayer();
            timer = 0f;
        }

        // 접촉 시 게임오버
        if (distance < gameOverDistance)
        {
            GameOver();
        }

    }

    private void TeleportNearPlayer() // 순간이동
    {
        Vector3 offset = new Vector3(Random.Range(-10, 10), 0f, Random.Range(-10, 10));
        Vector3 newPosition = player.position + offset;

        NavMeshHit hit;

        if (NavMesh.SamplePosition(newPosition, out hit, 2f, NavMesh.AllAreas))
        {
            agent.Warp(hit.position);  // NavMesh 위로 안전하게 순간이동
            Debug.Log("슬렌더맨 순간이동!");
        }
    }

    private void GameOver()
    {
        Debug.Log("게임 오버");
    }

    private void OnDrawGizmos()
    {
        // 추적 범위 (파란색)
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, followRange);

        // 게임 오버 거리 (빨간색)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, gameOverDistance);
    }
}
