/*
    Nathan Cruz

    NEED TO IMPLEMENT HOW HEALTH POTIONS WORK
    NEED TO IMPLEMENT HOW SIGIL POTIONS WORK

    Interface:
    void LoadEquipment(Equipment savedEquipment) - used for loading the equipment from the previous game session
    Equipment SaveEquipment() - used for saving the equipment from this game session
    activateSigil1() - is activated by (PlayerController.cs) 
    activateSigil2() - is activated by (PlayerController.cs)
    activateSigil3() - is activated by (PlayerController.cs)
    activateSigil4() - is activated by (PlayerController.cs)
    UseHealthPotion() - is activated by (PlayerController.cs)
    UseSigilPotion() - is activated by (PlayerController.cs)
    everything* - items are manipulated and accessed by for damage calulation (Inventory.cs, Player.cs, PlayerAttack.cs, PlayerRangedAttack.cs, Chest.cs)

    Dependencies:
    Sigil.cs - holds the effects

    Load Order:
    Equipment.cs loads before the script that handles the loading from a save file.
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Equipment : MonoBehaviour {

    public GameObject itemDatabase;
    public GameObject inventory;
    public GameObject player;

    public const float sigilActivationTime = 0.2f;

    public Item weapon;
    public Item armor;
    public Item shield;
    public Item activeSigil1;
    public Item activeSigil2;
    public Item activeSigil3;
    public Item activeSigil4;
    public Item passiveSigil1;
    public Item passiveSigil2;
    public Item passiveSigil3;
    public Item healthPotions;
    public Item sigilPotions;

    public int healthPotionCount = 0;
    public int allHealthPotionCount = 0;
    public int sigilPotionCount = 0;

    public const int minorHealthPotionHeal = 30;
    public const int healthPotionHeal = 60;
    public const int greaterHealthPotionHeal = 100;

    public const string minorHealthPotionName = "Minor Healing Potion";
    public const string healthPotionName = "Healing Potion";
    public const string greaterHealthPotionName = "Greater Healing Potion";

    //For loading the game
    public void LoadEquipment(Equipment savedEquipment)
    {
        weapon = savedEquipment.weapon;
        armor = savedEquipment.armor;
        shield = savedEquipment.shield;
        activeSigil1 = savedEquipment.activeSigil1;
        activeSigil2 = savedEquipment.activeSigil2;
        activeSigil3 = savedEquipment.activeSigil3;
        activeSigil4 = savedEquipment.activeSigil4;
        passiveSigil1 = savedEquipment.passiveSigil1;
        passiveSigil2 = savedEquipment.passiveSigil2;
        passiveSigil3 = savedEquipment.passiveSigil3;
        healthPotions = savedEquipment.healthPotions;
        sigilPotions = savedEquipment.sigilPotions;
    }

    //For saving the game
    public Equipment SaveEquipment()
    {
        return this;
    }

    //TEST VALUES
    void Start()
    {
        weapon = itemDatabase.GetComponent<ItemDatabase>().items[0];
        shield = itemDatabase.GetComponent<ItemDatabase>().items[18];
        armor = itemDatabase.GetComponent<ItemDatabase>().items[15];
        activeSigil1 = itemDatabase.GetComponent<ItemDatabase>().items[16];
        healthPotions = itemDatabase.GetComponent<ItemDatabase>().items[19];
        activeSigil2 = itemDatabase.GetComponent<ItemDatabase>().items[20];
        activeSigil3 = activeSigil4 = passiveSigil1 = passiveSigil2 = passiveSigil3 = sigilPotions = new Item();
        inventory.GetComponent<Inventory>().AddItemFromDrop(19);
        inventory.GetComponent<Inventory>().AddItemFromDrop(19);
        inventory.GetComponent<Inventory>().AddItemFromDrop(19);
        inventory.GetComponent<Inventory>().AddItemFromDrop(21);
        inventory.GetComponent<Inventory>().AddItemFromDrop(19);
        inventory.GetComponent<Inventory>().AddItemFromDrop(19);
        inventory.GetComponent<Inventory>().AddItemFromDrop(21);
        inventory.GetComponent<Inventory>().AddItemFromDrop(22);
        inventory.GetComponent<Inventory>().AddItemFromDrop(21);
        inventory.GetComponent<Inventory>().AddItemFromDrop(19);
        inventory.GetComponent<Inventory>().AddItemFromDrop(19);
        inventory.GetComponent<Inventory>().AddItemFromDrop(22);
        inventory.GetComponent<Inventory>().AddItemFromDrop(21);
        inventory.GetComponent<Inventory>().AddItemFromDrop(21);
        inventory.GetComponent<Inventory>().AddItemFromDrop(19);
        inventory.GetComponent<Inventory>().AddItemFromDrop(23);
        inventory.GetComponent<Inventory>().AddItemFromDrop(23);
        inventory.GetComponent<Inventory>().AddItemFromDrop(23);
        inventory.GetComponent<Inventory>().AddItemFromDrop(23);
        UpdateItemCount();
    }

    //Called by Player.cs
    public Item GetWeapon()
    {
        if (weapon.itemName != null)
            return weapon;
        //Thye punch if they got no weapon
        else
        {
            return new Item(-1, null, null, Item.ItemType.weapon, 1, 0, Item.AttackSpeed.fastest, Item.Range.smallest, Item.Knockback.smallest, Item.WeaponType.melee);
        }
    }

    //Called by PlayerController.cs
    public void activateSigil1()
    {
        //Activates when sigil is equipped and enabled
        if(activeSigil1.itemName != null && activeSigil1.sigil.GetComponent<Sigil>().enabledSigil)
        {
            player.GetComponent<PlayerController>().action = false;
            activeSigil1.sigil.GetComponent<Sigil>().activated();
            Invoke("Enable", sigilActivationTime);
        }
    }

    //Called by PlayerController.cs
    public void activateSigil2()
    {
        //Activates when sigil is equipped and enabled
        if (activeSigil2.itemName != null && activeSigil2.sigil.GetComponent<Sigil>().enabledSigil)
        {
            player.GetComponent<PlayerController>().action = false;
            activeSigil2.sigil.GetComponent<Sigil>().activated();
            Invoke("Enable", sigilActivationTime);
        }
    }

    //Called by PlayerController.cs
    public void activateSigil3()
    {
        //Activates when sigil is equipped and enabled
        if (activeSigil3.itemName != null && activeSigil3.sigil.GetComponent<Sigil>().enabledSigil)
        {
            player.GetComponent<PlayerController>().action = false;
            activeSigil3.sigil.GetComponent<Sigil>().activated();
            Invoke("Enable", sigilActivationTime);
        }
    }

    //Called by PlayerController.cs
    public void activateSigil4()
    {
        //Activates when sigil is equipped and enabled
        if (activeSigil4.itemName != null && activeSigil4.sigil.GetComponent<Sigil>().enabledSigil)
        {
            player.GetComponent<PlayerController>().action = false;
            activeSigil4.sigil.GetComponent<Sigil>().activated();
            Invoke("Enable", sigilActivationTime);
        }
    }

    void Enable()
    {
        player.GetComponent<PlayerController>().action = true;
    }

    public void UpdateItemCount()
    {
        if(healthPotions.itemName != null)
        {
            if (healthPotions.itemName == minorHealthPotionName)
            {
                healthPotionCount = inventory.GetComponent<Inventory>().CountMinorHealingPotions() + 1;
                allHealthPotionCount = inventory.GetComponent<Inventory>().CountMinorHealingPotions() + inventory.GetComponent<Inventory>().CountHealingPotions() + inventory.GetComponent<Inventory>().CountGreaterHealingPotions() + 1;
            }
            else if (healthPotions.itemName == healthPotionName)
            {
                healthPotionCount = inventory.GetComponent<Inventory>().CountHealingPotions() + 1;
                allHealthPotionCount = inventory.GetComponent<Inventory>().CountMinorHealingPotions() + inventory.GetComponent<Inventory>().CountHealingPotions() + inventory.GetComponent<Inventory>().CountGreaterHealingPotions() + 1;
            }
            else if(healthPotions.itemName == greaterHealthPotionName)
            {
                healthPotionCount = inventory.GetComponent<Inventory>().CountGreaterHealingPotions() + 1;
                allHealthPotionCount = inventory.GetComponent<Inventory>().CountMinorHealingPotions() + inventory.GetComponent<Inventory>().CountHealingPotions() + inventory.GetComponent<Inventory>().CountGreaterHealingPotions() + 1;
            }
        }
        else
        {
            allHealthPotionCount = inventory.GetComponent<Inventory>().CountMinorHealingPotions() + inventory.GetComponent<Inventory>().CountHealingPotions() + inventory.GetComponent<Inventory>().CountGreaterHealingPotions();
        }

        if(sigilPotions.itemName != null)
            sigilPotionCount = inventory.GetComponent<Inventory>().CountSigilPotions() + 1;
    }

    //Called by PlayerController.cs
    public void UseHealthPotion()
    {
        //Check there is a potion equipped, then check which kind, heal, see if there are any other then remove either from inventory or here
        if(healthPotions.itemName != null)
        {
            if(healthPotions.itemName == minorHealthPotionName)
            {
                player.GetComponent<Player>().Heal(minorHealthPotionHeal);
                if(inventory.GetComponent<Inventory>().Find(healthPotions.itemID))
                {
                    inventory.GetComponent<Inventory>().OthersRemoveItem(healthPotions.itemID);
                    healthPotionCount = inventory.GetComponent<Inventory>().CountMinorHealingPotions() + 1;
                    allHealthPotionCount = inventory.GetComponent<Inventory>().CountMinorHealingPotions() + inventory.GetComponent<Inventory>().CountHealingPotions() + inventory.GetComponent<Inventory>().CountGreaterHealingPotions() + 1;
                }
                else
                {
                    healthPotions = new Item();
                    healthPotionCount = healthPotions.itemName != null ? healthPotionCount = inventory.GetComponent<Inventory>().CountMinorHealingPotions() : 0;
                    allHealthPotionCount = inventory.GetComponent<Inventory>().CountMinorHealingPotions() + inventory.GetComponent<Inventory>().CountHealingPotions() + inventory.GetComponent<Inventory>().CountGreaterHealingPotions();
                }
            }
            else if (healthPotions.itemName == healthPotionName)
            {
                player.GetComponent<Player>().Heal(healthPotionHeal);
                if (inventory.GetComponent<Inventory>().Find(healthPotions.itemID))
                {
                    inventory.GetComponent<Inventory>().OthersRemoveItem(healthPotions.itemID);
                    healthPotionCount = inventory.GetComponent<Inventory>().CountHealingPotions() + 1;
                    allHealthPotionCount = inventory.GetComponent<Inventory>().CountMinorHealingPotions() + inventory.GetComponent<Inventory>().CountHealingPotions() + inventory.GetComponent<Inventory>().CountGreaterHealingPotions() + 1;
                }
                else
                {
                    healthPotions = new Item();
                    healthPotionCount = healthPotions.itemName != null ? healthPotionCount = inventory.GetComponent<Inventory>().CountHealingPotions() : 0;
                    allHealthPotionCount = inventory.GetComponent<Inventory>().CountMinorHealingPotions() + inventory.GetComponent<Inventory>().CountHealingPotions() + inventory.GetComponent<Inventory>().CountGreaterHealingPotions();
                }
            }
            else if (healthPotions.itemName == greaterHealthPotionName)
            {
                player.GetComponent<Player>().Heal(greaterHealthPotionHeal);
                if (inventory.GetComponent<Inventory>().Find(healthPotions.itemID))
                {
                    inventory.GetComponent<Inventory>().OthersRemoveItem(healthPotions.itemID);
                    healthPotionCount = inventory.GetComponent<Inventory>().CountGreaterHealingPotions() + 1;
                    allHealthPotionCount = inventory.GetComponent<Inventory>().CountMinorHealingPotions() + inventory.GetComponent<Inventory>().CountHealingPotions() + inventory.GetComponent<Inventory>().CountGreaterHealingPotions() + 1;
                }
                else
                {
                    healthPotions = new Item();
                    healthPotionCount = healthPotions.itemName != null ? healthPotionCount = inventory.GetComponent<Inventory>().CountGreaterHealingPotions() : 0;
                    allHealthPotionCount = inventory.GetComponent<Inventory>().CountMinorHealingPotions() + inventory.GetComponent<Inventory>().CountHealingPotions() + inventory.GetComponent<Inventory>().CountGreaterHealingPotions();
                }
            }
        }
    }

    //Called by PlayerController.cs
    public void UseSigilPotion()
    {
        if(sigilPotions.itemName != null)
        {
            if(activeSigil1.itemName != null)
            {
                activeSigil1.sigil.GetComponent<Sigil>().timer = 0;
            }

            if (activeSigil2.itemName != null)
            {
                activeSigil2.sigil.GetComponent<Sigil>().timer = 0;
            }

            if (activeSigil3.itemName != null)
            {
                activeSigil3.sigil.GetComponent<Sigil>().timer = 0;
            }

            if (activeSigil4.itemName != null)
            {
                activeSigil4.sigil.GetComponent<Sigil>().timer = 0;
            }

            if (inventory.GetComponent<Inventory>().Find(sigilPotions.itemID))
            {
                inventory.GetComponent<Inventory>().OthersRemoveItem(sigilPotions.itemID);
                sigilPotionCount = inventory.GetComponent<Inventory>().CountSigilPotions() + 1;
            }
            else
            {
                sigilPotions = new Item();
                sigilPotionCount = 0;
            }
        }
    }
}
