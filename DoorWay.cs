/*
    Nathan Cruz

    Signals to the Room class that the player has collided with it.
    This is attached to an entrance to the room.
    There should be multiple in most rooms.

    Dependency:
    Room.cs - (entered())

    Required:
    Attached to a cube gameObject (w/o material & w 2D Collider & trigger on)
    gameObject has a parent (The Room)
    The parent has a "Room" Script component
    The player gameObject has the "Player" tag

*/
using UnityEngine;
using System.Collections;

public class DoorWay : MonoBehaviour {

    //Signals to the room it has been entered, calls its entered()
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            transform.parent.gameObject.GetComponent<Room>().entered();
        }
    }
}
