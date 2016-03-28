/*
    Nathan Cruz

    This is to keep information on an item in the game for the purposes of inventory and equipment.
    This is NOT to for items dropped in game or released from chests. (This is for the Item Drop system (Chest.cs and ItemDrop.cs))
    ItemDatabase.cs uses this.

    Required:
    Set values to 0 when appropriate.
*/
using UnityEngine;
using System.Collections;

[System.Serializable]
public class Item {

    public enum ItemType { weapon, shield, armor, sigil, consumable, keyItem };
    public enum AttackSpeed { none, slowest, slow, medium, fast, fastest };
    public enum Range { none, smallest, small, medium, large, largest, longs, longest };
    public enum Knockback { none, smallest, smaller, small, medium, large, largest };
    public enum WeaponType { none, melee, range };

    public int itemID;
    public string itemName;
    public string itemDesc;
    public ItemType itemType;
    public Texture2D icon;

    //Weapons only, otherwise set to 0
    public int damage;
    public float criticalChance;
    public AttackSpeed attackSpeed;
    public Range range;
    public Knockback knockback;
    public WeaponType weaponType;

    //Shields only, otherwise set to 0
    public float damageMitigation;

    //Armor only, otherwise set to 0
    public int defense;

    //Sigil only
    public GameObject sigil;//the gameObject will be attached with a Sigil script that contains all information and behavior.
}
