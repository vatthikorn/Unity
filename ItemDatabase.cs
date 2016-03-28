/*
    Nathan Cruz

    This is to keep information of every item in the game for the purposes of inventory and equipment.
    A whole lot of other things depend on this too. Basically everything that deals with items.

    Required:
    Attached to an empty object.
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour {

    public List<Item> items = new List<Item>();
}
