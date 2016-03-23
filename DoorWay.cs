/*
    Signals to the Room class that the player has collided with it.
    This is attached to an entrance to the room.
    There should be multiple in most rooms.

    Required:
    Attached to cube gameObject (w/o material)
    gameObject has isTrigger set
    gameObject has a parent (The Room)
    The parent has a "Room" Script component
    The player gameObject has the "Player" tag

*/
using UnityEngine;
using System.Collections;

public class DoorWay : MonoBehaviour {

    //Signals to the room it has been entered, calls its entered()
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            transform.parent.gameObject.GetComponent<Room>().entered();
        }
    }
}
