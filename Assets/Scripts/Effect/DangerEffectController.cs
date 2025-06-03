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
        dangerUIText.text = "����!\n���� ���� ������ �ֽ��ϴ�";
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

        // ������ ������ ���¸� ���� ���·� ����
        if (flashlightController.IsFlashlightOn)
        {
            flashlight.enabled = true;
        }

        // �ؽ�Ʈ ���� �ʱ�ȭ
        Color color = dangerUIText.color;
        dangerUIText.color = new Color(color.r, color.g, color.b, 1f);
    }

    private void BlinkText()
    {
        if (dangerUIText != null)
        {
            float alpha = Mathf.PingPong(Time.time * 2f, 1f); // ���� ���� 0~1 ���̷� ��� �ٲٱ� (�ؽ�Ʈ�� �����ϴ� ��ó�� ����)
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
