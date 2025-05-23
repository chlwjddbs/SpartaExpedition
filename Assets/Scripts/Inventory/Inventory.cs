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

        if (newItemData.itemData.canStack)                                                             //추가하려는 아이템이 중첩 가능한 아이템이면
        {
            var sameItems = invenItems.Where(i => i != null && i.itemData == newItemData.itemData);    //같은 종류의 아이템인지 확인하려면 스크럽터블 오브젝트를 비교해야된다. 인스턴스 비교는 다른 아이템으로 판정

            foreach (var invenItem in sameItems)                                                       //같은 아이템 리스트를 전부 받아온다.
            {
                if(invenItem.quantity == invenItem.itemData.maxQuantity)                               //같은 아이템이지만 더 이상 중첩 불가능 상태면 패스한다.
                { 
                    continue;
                }
                else                                                                                  //같은 아이템이고 중첩 가능한 상태면 아이템을 추가한다.
                {
                    int stackAbleQuntity = invenItem.itemData.maxQuantity - invenItem.quantity;
                    int slotNum = Array.IndexOf(invenItems, invenItem);

                    if(newItemData.quantity > stackAbleQuntity)                                        //최대 소지한도 만큼 추가하고, 개수가 남았다면 다음 중복 아이템을 찾아서 아이템을 추가한다.
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
