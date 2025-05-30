using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] float interactRange = 4f;
    [SerializeField] LayerMask interactLayer;
    [SerializeField] Camera cam;
    [SerializeField] TMP_Text interactionText;

    [SerializeField] float interactionCooldown = 0.5f; // �������� ������ ȹ�� ���� ��Ÿ��
    private float lastInteractionTime = float.MinValue; // ���� ���� ���Ŀ��� ��Ÿ�� ���� �ٷ� ��ȣ�ۿ� ���� (float.MinValue�� ���� ���� ��)

    private void Update()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        if (Physics.SphereCast(ray, 0.3f, out RaycastHit hit, interactRange, interactLayer))
        {
            Item item = hit.collider.GetComponent<Item>();
            if (item != null)
            {
                interactionText.text = item.GetInteractionText();
                interactionText.enabled = true;

                // ��Ÿ�� üũ (�������� �������� �Ծ����� �κ��� ����)
                if (Input.GetKeyDown(KeyCode.E) && Time.time - lastInteractionTime > interactionCooldown)
                {
                    lastInteractionTime = Time.time;
                    item.Interact();
                }
                return;
            }
        }
        interactionText.enabled = false;
    }
}
