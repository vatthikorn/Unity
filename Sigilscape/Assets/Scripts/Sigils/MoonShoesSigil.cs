/*
    /*
    Nathan Cruz

    Controls the Moon Shoes sigil.
    Boosts jump height
*/
using UnityEngine;
using System.Collections;

public class MoonShoesSigil : Sigil {

    public GameObject equipment;

    public string sigilName = "Moon shoes";
    static public bool isEquipped = false;

    //Sets up type
    void Start()
    {
        sigilType = SigilType.passive;
    }

    //Activates effect when equipped
    void Update()
    {
        isEquipped = IsEquipped();

        if (isEquipped)
        {
            //DO SOMETHING
        }
    }

    override public void Effect()
    {
        //Does nothing.
    }

    //Checks if it is equipped
    bool IsEquipped()
    {
        return (equipment.GetComponent<Equipment>().passiveSigil1.itemName != null && equipment.GetComponent<Equipment>().passiveSigil1.itemName == sigilName) ||
            (equipment.GetComponent<Equipment>().passiveSigil2.itemName != null && equipment.GetComponent<Equipment>().passiveSigil2.itemName == sigilName) ||
            (equipment.GetComponent<Equipment>().passiveSigil3.itemName != null && equipment.GetComponent<Equipment>().passiveSigil3.itemName == sigilName);
    }
}
