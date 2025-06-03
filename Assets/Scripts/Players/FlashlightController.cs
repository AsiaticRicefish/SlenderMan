using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashlightController : MonoBehaviour
{
    [SerializeField] Light flashlight;
    [SerializeField] AudioSource switchSound;
    [SerializeField] Image[] batteryBars; // ���͸� UI ĭ

    [SerializeField] public float maxBatteryTime = 60f;
    [HideInInspector] public float currentBatteryTime;

    private bool isOn;  // �������� ���� �ִ� ����
    [HideInInspector] public bool IsFlashlightOn => isOn;


    private void Start()
    {
        currentBatteryTime = maxBatteryTime;
        flashlight.enabled = false;
    }

    private void Update()
    {
        if (GameManager._instance.isGameEnd || Time.timeScale == 0f) return;
        FlashOnOff();

        if (isOn) DrainBattery(); // ���� ������ ���͸��� �Ҹ�
    }

    private void FlashOnOff()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            // ���͸��� ������ ������ ����
            if (currentBatteryTime <= 0f) return;

            isOn = !isOn;
            flashlight.enabled = isOn;

            if (switchSound && !switchSound.isPlaying)
            {
                switchSound.Play();
            }
        }
    }

    private void DrainBattery()
    {
        currentBatteryTime -= Time.deltaTime;  // �������� ���� �ִ� ���� �� �����Ӹ��� ���͸� �ð� ����

        if (currentBatteryTime <= 0f)  // ���͸� �� ������ ������ ������ ����
        {
            currentBatteryTime = 0f;
            flashlight.enabled = false;
            isOn = false;
        }

        UpdateBatteryUI();
    }

    private void UpdateBatteryUI()
    {
        float percent = currentBatteryTime / maxBatteryTime; // ���� ���͸��� ����(%)�� ����Ͽ� ��ĭ�� ���������� ����

        for (int i = 0; i < batteryBars.Length; i++)
        {
            batteryBars[i].enabled = percent > (float)i / batteryBars.Length;
        }
    }

    public void BatteryRecovery()  // ���͸� ȸ��
    {
        currentBatteryTime = maxBatteryTime;
        UpdateBatteryUI();
    }

}
