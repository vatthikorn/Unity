/*
    Nathan Cruz

    NOT YET COMPLETE. NEED TO CHANGE THE CONDITION SO THAT THE CHEST ONLY UNLOCKS WHEN THE PLAYER USES THE "THE KEY" SIGIL.

    Script for chest. When activated by player while within range, enables the item Drop.

    Dependency:
    N/A when it comes to scripts.

    Required:
    This script is attached to the gameObject intended to be used as a chest.
    The itemDrop gameObject is a child of the chest gameObject.
    The itemDatabased is referenced.
    The itemDrop is referenced.

    Remember to:
    Place the itemDrop gameObject in front of the chest and in a z-coordinate that is higher than the player's z-coordinate.
*/
using UnityEngine;
using System.Collections;

public class Chest : MonoBehaviour {

    public enum ChestState { locked, unlocked }

    public GameObject itemDatabase;
    public GameObject itemDrop;

    public ChestState chestState = ChestState.locked;
    public int itemID;
    
    void Start()
    {
        itemDrop.gameObject.SetActive(false);
    }

    //The player has to be in front the chest, to unlock the chest and release the item drop
	void OnTriggerStay2D(Collider2D other)
    {
        if(chestState == ChestState.locked && Input.GetKeyDown(KeyCode.E))
        {
            chestState = ChestState.unlocked;
            itemDrop.gameObject.SetActive(true);
        }
    }

}
