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
        //Debug.Log($"{slot}��° ���Կ� {slotItem}�� ���Խ��ϴ�.");
        invenSlots[slot].UpdateSlot(slotItem);
    }

    public void OnSelectSlot(InvenSlot selectSlot)
    {
        if (this.selectSlot == selectSlot) //���� ������ ������ ������ ������ ���԰� ���ٸ� ���õ� ���� ����ó��
        {
            this.selectSlot = null;
            inventory.DeselectItem();
            return;
        }

        this.selectSlot?.DeselectSlot();  //���� ������ �������� ���ְ�
        this.selectSlot = selectSlot;     //���� ������ ���� ���
        inventory.SelectItem(selectSlot);
    }
}
