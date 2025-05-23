using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IInteractable
{
    void SetInterface(bool active);

    void OnInteraction();
}
