using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlendermanController : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float teleportCooldown = 10f;  // �����̵� ��Ÿ��
    [SerializeField] float followRange = 15f;       // ���� ���� �Ÿ�
    [SerializeField] float gameOverDistance = 1f;   // ���� �Ÿ�

    private float timer; // ���� �̵� Ÿ�̸�

    private NavMeshAgent agent; // ���� NavMesh ���

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        timer += Time.deltaTime; // �� �����Ӹ��� Ÿ�̸Ӹ� ������Ŵ

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < followRange)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            // agent.ResetPath(); // ���� ����
        }


        // �����̵�
        if (timer > teleportCooldown)
        {
            TeleportNearPlayer();
            timer = 0f;
        }

        // ���� �� ���ӿ���
        if (distance < gameOverDistance)
        {
            GameOver();
        }

    }

    private void TeleportNearPlayer() // �����̵�
    {
        Vector3 offset = new Vector3(Random.Range(-10, 10), 0f, Random.Range(-10, 10));
        Vector3 newPosition = player.position + offset;

        NavMeshHit hit;

        if (NavMesh.SamplePosition(newPosition, out hit, 2f, NavMesh.AllAreas))
        {
            agent.Warp(hit.position);  // NavMesh ���� �����ϰ� �����̵�
            Debug.Log("�������� �����̵�!");
        }
    }

    private void GameOver()
    {
        Debug.Log("���� ����");
    }

    private void OnDrawGizmos()
    {
        // ���� ���� (�Ķ���)
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, followRange);

        // ���� ���� �Ÿ� (������)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, gameOverDistance);
    }
}
