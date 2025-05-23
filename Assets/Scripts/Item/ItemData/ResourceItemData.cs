using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = ("Item/ResourceItem"))]
public class ResourceItemData : ItemData
{
    public override ItemType itemType => ItemType.Resource;
    public ResourceType resourceType;

    public override void UseItem()
    {
        
    }
}

public enum ResourceType
{
    Material,
    Alchemy,
    Quest,
}
