using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    [SerializeField] Light flashlight;
    [SerializeField] AudioSource switchSound;

    private void Start()
    {
        flashlight.enabled = false;
    }

    private void Update()
    {
        FlashOnOff();
    }

    private void FlashOnOff()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            flashlight.enabled = !flashlight.enabled;

            switchSound.Play();
        }
    }
}
