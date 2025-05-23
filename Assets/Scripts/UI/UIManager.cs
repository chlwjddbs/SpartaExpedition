using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public delegate void HpChangeHandler(float currentHealth, float maxHealth);
    public HpChangeHandler OnChangeHealth;

    public HpBar hpBar;
    public TextMeshProUGUI interactionText;

    public InventoryUI inventoryUI;

    public void Init()
    {
        hpBar.Init();
        inventoryUI.Init();
        interactionText.enabled = false;
    }

    public void SetInteractionText(bool active, string str = "")
    {
        interactionText.enabled = active;
        interactionText.text = str;
    }
}
