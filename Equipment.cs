/*
    Nathan Cruz

    NOT COMPLETE. BARELY STARTED.
    THE IDEA IS TO HAVE THIS BE REFERENCED IN THE COMBAT SYSTEM (SPECIFICALLY THE COLLISIONS, THE BLOCKS ATTACHED TO THE PLAYER ACTIVATED, DAMAGE, DEFENSE, ETC)
    CURRENTLY JUST A LIST OF SHIT THE PLAYER CAN HOLD AT A TIME.

    Interface:
    activateSigil1() - is activated by (PlayerController.cs) 
    activateSigil2() - is activated by (PlayerController.cs)
    activateSigil3() - is activated by (PlayerController.cs)
    activateSigil4() - is activated by (PlayerController.cs)
    UseHealthPotion() - is activated by (PlayerController.cs)
    UseSigilPotion() - is activated by (PlayerController.cs)
    everything* - items are manipulated and accessed by for damage calulation (Inventory.cs, Player.cs, PlayerAttack.cs, PlayerRangedAttack.cs, Chest.cs)

    Dependencies:
    Sigil.cs - holds the effects
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Equipment : MonoBehaviour {

    public GameObject itemDatabase;

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

    //TEST VALUES
    void Start()
    {
        weapon = itemDatabase.GetComponent<ItemDatabase>().items[0];
        shield = itemDatabase.GetComponent<ItemDatabase>().items[18];
        armor = itemDatabase.GetComponent<ItemDatabase>().items[15];
        activeSigil1 = itemDatabase.GetComponent<ItemDatabase>().items[16];
        healthPotions = itemDatabase.GetComponent<ItemDatabase>().items[19];
        activeSigil2 = activeSigil3 = activeSigil4 = passiveSigil1 = passiveSigil2 = passiveSigil3 = sigilPotions = new Item();
    }

    public void activateSigil1()
    {
        if(activeSigil1.itemName != null)
        {
            Debug.Log("This is where the function call for the sigil effect is supposed to go!");
        }
    }

    public void activateSigil2()
    {
        if (activeSigil2.itemName != null)
        {
            Debug.Log("This is where the function call for the sigil effect is supposed to go!");
        }
    }

    public void activateSigil3()
    {
        if (activeSigil3.itemName != null)
        {
            Debug.Log("This is where the function call for the sigil effect is supposed to go!");
        }
    }

    public void activateSigil4()
    {
        if (activeSigil4.itemName != null)
        {
            Debug.Log("This is where the function call for the sigil effect is supposed to go!");
        }
    }
    
    public void UseHealthPotion()
    {
        if(healthPotions.itemName != null)
        {
            Debug.Log("This is where the function call to use the health potion is supposed to go!");
        }
    }

    public void UseSigilPotion()
    {
        if(sigilPotions.itemName != null)
        {
            Debug.Log("This is where the function call to use the sigil potion is supposed to go!");
        }
    }
}
