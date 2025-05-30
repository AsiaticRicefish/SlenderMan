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

    [SerializeField] float interactionCooldown = 0.5f; // 연속으로 아이템 획득 방지 쿨타임
    private float lastInteractionTime = float.MinValue; // 게임 시작 직후에도 쿨타임 없이 바로 상호작용 가능 (float.MinValue는 아주 작은 수)

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

                // 쿨타임 체크 (연속으로 아이템이 먹어지는 부분을 수정)
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
