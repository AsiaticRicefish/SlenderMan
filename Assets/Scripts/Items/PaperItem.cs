using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperItem : Item
{
    [SerializeField] AudioSource paperAudio;
    public override void Interact()
    {
        if (paperAudio != null)
        {
            paperAudio.Play();
        }

        GameManager._instance.CollectNote();
        Destroy(gameObject, paperAudio.clip.length);
    }

    public override string GetInteractionText()
    {
        return "E키를 눌러 쪽지를 획득합니다";
    }
}
