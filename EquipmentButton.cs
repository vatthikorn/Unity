/*
    Nathan Cruz

    NOT COMPLETED: WHOLE LOT TO DO
    NEEDS TO SHOW ONLY EQUIPMENT ITEMS IN THE INVENTORY SCREEN

    Dependency:
    ButtonLocations.cs

    Required:
    Attached to a game object that is the Script holder
    The script holder object needs to be referenced in the function list held by the button
*/

using UnityEngine;
using System.Collections;

public class EquipmentButton : ButtonLocations {

    //Places the Equipment Button on top of the other buttons
    public void RiseToTop()
    {
        GameObject.Find(AllButtonLocation).transform.SetSiblingIndex(3);
        GameObject.Find(EquipmentButtonLocation).transform.SetSiblingIndex(4);
        GameObject.Find(SigilButtonLocation).transform.SetSiblingIndex(2);
        GameObject.Find(ConsumablesButtonLocation).transform.SetSiblingIndex(1);
        GameObject.Find(KeyItemsButtonLocation).transform.SetSiblingIndex(0);
    }
}
