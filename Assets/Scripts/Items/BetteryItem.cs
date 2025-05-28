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
        return "EŰ�� ���� ���͸� ȸ��";
    }
}
