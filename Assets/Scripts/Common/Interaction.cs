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
            currentInteraction?.SetInterface(false); //새로운 인터렉션 오브젝트를 찾았다면 기존에 켜져있던 인터렉션 오브젝트의 인터페이스를 오프하여야한다.

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
