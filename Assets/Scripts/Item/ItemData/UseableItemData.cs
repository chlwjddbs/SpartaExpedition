using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = ("Item/UseableItem"))]
public class UseableItemData : ItemData
{
    public override ItemType itemType => ItemType.Useable;
    public List<UseEffect> useEffect;

    public override void UseItem()
    {
        for (int i = 0; i < useEffect.Count; i++) 
        {
            (GameManager.Instance.player.stat as PlayerStat).ApplyUseItem(useEffect[i]);
        }
    }
}

[Serializable]
public class UseEffect
{
    public UseableType useableType;
    public float amount;
    public float duration;
}

public enum UseableType
{
    HP,
    Mana,
    Stamina,
    MoveSpeed,
}