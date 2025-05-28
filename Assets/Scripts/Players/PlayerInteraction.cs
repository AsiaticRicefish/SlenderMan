using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] float interactRange = 2f;
    [SerializeField] LayerMask interactLayer;
    [SerializeField] Camera cam;
    [SerializeField] TMP_Text interactionText;

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

                if (Input.GetKeyDown(KeyCode.E))
                {
                    item.Interact();
                }

                return;
            }
        }

        interactionText.enabled = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}
