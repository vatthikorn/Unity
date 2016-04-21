/*
    Nathan Cruz

    This is to keep information on an item in the game.
    This is NOT to for items dropped in game or released from chests. (Refer to ItemDrop.cs)
    ItemDatabase.cs uses this.

    Interface:
    everything here, everything needs everything here

    Required:
    Set values to 0 when appropriate.
*/
using UnityEngine;
using System.Collections;

[System.Serializable]
public class Item
{
    public enum ItemType
    {
        weapon,
        shield,
        armor,
        sigil,
        consumable,
        keyItem
    };

    public enum AttackSpeed
    {
        none,
        slowest,
        slow,
        medium,
        fast,
        fastest
    };

    public enum Range
    {
        None,
        Smallest,
        Small,
        Medium,
        Large,
        Largest,
        Longs,
        Longest
    };

    public enum Knockback
    {
        None,
        Smallest,
        Smaller,
        Small,
        Medium,
        Large,
        Largest
    };

    public enum WeaponType
    {
        None,
        Melee,
        Range
    };

    [HideInInspector] public int itemID;
    [HideInInspector] public string itemName;
    [HideInInspector] public string itemDesc;
    [HideInInspector] public ItemType itemType;
    public Texture2D icon;

    //Weapons only, otherwise set to 0
    [HideInInspector] public int damage;
    [HideInInspector] public float criticalChance;
    [HideInInspector] public AttackSpeed attackSpeed;
    [HideInInspector] public Range range;
    [HideInInspector] public Knockback knockback;
    [HideInInspector] public WeaponType weaponType;

    //Shields only, otherwise set to 0
    [HideInInspector] public float damageMitigation;

    //Armor only, otherwise set to 0
    [HideInInspector] public int defense;

    //Sigil only
    public GameObject sigil;//the gameObject will be attached with a Sigil script that contains all information and behavior.
    
    //Null Constructor
    public Item()
    {

    }

    //Called by Equipment.cs, for when the player is using their fist
    public Item(int ID, string name, string desc, ItemType itemType, int dmg, float crtChnce, AttackSpeed attspd, Range range, Knockback knockback, WeaponType weaponType)
    {
        addItemInfo(ID, name, desc, itemType);  
        this.damage = dmg;
        this.criticalChance = crtChnce;
        this.attackSpeed = attspd;
        this.range = range;
        this.knockback = knockback;
        this.weaponType = weaponType;
        this.damageMitigation = 0;
        this.defense = 0;
        this.sigil = null;
    }

    //Item initialization for Weapons
    public void addItemInfo(int ID, string name, string desc, ItemType itemType, int dmg, float crtChnce, AttackSpeed attspd, Range range, Knockback knockback, WeaponType weaponType)
    {
        addItemInfo(ID, name, desc, itemType);
        this.damage = dmg;
        this.criticalChance = crtChnce;
        this.attackSpeed = attspd;
        this.range = range;
        this.knockback = knockback;
        this.weaponType = weaponType;
        this.damageMitigation = 0;
        this.defense = 0;
        this.sigil = null;
    }

    //Item initialization for Shields
    public void addItemInfo(int ID, string name, string desc, ItemType itemType, float dmgMitigation)
    {
        addItemInfo(ID, name, desc, itemType);
        this.damage = 0;
        this.criticalChance = 0;
        this.attackSpeed = 0;
        this.range = 0;
        this.knockback = 0;
        this.weaponType = WeaponType.None;
        this.damageMitigation = dmgMitigation;
        this.defense = 0;
        this.sigil = null;
    }

    //Item initialization for Armor
    public void addItemInfo(int ID, string name, string desc, ItemType itemType, int defense)
    {
        addItemInfo(ID, name, desc, itemType);
        this.damage = 0;
        this.criticalChance = 0;
        this.attackSpeed = 0;
        this.range = 0;
        this.knockback = 0;
        this.weaponType = WeaponType.None;
        this.damageMitigation = 0;
        this.defense = defense;
        this.sigil = null;
    }

    //Item initialization for Sigil, Consumable and KeyItem
    public void addItemInfo(int ID, string name, string desc, ItemType itemType)
    {
        this.itemID = ID;
        this.itemName = name;
        this.itemDesc = desc;
        this.itemType = itemType;
        this.damage = 0;            
        this.criticalChance = 0;
        this.attackSpeed = 0;
        this.range = 0;
        this.knockback = 0;
        this.weaponType = WeaponType.None;
        this.damageMitigation = 0;
        this.defense = 0;
    }

    public System.String toString()
    {
        switch (this.itemType)
        {
            case ItemType.weapon:
                {
                    return this.range.ToString() + this.weaponType.ToString();
                }
            default:
                {
                    return this.itemName;
                }
        }
    }

    public float getAttackSpeed()
    {
        switch (this.itemType)
        {
            case ItemType.weapon:
                {
                    switch (this.attackSpeed)
                    {
                        case AttackSpeed.slowest:
                            {
                                return 0.35f;
                            }
                        case AttackSpeed.slow:
                            {
                                return 0.27f;
                            }
                        case AttackSpeed.medium:
                            {
                                return 0.20f;
                            }
                        case AttackSpeed.fast:
                            {
                                return 0.12f;
                            }
                        case AttackSpeed.fastest:
                            {
                                return 0.05f;
                            }
                        default:
                            {
                                return 0;
                            }
                    };
                }
            default:
                {
                    return 0;
                }
        }
    }
}
