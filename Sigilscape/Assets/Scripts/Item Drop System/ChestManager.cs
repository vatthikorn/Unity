/*
    Nathan Cruz

    Keeps a list of chests in the zone.

    Interface:
    void LoadChests(List<GameObject> savedChests) - sets chests' states
    List<GameObject> SaveChests() - returns copy of references to chests

    Load Order:
    ChestManager.cs loads before the script that handles the loading from a save file.

    Rememeber to:
    Reference all Chest game object here.
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChestManager : MonoBehaviour {

    public List<GameObject> chests = new List<GameObject>();

    //For loading the game
    public void LoadChests(List<GameObject> savedChests)
    {
        for(int i = 0; i < chests.Count; i++)
        {
            chests[i].GetComponent<Chest>().chestState = savedChests[i].GetComponent<Chest>().chestState;
        }
    }

    //For saving the game
    public List<GameObject> SaveChests()
    {
        return chests;
    }
}
