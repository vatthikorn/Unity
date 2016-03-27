using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Equipment : MonoBehaviour {

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
    public int[] healthPotions = new int[3];
    public int sigilPotions;

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
