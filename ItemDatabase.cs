/*
    This is to keep of every item in the game for the purposes of inventory and equipment.

    Required:
    Attached to an empty object in screen.
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour {

    public List<Item> items = new List<Item>();
}
