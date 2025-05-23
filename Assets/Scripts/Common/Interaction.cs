using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction 
{
    public float maxInteractionDistance;

    public GameObject currentInteractionObject;
    public IInteractable currentInteraction;

    private int interactionLayer = 8;
    public LayerMask interactionMask;

    public Interaction(float maxInteractionDistance)
    {
        this.maxInteractionDistance = maxInteractionDistance;
        interactionMask = 1 << interactionLayer;  
    }

    public void GetInteraction()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
       
        if (Physics.Raycast(ray, out RaycastHit hit, maxInteractionDistance,interactionMask))
        {
            if (hit.transform.gameObject == currentInteractionObject) return;          
            currentInteraction?.SetInterface(false); //���ο� ���ͷ��� ������Ʈ�� ã�Ҵٸ� ������ �����ִ� ���ͷ��� ������Ʈ�� �������̽��� �����Ͽ����Ѵ�.

            if (hit.transform.TryGetComponent<IInteractable>(out currentInteraction))
            {
                currentInteractionObject = hit.transform.gameObject;
                currentInteraction.SetInterface(true);
            }
        }
        else
        {
            currentInteraction?.SetInterface(false);
            currentInteractionObject = null;
            currentInteraction = null;
        }
    }

    public void OnInteraction()
    {
        if (currentInteraction != null)
        {
            currentInteraction.SetInterface(false);
            currentInteraction.OnInteraction();
            currentInteractionObject = null;
            currentInteraction = null;
        }
    }
}
