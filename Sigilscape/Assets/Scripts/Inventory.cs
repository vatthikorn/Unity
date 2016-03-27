using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

    public List<Item> items = new List<Item>();
    public List<Item> equipment = new List<Item>();
    public List<Item> sigils = new List<Item>();
    public List<Item> consumables = new List<Item>();
    public List<Item> keyItems = new List<Item>();
}
