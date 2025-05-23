using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemData : ScriptableObject
{
    public int itemCode;
    public string itemName;
    public string itemDescription;

    public abstract ItemType itemType { get; }
    public Sprite icon;
    public GameObject fieldItemPrefab;

    public bool canStack;
    public int maxQuantity;

    public abstract void UseItem();
}

public enum ItemType
{
    Equip,
    Useable,
    Resource
}
