/*
    Keeps track of the status of all of the rooms in the zone.
    This is attached to the entire zone.

    Required:
    Attached to a zone GameObject
    The list references all of the room GameObjects
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomTracker : MonoBehaviour {

    public List<GameObject> rooms = new List<GameObject>();
    public List<Room.RoomState> roomStatus = new List<Room.RoomState>();

    //Intializes all the rooms status in list
    void Start()
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            roomStatus.Add(rooms[i].GetComponent<Room>().roomType);
        }
    }

    //Called by room whenever player changes room, updates the status of all rooms in the list
    public void UpdateMap()
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            roomStatus[i] = rooms[i].GetComponent<Room>().roomType;
        }
    }

    //Called by room whenever player changes room, updates the status of all rooms
    public void resetAll()
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            rooms[i].gameObject.GetComponent<Room>().exited();
        }
    }
}
