using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    private Inventory inventory;

    public Transform invenSlotPosition;
    public InvenSlot invenSlotPrefab;

    public List<InvenSlot> invenSlots = new List<InvenSlot>();

    private bool isOpen;

    public InvenSlot selectSlot;

    public Button useButton;


    public void Init()
    {
        inventory = GameManager.Instance.player.Inventory;
        GameManager.Instance.player.PlayerInput.actions["Inventory"].started += Toggle;
        inventory.OnInventoryUpdate += OnUpdateSlot;
        useButton.onClick.AddListener(inventory.UseItem);

        for (int i = 0; i < Inventory.inventoryMaxSize; i++)
        {
            invenSlots.Add(Instantiate(invenSlotPrefab, invenSlotPosition));
            invenSlots[i].Init(i);
            invenSlots[i].OnSelectSlotHandler += OnSelectSlot;
        }
        CloseUI();
    }

    public void Toggle(InputAction.CallbackContext context)
    {
        if (isOpen)
        {
            CloseUI();
        }
        else
        {
            OpenUI();
        }
    }

    public void OpenUI()
    {
        isOpen = true;
        Cursor.lockState = CursorLockMode.None;
        gameObject.SetActive(true);
    }

    public void CloseUI()
    {
        isOpen = false;
        Cursor.lockState = CursorLockMode.Locked;
        gameObject.SetActive(false);

        selectSlot?.DeselectSlot();
        selectSlot = null;     
    }

    public void OnUpdateSlot(int slot , InventorySlotData slotItem)
    {
        //Debug.Log($"{slot}번째 슬롯에 {slotItem}이 들어왔습니다.");
        invenSlots[slot].UpdateSlot(slotItem);
    }

    public void OnSelectSlot(InvenSlot selectSlot)
    {
        if (this.selectSlot == selectSlot) //새로 선택한 슬롯이 기존에 선택한 슬롯과 같다면 선택된 슬롯 없음처리
        {
            this.selectSlot = null;
            inventory.DeselectItem();
            return;
        }

        this.selectSlot?.DeselectSlot();  //기존 슬롯을 선택해제 해주고
        this.selectSlot = selectSlot;     //새로 선택한 슬롯 등록
        inventory.SelectItem(selectSlot);
    }
}
