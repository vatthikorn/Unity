using UnityEngine;
using System.Collections;

public class SigilButton : ButtonLocations {

    public void RiseToTop()
    {
        GameObject.Find(AllButtonLocation).transform.SetSiblingIndex(2);
        GameObject.Find(EquipmentButtonLocation).transform.SetSiblingIndex(3);
        GameObject.Find(SigilButtonLocation).transform.SetSiblingIndex(4);
        GameObject.Find(ConsumablesButtonLocation).transform.SetSiblingIndex(1);
        GameObject.Find(KeyItemsButtonLocation).transform.SetSiblingIndex(0);
    }
}
