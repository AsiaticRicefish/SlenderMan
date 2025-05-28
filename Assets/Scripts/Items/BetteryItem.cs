using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetteryItem : Item
{
    public override void Interact()
    {
        FlashlightController flashlight = FindObjectOfType<FlashlightController>();
        if (flashlight != null) 
        {
            flashlight.BatteryRecovery();
            Destroy(gameObject);
        }
    }

    public override string GetInteractionText()
    {
        return "E키를 눌러 배터리 회복";
    }
}
