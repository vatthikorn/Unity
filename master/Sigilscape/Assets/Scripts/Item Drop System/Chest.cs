/*
    Nathan Cruz

    Script for chest. When activated by player with The Key while within range, enables the item Drop.

    Dependency:
    Equipment.cs - check the player is opening it using The Key (activeSigils*)

    Required:
    This script is attached to the gameObject intended to be used as a chest.
    The itemDrop gameObject is a child of the chest gameObject.
    The itemDatabased is referenced.
    The itemDrop is referenced.

    Remember to:
    Place the itemDrop gameObject in front of the chest and in a z-coordinate that is higher than the player's z-coordinate.
    Disable the itemDrop on start up.
*/
using UnityEngine;
using System.Collections;

public class Chest : MonoBehaviour {

    public enum ChestState { locked, unlocked }

    //Must be referenced beforehand
    public GameObject itemDrop;
    public GameObject equipment;

    public const string theKeyName = "The Key";

    public ChestState chestState = ChestState.locked;
    
    //Disables the item to be dropped from the chest.
    void Start()
    {
        itemDrop.gameObject.SetActive(false);
    }

    //The player has to be in front the chest, to unlock the chest and release the item drop
	void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == ("Player") && chestState == ChestState.locked)
        {
            if(equipment.GetComponent<Equipment>().activeSigil1.itemName != null && equipment.GetComponent<Equipment>().activeSigil1.itemName == theKeyName && (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)))
            {
                OpenChest();
            }

            if (equipment.GetComponent<Equipment>().activeSigil2.itemName != null && equipment.GetComponent<Equipment>().activeSigil2.itemName == theKeyName && (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2)))
            {
                OpenChest();
            }

            if (equipment.GetComponent<Equipment>().activeSigil3.itemName != null && equipment.GetComponent<Equipment>().activeSigil3.itemName == theKeyName && (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3)))
            {
                OpenChest();
            }

            if (equipment.GetComponent<Equipment>().activeSigil4.itemName != null && equipment.GetComponent<Equipment>().activeSigil4.itemName == theKeyName && (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4)))
            {
                OpenChest();
            }
        }
    }

    void OpenChest()
    {
        chestState = ChestState.unlocked;
        itemDrop.gameObject.SetActive(true);
    }

}
