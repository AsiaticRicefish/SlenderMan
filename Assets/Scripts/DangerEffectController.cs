using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DangerEffectController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Light flashlight;
    [SerializeField] private AudioSource dangerSound;
    [SerializeField] private TMP_Text dangerUIText;

    [SerializeField] private float dangerDistance = 5f;
    private bool isDangerActive = false;

    [SerializeField] private FlashlightController flashlightController;

    private void Update()
    {

        Transform closestChaser = FindClosestChaser();

        if (closestChaser == null) return;

        float distance = Vector3.Distance(closestChaser.position, player.position);

        if (distance < dangerDistance)
        {
            if (!isDangerActive)
            {
                StartDangerEffect();
            }

            if (flashlightController != null && flashlightController.IsFlashlightOn)
            {
                BlinkFlashlight();
            }

            BlinkText();
        }
        else
        {
            if (isDangerActive)
            {
                StopDangerEffect();
            }
        }
    }


    private Transform FindClosestChaser()
    {
        GameObject[] chasers = GameObject.FindGameObjectsWithTag("Chaser");

        Transform closest = null;
        float minDistance = float.MaxValue;

        foreach (GameObject chaserObj in chasers)
        {
            float dist = Vector3.Distance(chaserObj.transform.position, player.position);
            if (dist < minDistance)
            {
                minDistance = dist;
                closest = chaserObj.transform;
            }
        }

        return closest;
    }

    private void StartDangerEffect()
    {
        isDangerActive = true;

        dangerUIText.enabled = true;
        dangerUIText.text = "위험!\n적이 아주 가까이 있습니다";
        dangerUIText.alignment = TextAlignmentOptions.Center;

        dangerSound.volume = 1f;
        dangerSound.loop = true;
        dangerSound.Play();
    }

    private void StopDangerEffect()
    {
        isDangerActive = false;
        dangerUIText.enabled = false;
        dangerSound.Stop();
        dangerSound.volume = 0f;

        // 손전등 꺼졌던 상태면 꺼진 상태로 유지
        if (flashlightController.IsFlashlightOn)
        {
            flashlight.enabled = true;
        }

        // 텍스트 색상 초기화
        Color color = dangerUIText.color;
        dangerUIText.color = new Color(color.r, color.g, color.b, 1f);
    }

    private void BlinkText()
    {
        if (dangerUIText != null)
        {
            float alpha = Mathf.PingPong(Time.time * 2f, 1f); // 알파 값을 0~1 사이로 계속 바꾸기 (텍스트가 깜빡하는 것처럼 보임)
            Color color = dangerUIText.color;
            dangerUIText.color = new Color(color.r, color.g, color.b, alpha);
        }
    }

    private void BlinkFlashlight()
    {
        if (!flashlightController.IsFlashlightOn) return;

        flashlight.enabled = Mathf.FloorToInt(Time.time * 10f) % 2 == 0;
    }

}
