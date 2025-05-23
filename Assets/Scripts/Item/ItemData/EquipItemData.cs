using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = ("Item/EquipItem"))]
public class EquipItemData : ItemData
{
    public override ItemType itemType => ItemType.Equip;
    public EquipType equipType;

    public float attackPoint;
    public float defencePoint;
    public float healthPoint;
    public float manaPoint;
    public float staminaPoint;
    public float moveSpeedPoint;

    public override void UseItem()
    {
        throw new System.NotImplementedException();
    }
}

public enum EquipType
{
    Weapon,
    Head,
    Armor,
    Shoes,
    Ring,
}