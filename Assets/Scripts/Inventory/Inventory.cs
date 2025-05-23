using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Inventory
{
    public Inventory() 
    {
        for (int i = 0; i < invenItems.Length; i++)
        {
            invenItems[i] = new InventorySlotData();
        }
    }
    
    public const int inventoryMaxSize = 25;
    public InventorySlotData[] invenItems = new InventorySlotData[inventoryMaxSize];

    public int selectSlotNum;
    public InventorySlotData selectItem;

    public UnityAction<int, InventorySlotData> OnInventoryUpdate;

    public bool AddItem(ItemInstance newItem, out int usedQuantity)
    {
        Debug.Log(newItem);
        InventorySlotData newItemData = new InventorySlotData(newItem);
        usedQuantity = 0;

        if (newItemData.itemData.canStack)                                                             //�߰��Ϸ��� �������� ��ø ������ �������̸�
        {
            var sameItems = invenItems.Where(i => i != null && i.itemData == newItemData.itemData);    //���� ������ ���������� Ȯ���Ϸ��� ��ũ���ͺ� ������Ʈ�� ���ؾߵȴ�. �ν��Ͻ� �񱳴� �ٸ� ���������� ����

            foreach (var invenItem in sameItems)                                                       //���� ������ ����Ʈ�� ���� �޾ƿ´�.
            {
                if(invenItem.quantity == invenItem.itemData.maxQuantity)                               //���� ������������ �� �̻� ��ø �Ұ��� ���¸� �н��Ѵ�.
                { 
                    continue;
                }
                else                                                                                  //���� �������̰� ��ø ������ ���¸� �������� �߰��Ѵ�.
                {
                    int stackAbleQuntity = invenItem.itemData.maxQuantity - invenItem.quantity;
                    int slotNum = Array.IndexOf(invenItems, invenItem);

                    if(newItemData.quantity > stackAbleQuntity)                                        //�ִ� �����ѵ� ��ŭ �߰��ϰ�, ������ ���Ҵٸ� ���� �ߺ� �������� ã�Ƽ� �������� �߰��Ѵ�.
                    {
                        invenItem.quantity += stackAbleQuntity;
                        newItemData.quantity -= stackAbleQuntity;
                        usedQuantity += stackAbleQuntity;
                        OnInventoryUpdate?.Invoke(slotNum, invenItem);
                    }
                    else
                    {
                        invenItem.quantity += newItemData.quantity;
                        OnInventoryUpdate?.Invoke(slotNum, invenItem);
                        return true;
                    }
                }
            }
        }


        for (int i = 0; i < invenItems.Length; i++)
        {
            if (invenItems[i].itemData == null)
            {
                invenItems[i] = newItemData;
                Debug.Log(newItemData.itemData.itemName);
                OnInventoryUpdate?.Invoke(i , newItemData);
                return true;
            }
        }

        return false;
    }

    public void UseItem()
    {
        if (selectItem == null) return;

        selectItem.UseItem();

        if (selectItem.quantity == 0)
        {
            RemoveItem();
        }
        OnInventoryUpdate?.Invoke(selectSlotNum, selectItem);
    }

    public void RemoveItem()
    {
        invenItems[selectSlotNum] = null;
    }

    public void SelectItem(InvenSlot slotData)
    {
        selectSlotNum = slotData.slotNum;
        selectItem = slotData.slotItem;
    }

    public void DeselectItem()
    {
        selectSlotNum = -1;
        selectItem = null;
    }
}
