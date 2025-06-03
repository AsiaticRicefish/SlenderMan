using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NormalChaser : MonoBehaviour
{
    private Transform player;
    private NavMeshAgent agent;
    private Animator animator;
    [SerializeField] float gameOverDistance = 1f;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            player = playerObj.transform;
        }
    }

    private void Update()
    {
        if (player != null)
        {
            agent.SetDestination(player.position);

            float speed = agent.velocity.magnitude;
            bool isWalking = speed > 0.1f;

            if (animator != null)
            {
                animator.SetBool("IsWalk", isWalking);
            }

            float distance = Vector3.Distance(transform.position, player.position);
            if (distance <= gameOverDistance)
            {
                GameOver();
            }
        }
    }

    private void GameOver()
    {
        if (GameManager._instance != null)
        {
            GameManager._instance.GameOver();
        }
    }

}
