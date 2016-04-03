/*
    Nathan Cruz

    NOT COMPLETE. BARELY STARTED.
    THE IDEA IS TO HAVE THIS BE REFERENCED IN THE COMBAT SYSTEM (SPECIFICALLY THE COLLISIONS, THE BLOCKS ATTACHED TO THE PLAYER ACTIVATED, DAMAGE, DEFENSE, ETC)
    CURRENTLY JUST A LIST OF SHIT THE PLAYER CAN HOLD AT A TIME.
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
    public int healthPotions;
    public int sigilPotions;

    void Start()
    {
        weapon = itemDatabase.GetComponent<ItemDatabase>().items[6];
        shield = itemDatabase.GetComponent<ItemDatabase>().items[18];
        armor = itemDatabase.GetComponent<ItemDatabase>().items[15];
    }

    public void activateSigil1()
    {
        if(activeSigil1 != null)
        {
            
        }
    }

    public void activateSigil2()
    {
        if (activeSigil2 != null)
        {

        }
    }

    public void activateSigil3()
    {
        if (activeSigil3 != null)
        {

        }
    }

    public void activateSigil4()
    {
        if (activeSigil4 != null)
        {

        }
    }
    
    public void UseHealthPotion()
    {

    }

    public void UseSigilPotion()
    {

    }
}
