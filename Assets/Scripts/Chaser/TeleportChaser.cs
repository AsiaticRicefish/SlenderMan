using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class TeleportChaser : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float teleportCooldown = 7f;
    [SerializeField] float followRange = 30f;     
    [SerializeField] float gameOverDistance = 1f;

    [SerializeField] AudioSource footstepSource;
    [SerializeField] AudioClip[] footstepClips;
    [SerializeField] AudioClip teleportClip;

    [SerializeField] TMP_Text teleportUIText;
    [SerializeField] float teleportUIShowTime = 1f;

    [Header("Animation")]
    [SerializeField] Animator animator;

    private float timer; 
    private NavMeshAgent agent;

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
        timer += Time.deltaTime;

        float distance = Vector3.Distance(transform.position, player.position);

        bool isChasing = distance < followRange;

        // 추적 조건
        if (distance < followRange)
        {
            agent.SetDestination(player.position);
        }

        float velocity = agent.velocity.magnitude;
        bool isWalking = velocity > 0.1f;

        if (animator != null)
        {
            animator.SetBool("IsWalk", isWalking);
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
        Vector3 offset = new Vector3(Random.Range(-7f, 7f), 0f, Random.Range(-7f, 7f));
        Vector3 newPosition = player.position + offset;

        NavMeshHit hit;

        if (NavMesh.SamplePosition(newPosition, out hit, 2f, NavMesh.AllAreas))
        {
            agent.Warp(hit.position);  // NavMesh 위로 안전하게 순간이동
        }

        // 순간이동 사운드
        if (footstepSource != null && teleportClip != null)
        {
            footstepSource.PlayOneShot(teleportClip);
        }

        // 순간이동 UI 표시
        if (teleportUIText != null)
        {
            StopAllCoroutines();
            StartCoroutine(ShowTeleportMessage());
        }
    }

    private IEnumerator ShowTeleportMessage()
    {
        teleportUIText.enabled = true;
        teleportUIText.text = "추격자가 순간이동합니다!";
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
}
