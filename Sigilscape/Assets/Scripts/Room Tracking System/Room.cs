/*
    Nathan Cruz

    Keeps track of the status of the room.
    This is attached to a single room.
    The status of the room is updated every time the player enters (collides with DoorWay of this room) or exits (collides with a DoorWay of another room) the room.

    Dependency:
    RoomTracker.cs

    Required:
    Attached to a room GameObject (empy GameObject)
    The room GameObject has doorways
    The room GameObject has a parent (The Zone)

    Remember to:
    Set up the roomType beforehand
    Set up the playerIsIn and found to true for rooms the player starts in
    Set up room status from Unity
    Place one Doorway Object at every entrance of the room
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Room : MonoBehaviour {

    //undiscovered ... playerinside - self-explanatory
    //boss - the room where the boss is
    //bossBattle - when the player enters the room and the boss is not dead yet
    //defeatedInside - when the boss is dead and player is inside the room
    //defeatedOutside - when the boss is dead and player is in another room
    public enum RoomState { undiscovered, hidden, discovered, playerInside, boss, bossBattle, defeatedInside, defeatedOutside};
    
    //Initial State: undiscovered, hidden or boss (or playerInside for room the player appears in)
    public RoomState roomType;

    //undiscovered || hidden || discovered -> playerInside
    //defeatedOutside -> defeatedInside
    //boss -> bossBattle
    //Is called by a doorway when player collides with it
    public void entered()
    {
        transform.parent.GetComponent<RoomTracker>().resetAll();

        //Updates the room status accordingly
        if (roomType == RoomState.boss)
            roomType = RoomState.bossBattle;
        else if (roomType == RoomState.defeatedOutside)
            roomType = RoomState.defeatedInside;
        else if (roomType == RoomState.undiscovered || roomType == RoomState.hidden || roomType == RoomState.discovered)
            roomType = RoomState.playerInside;

        transform.parent.GetComponent<RoomTracker>().UpdateMap();
    }

    //playerInside -> discovered
    //defeatedInside -> defeatedOustide
    //hidden, undiscovered and boss stay the same
    //bossbattle should never be a situation
    //called by the Zone to reset all rooms to proper state before updating the map
    public void exited()
    {
        if (roomType == RoomState.defeatedInside)
            roomType = RoomState.defeatedOutside;
        else if (roomType == RoomState.playerInside)
            roomType = RoomState.discovered;
    }
}
