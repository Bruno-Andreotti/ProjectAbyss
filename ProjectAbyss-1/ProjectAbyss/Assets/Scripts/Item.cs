using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{//essa classe seria parte do sistema de inventário, mas este nao foi implementado
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
