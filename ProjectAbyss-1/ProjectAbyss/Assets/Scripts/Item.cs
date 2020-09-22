using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
   public enum ItemType
    {
        Weapon,
        Healing,
        Ammo,
        KeyItem
    }
    public ItemType itemType;
    public int amount;
}
