using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class TeleportChaser : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float teleportCooldown = 7f;  // �����̵� ��Ÿ��
    [SerializeField] float followRange = 30f;       // ���� ���� �Ÿ�
    [SerializeField] float gameOverDistance = 1f;   // ���� �Ÿ�

    [SerializeField] AudioSource footstepSource;
    [SerializeField] AudioClip[] footstepClips;
    [SerializeField] AudioClip teleportClip;

    [SerializeField] TMP_Text teleportUIText;
    [SerializeField] float teleportUIShowTime = 1f;

    [Header("Animation")]
    [SerializeField] Animator animator;

    private float timer; // ���� �̵� Ÿ�̸�
    private NavMeshAgent agent; // ���� NavMesh ���

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (teleportUIText != null)
        {
            teleportUIText.enabled = false;
        }
    }

    private void Update()
    {
        timer += Time.deltaTime; // �� �����Ӹ��� Ÿ�̸Ӹ� ������Ŵ

        float distance = Vector3.Distance(transform.position, player.position);

        bool isChasing = distance < followRange;

        // ���� ����
        if (distance < followRange)
        {
            agent.SetDestination(player.position);
        }

        // �ִϸ��̼� ó��
        float velocity = agent.velocity.magnitude;
        bool isWalking = velocity > 0.1f;

        if (animator != null)
        {
            animator.SetBool("IsWalk", isWalking);
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
        Vector3 offset = new Vector3(Random.Range(-7f, 7f), 0f, Random.Range(-7f, 7f));
        Vector3 newPosition = player.position + offset;

        NavMeshHit hit;

        if (NavMesh.SamplePosition(newPosition, out hit, 2f, NavMesh.AllAreas))
        {
            agent.Warp(hit.position);  // NavMesh ���� �����ϰ� �����̵�
        }

        // �����̵� ����
        if (footstepSource != null && teleportClip != null)
        {
            footstepSource.PlayOneShot(teleportClip);
        }

        // �����̵� UI ǥ��
        if (teleportUIText != null)
        {
            StopAllCoroutines();
            StartCoroutine(ShowTeleportMessage());
        }
    }

    private IEnumerator ShowTeleportMessage()
    {
        teleportUIText.enabled = true;
        teleportUIText.text = "�߰��ڰ� �����̵��մϴ�!";
        yield return new WaitForSeconds(teleportUIShowTime);
        teleportUIText.enabled = false;
    }


    private void GameOver()
    {
        if (GameManager._instance != null)
        {
            GameManager._instance.GameOver();
        }
    }

    public void OnFootstep()
    {
        if (footstepClips == null || footstepClips.Length == 0 || footstepSource == null) return;

        int index = Random.Range(0, footstepClips.Length);
        footstepSource.PlayOneShot(footstepClips[index]);
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
