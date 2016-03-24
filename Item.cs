/*
    This is to keep information on an item in the game for the purposes of inventory and equipment.
    This is NOT to for items dropped in game or released from chests. (This is for a separate system)

    Required:
    Set values to 0 when appropriate.
*/
using UnityEngine;
using System.Collections;

[System.Serializable]
public class Item {

    public enum ItemType { weapon, shield, armor, sigil, consumable, keyItem };

    public int itemID;
    public string itemName;
    public string itemDesc;
    public ItemType itemType;
    public Texture2D icon;

    //Weapons only, otherwise set to 0
    public int strength;
    public float criticalChance;

    //Shields only, otherwise set to 0
    public float damageMitigation;

    //Armor only, otherwise set to 0
    public int defense;

    //Sigil only
    public GameObject sigil;//the gameObject will be attached with a Sigil script that contains all information and behavior.
}
