using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetteryItem : Item
{
    [SerializeField] AudioSource betteryAudio;

    public override void Interact()
    {
        FlashlightController flashlight = FindObjectOfType<FlashlightController>();
        if (flashlight != null) 
        {
            if (betteryAudio != null)
            {
                betteryAudio.Play();
            }

            flashlight.BatteryRecovery();
            Destroy(gameObject, betteryAudio.clip.length);
        }
    }

    public override string GetInteractionText()
    {
        return "E키를 눌러 배터리 회복";
    }
}
