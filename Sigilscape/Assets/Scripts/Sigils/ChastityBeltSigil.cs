/*
    Nathan Cruz

    Controls the Chastity Belt sigil.
    Reduced damage.
*/
using UnityEngine;
using System.Collections;

public class ChastityBeltSigil : Sigil {
    
    public GameObject equipment;

    //name of sigil in game
    public string sigilName = "Chastity Belt";
    static public bool isEquipped = false;
    static public float damageMitigation = 0.05f;

    //Sets up type
    void Start()
    {
        sigilType = SigilType.passive;
    }

    //Activates effect when equipped
    void Update()
    {
        isEquipped = IsEquipped();
    }

    override public void Effect()
    {
        //Does nothing.
    }

    static public int ReduceDamage(int damage)
    {
        if(isEquipped)
        {
            return (int)(damage * (1 - damageMitigation));
        }

        return damage;
    }

    //Checks if it is equipped
    bool IsEquipped()
    {
        return (equipment.GetComponent<Equipment>().passiveSigil1.itemName != null && equipment.GetComponent<Equipment>().passiveSigil1.itemName == sigilName) ||
            (equipment.GetComponent<Equipment>().passiveSigil2.itemName != null && equipment.GetComponent<Equipment>().passiveSigil2.itemName == sigilName) ||
            (equipment.GetComponent<Equipment>().passiveSigil3.itemName != null && equipment.GetComponent<Equipment>().passiveSigil3.itemName == sigilName);
    }
}
