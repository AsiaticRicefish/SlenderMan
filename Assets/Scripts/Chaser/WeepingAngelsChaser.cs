using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class WeepingAngelsChaser : MonoBehaviour
{
    [SerializeField] private Transform player;
    private NavMeshAgent agent;
    private Animator animator;
    [SerializeField] float gameOverDistance = 1f;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // 플레이어 자동 탐색
        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
        }
    }

    private void Update()
    {
        if (!IsPlayerLookingAtMe())
        {
            // 플레이어가 안 보고 있음 → 추격
            agent.SetDestination(player.position);
        }
        else
        {
            // 멈춤
            agent.SetDestination(transform.position);
        }

        float speed = agent.velocity.magnitude;
        bool isWalking = speed > 0.1f;

        if (animator != null)
        {
            animator.SetBool("IsWalk", isWalking);
        }

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < gameOverDistance)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        if (GameManager._instance != null)
        {
            GameManager._instance.GameOver();
        }
    }

    private bool IsPlayerLookingAtMe()
    {
        Vector3 _direction = (transform.position - player.position).normalized;
        float dot = Vector3.Dot(player.forward, _direction);

        if (dot > 0.5f) // 정면 범위 안
        {
            // 시야에 들어오면 → Ray 쏴서 진짜 보이는지 확인
            Ray ray = new Ray(player.position + Vector3.up * 1.6f, _direction); // 눈 높이에서 Ray
            if (Physics.Raycast(ray, out RaycastHit hit, 50f))
            {
                if (hit.transform == transform)
                {
                    // Ray에 내가 직접 맞음 → 실제로 보고 있음
                    return true;
                }
            }
        }
        // 시야 바깥 or 가로막혀 있음
        return false;
    }
}
