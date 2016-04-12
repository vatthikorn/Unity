/*
    Nathan Cruz

    Keeps track of the status of all of the rooms in the zone.
    This is attached to the entire zone.
    Sends information to the mapManager and signals it to update evertime the player collides with a DoorWay.

    Interface:
    void UpdateMap() - updates room list status (Room.cs)
    void ResetAll() -  resets all rooms (Room.cs)

    Dependency:
    Room.cs - get room status (roomType, exited())
    MapMangager.cs (2) - (UpdateMapImage(), SortMaps())

    Required:
    Attached to a zone GameObject (empty)
    The list references all of the room GameObjects
    The miniMapManager GameObject is referenced
    The largeMapManager GameObject is referenced
    (Both mapManagers should be child of their corresponding canvases (canvas for miniMap, canvas for largeMap))
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomTracker : MonoBehaviour {
    
    //Needs prior setup
    public GameObject miniMapManager;
    public GameObject largeMapManager;
    public List<GameObject> rooms = new List<GameObject>();

    public List<Room.RoomState> roomStatus = new List<Room.RoomState>();

    //Intializes all the rooms status in list and sets up the mapManager for display
    void Start()
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            roomStatus.Add(rooms[i].GetComponent<Room>().roomType);
        }

        miniMapManager.GetComponent<MapManager>().SortMaps();
        largeMapManager.GetComponent<MapManager>().SortMaps();
        miniMapManager.GetComponent<MapManager>().UpdateMapImage(roomStatus);
        largeMapManager.GetComponent<MapManager>().UpdateMapImage(roomStatus);
    }

    //Called by room whenever player changes room, updates the status of all rooms in the list, updates the miniMap and largeMap
    public void UpdateMap()
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            roomStatus[i] = rooms[i].GetComponent<Room>().roomType;
        }

        miniMapManager.GetComponent<MapManager>().UpdateMapImage(roomStatus);
        largeMapManager.GetComponent<MapManager>().UpdateMapImage(roomStatus);
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
