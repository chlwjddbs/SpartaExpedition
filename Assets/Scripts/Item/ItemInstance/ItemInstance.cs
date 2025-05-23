using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class ItemInstance : MonoBehaviour, IInteractable
{
    public ItemData itemData;
    public int quantity = 1;

    public virtual void OnInteraction() 
    {
        int used = 0;
        if (GameManager.Instance.player.Inventory.AddItem(this ,out used))
        {
            Destroy(gameObject);
        }
        else
        {
            quantity -= used;
        }
    }
    
    public virtual void SetInterface(bool active)
    {
        string itemDescription = "";

        if (active)
        {
            itemDescription = $"{itemData.itemName} \n {itemData.itemDescription}";
        }

        UIManager.Instance.SetInteractionText(active, itemDescription);
    }

    public virtual void UseItem() { }
}
