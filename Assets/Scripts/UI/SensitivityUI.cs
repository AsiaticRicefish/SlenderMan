using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SensitivityUI : MonoBehaviour
{
    [SerializeField] private PlayerLook playerLook;
    [SerializeField] private Slider sensitivitySlider;
    [SerializeField] private TextMeshProUGUI sensitivityText;

    private void Start()
    {
        float savedSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 100f);
        sensitivitySlider.value = savedSensitivity;
        UpdateSensitivityUI(savedSensitivity);
    }

    public void OnSliderValueChanged(float value)
    {
        playerLook.SetSensitivity(value);
        UpdateSensitivityUI(value);
    }

    private void UpdateSensitivityUI(float value)
    {
        sensitivityText.text = $"마우스 감도: {value:F2}";
    }
}
