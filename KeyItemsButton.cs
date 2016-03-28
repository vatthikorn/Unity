/*
    Nathan Cruz

    NOT COMPLETED: WHOLE LOT TO DO
    NEEDS TO SHOW ONLY KEYITEMS ITEMS IN THE INVENTORY SCREEN

    Dependency:
    ButtonLocations.cs

    Required:
    Attached to a game object that is the Script holder
    The script holder object needs to be referenced in the function list held by the button
*/

using UnityEngine;
using System.Collections;

public class KeyItemsButton : ButtonLocations {

    //Places the KeyItems Button on top of the other buttons
    public void RiseToTop()
    {
        GameObject.Find(AllButtonLocation).transform.SetSiblingIndex(0);
        GameObject.Find(EquipmentButtonLocation).transform.SetSiblingIndex(1);
        GameObject.Find(SigilButtonLocation).transform.SetSiblingIndex(2);
        GameObject.Find(ConsumablesButtonLocation).transform.SetSiblingIndex(3);
        GameObject.Find(KeyItemsButtonLocation).transform.SetSiblingIndex(4);
    }
}
