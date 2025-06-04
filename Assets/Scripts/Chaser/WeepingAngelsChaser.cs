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

        // �÷��̾� �ڵ� Ž��
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
            // �÷��̾ �� ���� ���� �� �߰�
            agent.SetDestination(player.position);
        }
        else
        {
            // ����
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

        if (dot > 0.5f) // ���� ���� ��
        {
            // �þ߿� ������ �� Ray ���� ��¥ ���̴��� Ȯ��
            Ray ray = new Ray(player.position + Vector3.up * 1.6f, _direction); // �� ���̿��� Ray
            if (Physics.Raycast(ray, out RaycastHit hit, 50f))
            {
                if (hit.transform == transform)
                {
                    // Ray�� ���� ���� ���� �� ������ ���� ����
                    return true;
                }
            }
        }
        // �þ� �ٱ� or ���θ��� ����
        return false;
    }
}
