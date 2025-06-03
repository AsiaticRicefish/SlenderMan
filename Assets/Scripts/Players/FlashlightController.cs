using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashlightController : MonoBehaviour
{
    [SerializeField] Light flashlight;
    [SerializeField] AudioSource switchSound;
    [SerializeField] Image[] batteryBars; // 배터리 UI 칸

    [SerializeField] public float maxBatteryTime = 60f;
    [HideInInspector] public float currentBatteryTime;

    private bool isOn;  // 손전등이 켜져 있는 상태
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

        if (isOn) DrainBattery(); // 켜져 있으면 배터리를 소모
    }

    private void FlashOnOff()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            // 배터리가 없으면 켜지지 않음
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
        currentBatteryTime -= Time.deltaTime;  // 손전등이 켜져 있는 동안 매 프레임마다 배터리 시간 감소

        if (currentBatteryTime <= 0f)  // 배터리 다 닳으면 손전등 강제로 꺼짐
        {
            currentBatteryTime = 0f;
            flashlight.enabled = false;
            isOn = false;
        }

        UpdateBatteryUI();
    }

    private void UpdateBatteryUI()
    {
        float percent = currentBatteryTime / maxBatteryTime; // 현재 배터리의 비율(%)을 계산하여 한칸씩 없어지도록 만듦

        for (int i = 0; i < batteryBars.Length; i++)
        {
            batteryBars[i].enabled = percent > (float)i / batteryBars.Length;
        }
    }

    public void BatteryRecovery()  // 배터리 회복
    {
        currentBatteryTime = maxBatteryTime;
        UpdateBatteryUI();
    }

}
