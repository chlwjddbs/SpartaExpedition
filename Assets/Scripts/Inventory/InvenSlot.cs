using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InvenSlot : MonoBehaviour
{
    public int slotNum;
    public InventorySlotData slotItem;

    public Outline itemFrame;
    public Image itemImage;
    public TextMeshProUGUI quantityText;

    public Button slotButton;

    private Color selectColor = new Color(0,245/255f,1);
    private Color noramlColor = Color.white;
    private Color nonItemColor = Color.black;

    private bool isSelect;

    public UnityAction<InvenSlot> OnSelectSlotHandler;

    public void Init(int slotNum)
    {
        this.slotNum = slotNum;
        slotButton.onClick.AddListener(Toggle);
        itemImage.sprite = null;
        itemImage.color = nonItemColor;
        quantityText.enabled = false;
        DeselectSlot();
    }

    public void AddItem(InventorySlotData addItem)
    {
        slotItem = addItem;
        itemImage.enabled = true;
        itemImage.sprite = addItem.itemData.icon;
        itemImage.color = noramlColor;
    }

    public void RemoveItem()
    {
        slotItem = null;
        itemImage.sprite = null;
        itemImage.color = nonItemColor;
        quantityText.enabled = false;

        DeselectSlot();
    }

    public void UpdateSlot(InventorySlotData item)
    {
        //Debug.Log(item.itemData.itemName);
        if (item != null && item.itemData != null && item.quantity > 0)
        {
            slotItem = item;

            itemImage.sprite = item.itemData.icon;
            itemImage.color = noramlColor;

            if (slotItem.quantity > 1)
            {
                quantityText.enabled = true;
                quantityText.text = slotItem.quantity.ToString();
            }
            else
            {
                quantityText.enabled = false;
            }
        }
        else
        {
            RemoveItem();
        }
    }

    public void Toggle()
    {
        if (isSelect)
        {
            DeselectSlot();
        }
        else
        {
            SelectSlot();
        }
    }

    public void SelectSlot()
    {
        if (slotItem?.itemData == null) return;

        isSelect = true;
        itemFrame.effectColor = selectColor;
        OnSelectSlotHandler?.Invoke(this);
    }

    public void DeselectSlot()
    {
        isSelect = false;
        itemFrame.effectColor = noramlColor;
        OnSelectSlotHandler?.Invoke(this);
    }
}
