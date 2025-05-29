using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public abstract void Interact();
    public virtual string GetInteractionText() => "EŰ�� ���� ��ȣ�ۿ�";
}
