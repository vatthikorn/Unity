/*
    Nathan Cruz

    Manages what the map displays.
    initialMap is the what the map will look like if all rooms were "undiscovered" or "hidden"
    playerHere is collection of images that will overlay on top of the initialMap. Only one is enabled indicating the room the player is in.
    roomsDiscovered is collection of images that will overlay on top of the initialMap. Many can be enabled.
    boss is collection of images that will overlay on top of the initialMap. Many can be enabled.
    bossDeafeted is collection of images that will overlay on top of the initialMap. Many can be enabled.
    Only the initialMap should be a complete image, all other images will just be pieces to lie on top of it.
    Layer Order (top to bottom): bossDefeated, boss, playerHere, roomDiscovered, initialMap

    Interface:
    SortMaps(), UpdateMapImage() - layers map and updates image as player moves (RoomTracker.cs)

    Required:
    DO NOT MANIPULATE ANY OF THE INTEGER VALUES, SCRIPT WILL HANDLE THESES
    Set the size of all lists of images to the number of rooms the zone has.
    Every slot of playerHere list and roomsDiscovered list should contain a reference to the image.
    Only the slots where the boss will be should have a boss and bossDeafeted image. (Null cases are handled) (Error can still occur if the status of the room indicates there should be a boss and there is no image for that)
    This script is attached to a mapManager object (which is a child to the canvas (map itself))
    All images are also child to the same canvas
    All images should be formatted accordingly with anchors in Unity. (miniMap anchor min(.85, .05) max(.95, .15)) (largeMap anchor min(.2, .2) max(.8, .8)) (This can be up for discussion)

    Rememeber to:
    Attach a separate MapManager.cs to a mapManager (one for miniMap, one for largeMap)
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MapManager : MonoBehaviour {

    //All references should be set (Read Required)
    //All lists should be the same size
    public Image initialMap;
    public List<Image> playerHere;
    public List<Image> roomsDiscovered;
    public List<Image> boss;
    public List<Image> bossDefeated;

    //DO NOT TOUCH THESE VALUES IN UNITY (SCRIPT WILL HANDLE THESE)
    public int playerHereNum;
    public List<int> roomsDiscoveredNum = new List<int>();
    public List<int> bossNum;
    public List<int> bossDefeatedNum;
    
    //Called by RoomTracker.cs to update the map
	public void UpdateMapImage(List<Room.RoomState> roomStatus)
    {
        CheckStatus(roomStatus);
        PlaceImages();
    }

    //Called by RoomTracker.cs to set the images in the correct order for display
    public void SortMaps()
    {
        int index = 2;
        this.transform.SetAsFirstSibling();
        initialMap.transform.SetSiblingIndex(1);
        for(int i = 0; i < roomsDiscovered.Count; i++)
        {
            roomsDiscovered[i].transform.SetSiblingIndex(index++);
        }
        for (int i = 0; i < playerHere.Count; i++)
        {
            playerHere[i].transform.SetSiblingIndex(index++);
        }
        for (int i = 0; i < boss.Count; i++)
        {
            if(boss[i] != null)
                boss[i].transform.SetSiblingIndex(index++);
        }
        for (int i = 0; i < bossDefeated.Count; i++)
        {
            if (bossDefeated[i] != null)
                bossDefeated[i].transform.SetSiblingIndex(index++);
        }
    }

    //Updates the statuses of the room and updates lists
    void CheckStatus(List<Room.RoomState> roomStatus)
    {
        roomsDiscoveredNum.Clear();
        bossNum.Clear();
        bossDefeatedNum.Clear();

        for (int i = 0; i < roomStatus.Count; i++)
        {
            switch (roomStatus[i])
            {
                case Room.RoomState.playerInside:
                    playerHereNum = i;
                    break;
                case Room.RoomState.discovered:
                    roomsDiscoveredNum.Add(i);
                    break;
                case Room.RoomState.boss:
                    bossNum.Add(i);
                    break;
                case Room.RoomState.bossBattle:
                    bossNum.Add(i);
                    playerHereNum = i;
                    break;
                case Room.RoomState.defeatedOutside:
                    bossDefeatedNum.Add(i);
                    roomsDiscoveredNum.Add(i);
                    break;
                case Room.RoomState.defeatedInside:
                    bossDefeatedNum.Add(i);
                    playerHereNum = i;
                    break;
                default:
                    break;
            }
        }
    }

    //Resets and then sets the map
    void PlaceImages()
    {
        ClearImagesOnMap();
        PlaceImagesOnMap();
    }

    //Removes all images but the initialMap
    void ClearImagesOnMap()
    {
        for(int i = 0; i < playerHere.Count; i++)
        {
            playerHere[i].enabled = false;
        }

        for (int i = 0; i < roomsDiscovered.Count; i++)
        {
            roomsDiscovered[i].enabled = false;
        }

        for (int i = 0; i < boss.Count; i++)
        {
            if(boss[i] != null)
            {
                boss[i].enabled = false;
            }
        }

        for(int i = 0; i < bossDefeated.Count; i++)
        {
            if (bossDefeated[i] != null)
            {
                bossDefeated[i].enabled = false;
            }
        }
    }

    //Enables the images according to room status
    void PlaceImagesOnMap()
    {
        playerHere[playerHereNum].enabled = true;
        for(int i = 0; i < roomsDiscoveredNum.Count; i++)
        {
            roomsDiscovered[roomsDiscoveredNum[i]].enabled = true;
        }
        for (int i = 0; i < bossNum.Count; i++)
        {
            boss[bossNum[i]].enabled = true;
        }
        for (int i = 0; i < bossDefeatedNum.Count; i++)
        {
            bossDefeated[bossDefeatedNum[i]].enabled = true;
        }
    }
}
