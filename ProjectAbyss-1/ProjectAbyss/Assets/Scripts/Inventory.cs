using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{//essa classe seria utilizada para organizar um sistema de inventario, mas a mecanica nao pôde ser implementada de modo satisfatorio
    private List<Item> itemList;
    public Inventory()
    {
        itemList = new List<Item>();

        AddItem(new Item { itemType = Item.ItemType.Weapon, amount = 1 });
        Debug.Log(itemList.Count);
    }

    public void AddItem(Item item)
    {
        itemList.Add(item);
    }

}
