using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperItem : Item
{
    public override void Interact()
    {
        GameManager._instance.CollectNote();
        Destroy(gameObject);
    }

    public override string GetInteractionText()
    {
        return "E키를 눌러 쪽지를 획득합니다";
    }
}
