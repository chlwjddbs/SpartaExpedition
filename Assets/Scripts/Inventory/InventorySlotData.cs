using System;
using UnityEngine;

[Serializable]
public class InventorySlotData 
{
    public InventorySlotData() { }

    public InventorySlotData(ItemInstance item)  
    {
        itemData = item.itemData;
        quantity = item.quantity;
    }

    public ItemData itemData;
    public int quantity;

    public void UseItem()
    {
        itemData.UseItem();
        quantity--;
    }
}
