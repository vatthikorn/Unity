/*
    Nathan Cruz

    NOT COMPLETE

    Inventory for all the items the player has. Manipulates items by picking up objects from the screen, dragging and dropping in the inventory screen (NOT IMPLEMENTED).

    Dependency:
    Item.cs
    ItemDatabase.cs

    Required:
    Attached a gameobject "Inventory".
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

    //Need to reference ItemManager
    public GameObject itemDatabase;

    public List<Item> items = new List<Item>();
    public List<Item> equipment = new List<Item>();
    public List<Item> sigils = new List<Item>();
    public List<Item> consumables = new List<Item>();
    public List<Item> keyItems = new List<Item>();

    //Called by ItemDrop.cs, adds an item the inventory when the player picks up an item
    //Stores item in correct section (All and one of the four other specific categories)
    public void AddItemFromDrop(int itemID)
    {
        Item newItem = itemDatabase.GetComponent<ItemDatabase>().items[itemID];

        items.Add(newItem);

        switch(newItem.itemType)
        {
            case Item.ItemType.weapon:
            case Item.ItemType.armor:
            case Item.ItemType.shield:
                equipment.Add(newItem);
                break;
            case Item.ItemType.sigil:
                sigils.Add(newItem);
                break;
            case Item.ItemType.consumable:
                consumables.Add(newItem);
                break;
            case Item.ItemType.keyItem:
                keyItems.Add(newItem);
                break;
        }
    }
}
