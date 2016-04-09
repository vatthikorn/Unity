/*
    Nathan Cruz

    NOT COMPLETE

    Inventory for all the items the player has. Manipulates items by picking up objects from the screen, dragging and dropping in the inventory screen (NOT IMPLEMENTED).

    Handles holding items.
    
    Instructions (on how to set up the image for the item slot):
    create a GUISkin (name it and remember the name)
    Go under custom style of GUISkin
    Go under normal of custom style
    Go to background under normal, and set up the source image to be the image you are using
    Slice the image. This will makre sure it only scales borders, and not make the slot image look nasty
    In the code: In DrawInventory in the loop: new Rect (x, null, skin.GetStyle("StyleName")) // where the "StyleName" is the name of the GUISkin you are using
    Make sure to reference the skin as well!
    
    Dependency:
    Item.cs
    ItemDatabase.cs

    Required:
    Attached a gameobject "Inventory".
*/
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    //Objects to reference for purpose of en/disabling inventory screen, getting item information, getting information from the player's equipped items
    public GameObject player;
    public GameObject itemDatabase;
    public GameObject playersEquipment;

    //Different categories of the inventory
    public List<Item> items = new List<Item>();
    public List<Item> equipment = new List<Item>();
    public List<Item> sigils = new List<Item>();
    public List<Item> consumables = new List<Item>();
    public List<Item> keyItems = new List<Item>();

    //To be displayed
    public List<Item> slots = new List<Item>();

    public GUISkin slotSkin;
    public GUISkin inventorySkin;

    //Inventory's Screen Dimensions
    public const float screenUpperLeftAnchorX = .05f;
    public const float screenUpperLeftAnchorY = .125f;
    public const float inventoryWidth = .9f;
    public const float inventoryHeight = .750f;

    //Dimensions of slots
    public const int slotWidth = 50;
    public const int slotHeight = 50;

    //Dimensions of tabs
    public const int tabWidth = 95;
    public const int tabHeight = 25;

    //Controls the tabs
    bool displayAll = true;
    bool displayEquipment = false;
    bool displaySigils = false;
    bool displayConsumables = false;
    bool displayKeyItems = false;

    //Inventory Grid's Dimensions and Counts
    static float inventoryGridUpperLeftAnchorX = .075f;
    static float inventoryGridUpperLeftAnchorY = .20f;
    public const int slotsX = 10;
    public const int slotsY = 5;

    //Information Space's Dimensions
    public const float infoLowerLeftAnchorX = .075f;
    public const float infoLowerLeftAnchorY = .850f;
    public const int infoWidth = 500;
    public const int infoHeight = 100;

    //Anchor for all Equipment slots
    public const float equipmentRightAnchorX = .95f;

    //For dragging items
    bool draggingItem;
    Item draggedItem;
    //activeSigil1 = 1001, activeSigil2 = 1002, activeSigil3 = 1003, activeSigil4 = 1004,
    //passiveSigil1 = 1005, passiveSigil2 = 1006, passiveSigil3 = 1007,
    //weapon = 1008, healthPotion = 1009, armor = 1010, shield = 1011, sigilPotion = 1012
    int prevIndex;

    //Sets up slots
    void Start()
    {
        for (int i = 0; i < (slotsX * slotsY); i++)
        {
            slots.Add(new Item());
            items.Add(new Item());
            equipment.Add(new Item());
            sigils.Add(new Item());
            consumables.Add(new Item());
            keyItems.Add(new Item());
        }
    }

    void OnGUI()
    {
        if (player.GetComponent<PlayerController>().screenState == PlayerController.ScreenState.inventory)
        {
            DrawInventoryScreen();
        }

        if(draggingItem)
        {
            GUI.DrawTexture(new Rect(Event.current.mousePosition.x - slotWidth/2, Event.current.mousePosition.y - slotHeight/2, slotWidth, slotHeight), draggedItem.icon);
        }
    }

    //Draws the entire Inventory Screen
    void DrawInventoryScreen()
    {
        Event e = Event.current;
        int i = 0;
        GUI.skin = slotSkin;

        //Style for Info Box
        GUIStyle guiStyle = GUI.skin.box;
        guiStyle.wordWrap = true;
        guiStyle.alignment = TextAnchor.UpperLeft;

        //Draws the entire panel
        GUI.Box(new Rect(Screen.width * screenUpperLeftAnchorX, Screen.height * screenUpperLeftAnchorY, Screen.width * inventoryWidth, Screen.height * inventoryHeight), "");

        //Drawn either at end or in the middle if the player mouses over an item
        Rect infoRect = new Rect(Screen.width * infoLowerLeftAnchorX, Screen.height * infoLowerLeftAnchorY - infoHeight, infoWidth, infoHeight);
        bool containsInfo = false;

        //Draws ActiveSigil1 slot
        Rect slotRect = new Rect(Screen.width * equipmentRightAnchorX - 290, Screen.height * .2f, slotWidth, slotHeight);
        GUI.Box(slotRect, "");
        if (playersEquipment.GetComponent<Equipment>().activeSigil1.itemName != null)
        {
            GUI.DrawTexture(slotRect, playersEquipment.GetComponent<Equipment>().activeSigil1.icon);
            if (slotRect.Contains(Event.current.mousePosition))
            {
                GUI.Box(infoRect, DisplayInfo(playersEquipment.GetComponent<Equipment>().activeSigil1.itemID), guiStyle);
                containsInfo = true;
                if (e.button == 0 && e.type == EventType.mouseDrag && !draggingItem)
                {
                    draggingItem = true;
                    prevIndex = 1001;
                    draggedItem = playersEquipment.GetComponent<Equipment>().activeSigil1;
                    playersEquipment.GetComponent<Equipment>().activeSigil1 = new Item();
                }
                if(e.type == EventType.mouseUp && draggingItem && draggedItem.itemType == Item.ItemType.sigil && draggedItem.sigil.GetComponent<Sigil>().sigilType != Sigil.SigilType.passive)
                {
                    switch(prevIndex)
                    {
                        case 1001:
                            playersEquipment.GetComponent<Equipment>().activeSigil1 = playersEquipment.GetComponent<Equipment>().activeSigil1;
                            playersEquipment.GetComponent<Equipment>().activeSigil1 = draggedItem;
                            draggingItem = false;
                            draggedItem = null;
                            break;
                        case 1002:
                            playersEquipment.GetComponent<Equipment>().activeSigil2 = playersEquipment.GetComponent<Equipment>().activeSigil1;
                            playersEquipment.GetComponent<Equipment>().activeSigil1 = draggedItem;
                            draggingItem = false;
                            draggedItem = null;
                            break;
                        case 1003:
                            playersEquipment.GetComponent<Equipment>().activeSigil3 = playersEquipment.GetComponent<Equipment>().activeSigil1;
                            playersEquipment.GetComponent<Equipment>().activeSigil1 = draggedItem;
                            draggingItem = false;
                            draggedItem = null;
                            break;
                        case 1004:
                            playersEquipment.GetComponent<Equipment>().activeSigil4 = playersEquipment.GetComponent<Equipment>().activeSigil1;
                            playersEquipment.GetComponent<Equipment>().activeSigil1 = draggedItem;
                            draggingItem = false;
                            draggedItem = null;
                            break;
                        default:
                            if(displayAll)
                            {
                                items[prevIndex] = playersEquipment.GetComponent<Equipment>().activeSigil1;
                                playersEquipment.GetComponent<Equipment>().activeSigil1 = draggedItem;
                                draggingItem = false;
                                draggedItem = null;
                            }
                            else if(displaySigils)
                            {
                                sigils[prevIndex] = playersEquipment.GetComponent<Equipment>().activeSigil1;
                                playersEquipment.GetComponent<Equipment>().activeSigil1 = draggedItem;
                                draggingItem = false;
                                draggedItem = null;
                            }
                            break;
                    }
                }
            }
        }
        else
        {
            if (slotRect.Contains(e.mousePosition))
            {
                if (e.type == EventType.mouseUp && draggingItem && draggedItem.itemType == Item.ItemType.sigil && draggedItem.sigil.GetComponent<Sigil>().sigilType != Sigil.SigilType.passive)
                {
                    playersEquipment.GetComponent<Equipment>().activeSigil1 = draggedItem;
                    draggingItem = false;
                    draggedItem = null;
                }
            }
        }

        //Draws ActiveSigil2 slot
        slotRect = new Rect(Screen.width * equipmentRightAnchorX - 215, Screen.height * .17f, slotWidth, slotHeight);
        GUI.Box(slotRect, "");
        if (playersEquipment.GetComponent<Equipment>().activeSigil2.itemName != null)
        {
            GUI.DrawTexture(slotRect, playersEquipment.GetComponent<Equipment>().activeSigil2.icon);
            if (slotRect.Contains(Event.current.mousePosition))
            {
                GUI.Box(infoRect, DisplayInfo(playersEquipment.GetComponent<Equipment>().activeSigil2.itemID), guiStyle);
                containsInfo = true;
                if (e.button == 0 && e.type == EventType.mouseDrag && !draggingItem)
                {
                    draggingItem = true;
                    prevIndex = 1002;
                    draggedItem = playersEquipment.GetComponent<Equipment>().activeSigil2;
                    playersEquipment.GetComponent<Equipment>().activeSigil2 = new Item();
                }
                if (e.type == EventType.mouseUp && draggingItem && draggedItem.itemType == Item.ItemType.sigil && draggedItem.sigil.GetComponent<Sigil>().sigilType != Sigil.SigilType.passive)
                {
                    switch (prevIndex)
                    {
                        case 1001:
                            playersEquipment.GetComponent<Equipment>().activeSigil1 = playersEquipment.GetComponent<Equipment>().activeSigil2;
                            playersEquipment.GetComponent<Equipment>().activeSigil2 = draggedItem;
                            draggingItem = false;
                            draggedItem = null;
                            break;
                        case 1002:
                            playersEquipment.GetComponent<Equipment>().activeSigil2 = playersEquipment.GetComponent<Equipment>().activeSigil2;
                            playersEquipment.GetComponent<Equipment>().activeSigil2 = draggedItem;
                            draggingItem = false;
                            draggedItem = null;
                            break;
                        case 1003:
                            playersEquipment.GetComponent<Equipment>().activeSigil3 = playersEquipment.GetComponent<Equipment>().activeSigil2;
                            playersEquipment.GetComponent<Equipment>().activeSigil2 = draggedItem;
                            draggingItem = false;
                            draggedItem = null;
                            break;
                        case 1004:
                            playersEquipment.GetComponent<Equipment>().activeSigil4 = playersEquipment.GetComponent<Equipment>().activeSigil2;
                            playersEquipment.GetComponent<Equipment>().activeSigil2 = draggedItem;
                            draggingItem = false;
                            draggedItem = null;
                            break;
                        default:
                            if (displayAll)
                            {
                                items[prevIndex] = playersEquipment.GetComponent<Equipment>().activeSigil2;
                                playersEquipment.GetComponent<Equipment>().activeSigil2 = draggedItem;
                                draggingItem = false;
                                draggedItem = null;
                            }
                            else if (displaySigils)
                            {
                                sigils[prevIndex] = playersEquipment.GetComponent<Equipment>().activeSigil2;
                                playersEquipment.GetComponent<Equipment>().activeSigil2 = draggedItem;
                                draggingItem = false;
                                draggedItem = null;
                            }
                            break;
                    }
                }
            }
        }
        else
        {
            if (slotRect.Contains(e.mousePosition))
            {
                if (e.type == EventType.mouseUp && draggingItem && draggedItem.itemType == Item.ItemType.sigil && draggedItem.sigil.GetComponent<Sigil>().sigilType != Sigil.SigilType.passive)
                {
                    playersEquipment.GetComponent<Equipment>().activeSigil2 = draggedItem;
                    draggingItem = false;
                    draggedItem = null;
                }
            }
        }

        //Draws ActiveSigil3 slot
        slotRect = new Rect(Screen.width * equipmentRightAnchorX - 140, Screen.height * .17f, slotWidth, slotHeight);
        GUI.Box(slotRect, "");
        if (playersEquipment.GetComponent<Equipment>().activeSigil3.itemName != null)
        {
            GUI.DrawTexture(slotRect, playersEquipment.GetComponent<Equipment>().activeSigil3.icon);
            if (slotRect.Contains(Event.current.mousePosition))
            {
                GUI.Box(infoRect, DisplayInfo(playersEquipment.GetComponent<Equipment>().activeSigil3.itemID), guiStyle);
                containsInfo = true;
                if (e.button == 0 && e.type == EventType.mouseDrag && !draggingItem)
                {
                    draggingItem = true;
                    prevIndex = 1003;
                    draggedItem = playersEquipment.GetComponent<Equipment>().activeSigil3;
                    playersEquipment.GetComponent<Equipment>().activeSigil3 = new Item();
                }
                if (e.type == EventType.mouseUp && draggingItem && draggedItem.itemType == Item.ItemType.sigil && draggedItem.sigil.GetComponent<Sigil>().sigilType != Sigil.SigilType.passive)
                {
                    switch (prevIndex)
                    {
                        case 1001:
                            playersEquipment.GetComponent<Equipment>().activeSigil1 = playersEquipment.GetComponent<Equipment>().activeSigil3;
                            playersEquipment.GetComponent<Equipment>().activeSigil3 = draggedItem;
                            draggingItem = false;
                            draggedItem = null;
                            break;
                        case 1002:
                            playersEquipment.GetComponent<Equipment>().activeSigil2 = playersEquipment.GetComponent<Equipment>().activeSigil3;
                            playersEquipment.GetComponent<Equipment>().activeSigil3 = draggedItem;
                            draggingItem = false;
                            draggedItem = null;
                            break;
                        case 1003:
                            playersEquipment.GetComponent<Equipment>().activeSigil3 = playersEquipment.GetComponent<Equipment>().activeSigil3;
                            playersEquipment.GetComponent<Equipment>().activeSigil3 = draggedItem;
                            draggingItem = false;
                            draggedItem = null;
                            break;
                        case 1004:
                            playersEquipment.GetComponent<Equipment>().activeSigil4 = playersEquipment.GetComponent<Equipment>().activeSigil3;
                            playersEquipment.GetComponent<Equipment>().activeSigil3 = draggedItem;
                            draggingItem = false;
                            draggedItem = null;
                            break;
                        default:
                            if (displayAll)
                            {
                                items[prevIndex] = playersEquipment.GetComponent<Equipment>().activeSigil3;
                                playersEquipment.GetComponent<Equipment>().activeSigil3 = draggedItem;
                                draggingItem = false;
                                draggedItem = null;
                            }
                            else if (displaySigils)
                            {
                                sigils[prevIndex] = playersEquipment.GetComponent<Equipment>().activeSigil3;
                                playersEquipment.GetComponent<Equipment>().activeSigil3 = draggedItem;
                                draggingItem = false;
                                draggedItem = null;
                            }
                            break;
                    }
                }
            }
        }
        else
        {
            if (slotRect.Contains(e.mousePosition))
            {
                if (e.type == EventType.mouseUp && draggingItem && draggedItem.itemType == Item.ItemType.sigil && draggedItem.sigil.GetComponent<Sigil>().sigilType != Sigil.SigilType.passive)
                {
                    playersEquipment.GetComponent<Equipment>().activeSigil3 = draggedItem;
                    draggingItem = false;
                    draggedItem = null;
                }
            }
        }

        //Draws ActiveSigil4 slot
        slotRect = new Rect(Screen.width * equipmentRightAnchorX - 65, Screen.height * .2f, slotWidth, slotHeight);
        GUI.Box(slotRect, "");
        if (playersEquipment.GetComponent<Equipment>().activeSigil4.itemName != null)
        {
            GUI.DrawTexture(slotRect, playersEquipment.GetComponent<Equipment>().activeSigil4.icon);
            if (slotRect.Contains(Event.current.mousePosition))
            {
                GUI.Box(infoRect, DisplayInfo(playersEquipment.GetComponent<Equipment>().activeSigil4.itemID), guiStyle);
                containsInfo = true;
                if (e.button == 0 && e.type == EventType.mouseDrag && !draggingItem)
                {
                    draggingItem = true;
                    prevIndex = 1004;
                    draggedItem = playersEquipment.GetComponent<Equipment>().activeSigil4;
                    playersEquipment.GetComponent<Equipment>().activeSigil4 = new Item();
                }
                if (e.type == EventType.mouseUp && draggingItem && draggedItem.itemType == Item.ItemType.sigil && draggedItem.sigil.GetComponent<Sigil>().sigilType != Sigil.SigilType.passive)
                {
                    switch (prevIndex)
                    {
                        case 1001:
                            playersEquipment.GetComponent<Equipment>().activeSigil1 = playersEquipment.GetComponent<Equipment>().activeSigil4;
                            playersEquipment.GetComponent<Equipment>().activeSigil4 = draggedItem;
                            draggingItem = false;
                            draggedItem = null;
                            break;
                        case 1002:
                            playersEquipment.GetComponent<Equipment>().activeSigil2 = playersEquipment.GetComponent<Equipment>().activeSigil4;
                            playersEquipment.GetComponent<Equipment>().activeSigil4 = draggedItem;
                            draggingItem = false;
                            draggedItem = null;
                            break;
                        case 1003:
                            playersEquipment.GetComponent<Equipment>().activeSigil3 = playersEquipment.GetComponent<Equipment>().activeSigil4;
                            playersEquipment.GetComponent<Equipment>().activeSigil4 = draggedItem;
                            draggingItem = false;
                            draggedItem = null;
                            break;
                        case 1004:
                            playersEquipment.GetComponent<Equipment>().activeSigil4 = playersEquipment.GetComponent<Equipment>().activeSigil4;
                            playersEquipment.GetComponent<Equipment>().activeSigil4 = draggedItem;
                            draggingItem = false;
                            draggedItem = null;
                            break;
                        default:
                            if (displayAll)
                            {
                                items[prevIndex] = playersEquipment.GetComponent<Equipment>().activeSigil4;
                                playersEquipment.GetComponent<Equipment>().activeSigil4 = draggedItem;
                                draggingItem = false;
                                draggedItem = null;
                            }
                            else if (displaySigils)
                            {
                                sigils[prevIndex] = playersEquipment.GetComponent<Equipment>().activeSigil4;
                                playersEquipment.GetComponent<Equipment>().activeSigil4 = draggedItem;
                                draggingItem = false;
                                draggedItem = null;
                            }
                            break;
                    }
                }
            }
        }
        else
        {
            if (slotRect.Contains(e.mousePosition))
            {
                if (e.type == EventType.mouseUp && draggingItem && draggedItem.itemType == Item.ItemType.sigil && draggedItem.sigil.GetComponent<Sigil>().sigilType != Sigil.SigilType.passive)
                {
                    playersEquipment.GetComponent<Equipment>().activeSigil4 = draggedItem;
                    draggingItem = false;
                    draggedItem = null;
                }
            }
        }

        //Draws PassiveSigil1 slot
        slotRect = new Rect(Screen.width * equipmentRightAnchorX - 250, Screen.height * .85f - 88, slotWidth, slotHeight);
        GUI.Box(slotRect, "");
        if (playersEquipment.GetComponent<Equipment>().passiveSigil1.itemName != null)
        {
            GUI.DrawTexture(slotRect, playersEquipment.GetComponent<Equipment>().passiveSigil1.icon);
            if (slotRect.Contains(Event.current.mousePosition))
            {
                GUI.Box(infoRect, DisplayInfo(playersEquipment.GetComponent<Equipment>().passiveSigil1.itemID), guiStyle);
                containsInfo = true;
                if (e.button == 0 && e.type == EventType.mouseDrag && !draggingItem)
                {
                    draggingItem = true;
                    prevIndex = 1005;
                    draggedItem = playersEquipment.GetComponent<Equipment>().passiveSigil1;
                    playersEquipment.GetComponent<Equipment>().passiveSigil1 = new Item();
                }
                if (e.type == EventType.mouseUp && draggingItem && draggedItem.itemType == Item.ItemType.sigil && draggedItem.sigil.GetComponent<Sigil>().sigilType == Sigil.SigilType.passive)
                {
                    switch (prevIndex)
                    {
                        case 1005:
                            playersEquipment.GetComponent<Equipment>().passiveSigil1 = playersEquipment.GetComponent<Equipment>().passiveSigil1;
                            playersEquipment.GetComponent<Equipment>().passiveSigil1 = draggedItem;
                            draggingItem = false;
                            draggedItem = null;
                            break;
                        case 1006:
                            playersEquipment.GetComponent<Equipment>().passiveSigil2 = playersEquipment.GetComponent<Equipment>().passiveSigil1;
                            playersEquipment.GetComponent<Equipment>().passiveSigil1 = draggedItem;
                            draggingItem = false;
                            draggedItem = null;
                            break;
                        case 1007:
                            playersEquipment.GetComponent<Equipment>().passiveSigil3 = playersEquipment.GetComponent<Equipment>().passiveSigil1;
                            playersEquipment.GetComponent<Equipment>().passiveSigil1 = draggedItem;
                            draggingItem = false;
                            draggedItem = null;
                            break;
                        default:
                            if (displayAll)
                            {
                                items[prevIndex] = playersEquipment.GetComponent<Equipment>().passiveSigil1;
                                playersEquipment.GetComponent<Equipment>().passiveSigil1 = draggedItem;
                                draggingItem = false;
                                draggedItem = null;
                            }
                            else if (displaySigils)
                            {
                                sigils[prevIndex] = playersEquipment.GetComponent<Equipment>().passiveSigil1;
                                playersEquipment.GetComponent<Equipment>().passiveSigil1 = draggedItem;
                                draggingItem = false;
                                draggedItem = null;
                            }
                            break;
                    }
                }
            }
        }
        else
        {
            if (slotRect.Contains(e.mousePosition))
            {
                if (e.type == EventType.mouseUp && draggingItem && draggedItem.itemType == Item.ItemType.sigil && draggedItem.sigil.GetComponent<Sigil>().sigilType == Sigil.SigilType.passive)
                {
                    playersEquipment.GetComponent<Equipment>().passiveSigil1 = draggedItem;
                    draggingItem = false;
                    draggedItem = null;
                }
            }
        }

        //Draws PassiveSigil2 slot
        slotRect = new Rect(Screen.width * equipmentRightAnchorX - 175, Screen.height * .85f - 75, slotWidth, slotHeight);
        GUI.Box(slotRect, "");
        if (playersEquipment.GetComponent<Equipment>().passiveSigil2.itemName != null)
        {
            GUI.DrawTexture(slotRect, playersEquipment.GetComponent<Equipment>().passiveSigil2.icon);
            if (slotRect.Contains(Event.current.mousePosition))
            {
                GUI.Box(infoRect, DisplayInfo(playersEquipment.GetComponent<Equipment>().passiveSigil2.itemID), guiStyle);
                containsInfo = true;
                if (e.button == 0 && e.type == EventType.mouseDrag && !draggingItem)
                {
                    draggingItem = true;
                    prevIndex = 1006;
                    draggedItem = playersEquipment.GetComponent<Equipment>().passiveSigil2;
                    playersEquipment.GetComponent<Equipment>().passiveSigil2 = new Item();
                }
                if (e.type == EventType.mouseUp && draggingItem && draggedItem.itemType == Item.ItemType.sigil && draggedItem.sigil.GetComponent<Sigil>().sigilType == Sigil.SigilType.passive)
                {
                    switch (prevIndex)
                    {
                        case 1005:
                            playersEquipment.GetComponent<Equipment>().passiveSigil1 = playersEquipment.GetComponent<Equipment>().passiveSigil2;
                            playersEquipment.GetComponent<Equipment>().passiveSigil2 = draggedItem;
                            draggingItem = false;
                            draggedItem = null;
                            break;
                        case 1006:
                            playersEquipment.GetComponent<Equipment>().passiveSigil2 = playersEquipment.GetComponent<Equipment>().passiveSigil2;
                            playersEquipment.GetComponent<Equipment>().passiveSigil2 = draggedItem;
                            draggingItem = false;
                            draggedItem = null;
                            break;
                        case 1007:
                            playersEquipment.GetComponent<Equipment>().passiveSigil3 = playersEquipment.GetComponent<Equipment>().passiveSigil2;
                            playersEquipment.GetComponent<Equipment>().passiveSigil2 = draggedItem;
                            draggingItem = false;
                            draggedItem = null;
                            break;
                        default:
                            if (displayAll)
                            {
                                items[prevIndex] = playersEquipment.GetComponent<Equipment>().passiveSigil2;
                                playersEquipment.GetComponent<Equipment>().passiveSigil2 = draggedItem;
                                draggingItem = false;
                                draggedItem = null;
                            }
                            else if (displaySigils)
                            {
                                sigils[prevIndex] = playersEquipment.GetComponent<Equipment>().passiveSigil2;
                                playersEquipment.GetComponent<Equipment>().passiveSigil2 = draggedItem;
                                draggingItem = false;
                                draggedItem = null;
                            }
                            break;
                    }
                }
            }
        }
        else
        {
            if (slotRect.Contains(e.mousePosition))
            {
                if (e.type == EventType.mouseUp && draggingItem && draggingItem && draggedItem.itemType == Item.ItemType.sigil && draggedItem.sigil.GetComponent<Sigil>().sigilType == Sigil.SigilType.passive)
                {
                    playersEquipment.GetComponent<Equipment>().passiveSigil2= draggedItem;
                    draggingItem = false;
                    draggedItem = null;
                }
            }
        }

        //Draws PassiveSigil3 slot
        slotRect = new Rect(Screen.width * equipmentRightAnchorX - 100, Screen.height * .85f - 88, slotWidth, slotHeight);
        GUI.Box(slotRect, "");
        if (playersEquipment.GetComponent<Equipment>().passiveSigil3.itemName != null)
        {
            GUI.DrawTexture(slotRect, playersEquipment.GetComponent<Equipment>().passiveSigil3.icon);
            if (slotRect.Contains(Event.current.mousePosition))
            {
                GUI.Box(infoRect, DisplayInfo(playersEquipment.GetComponent<Equipment>().passiveSigil3.itemID), guiStyle);
                containsInfo = true;
                if (e.button == 0 && e.type == EventType.mouseDrag && !draggingItem)
                {
                    draggingItem = true;
                    prevIndex = 1007;
                    draggedItem = playersEquipment.GetComponent<Equipment>().passiveSigil3;
                    playersEquipment.GetComponent<Equipment>().passiveSigil3 = new Item();
                }
                if (e.type == EventType.mouseUp && draggingItem && draggedItem.itemType == Item.ItemType.sigil && draggedItem.sigil.GetComponent<Sigil>().sigilType == Sigil.SigilType.passive)
                {
                    switch (prevIndex)
                    {
                        case 1005:
                            playersEquipment.GetComponent<Equipment>().passiveSigil1 = playersEquipment.GetComponent<Equipment>().passiveSigil3;
                            playersEquipment.GetComponent<Equipment>().passiveSigil3 = draggedItem;
                            draggingItem = false;
                            draggedItem = null;
                            break;
                        case 1006:
                            playersEquipment.GetComponent<Equipment>().passiveSigil2 = playersEquipment.GetComponent<Equipment>().passiveSigil3;
                            playersEquipment.GetComponent<Equipment>().passiveSigil3 = draggedItem;
                            draggingItem = false;
                            draggedItem = null;
                            break;
                        case 1007:
                            playersEquipment.GetComponent<Equipment>().passiveSigil3 = playersEquipment.GetComponent<Equipment>().passiveSigil3;
                            playersEquipment.GetComponent<Equipment>().passiveSigil3 = draggedItem;
                            draggingItem = false;
                            draggedItem = null;
                            break;
                        default:
                            if (displayAll)
                            {
                                items[prevIndex] = playersEquipment.GetComponent<Equipment>().passiveSigil3;
                                playersEquipment.GetComponent<Equipment>().passiveSigil3 = draggedItem;
                                draggingItem = false;
                                draggedItem = null;
                            }
                            else if (displaySigils)
                            {
                                sigils[prevIndex] = playersEquipment.GetComponent<Equipment>().passiveSigil3;
                                playersEquipment.GetComponent<Equipment>().passiveSigil3 = draggedItem;
                                draggingItem = false;
                                draggedItem = null;
                            }
                            break;
                    }
                }
            }
        }
        else
        {
            if (slotRect.Contains(e.mousePosition))
            {
                if (e.type == EventType.mouseUp && draggingItem && draggingItem && draggedItem.itemType == Item.ItemType.sigil && draggedItem.sigil.GetComponent<Sigil>().sigilType == Sigil.SigilType.passive)
                {
                    playersEquipment.GetComponent<Equipment>().passiveSigil3 = draggedItem;
                    draggingItem = false;
                    draggedItem = null;
                }
            }
        }

        //Draws Weapon slot
        slotRect = new Rect(Screen.width * equipmentRightAnchorX - 290, Screen.height * .5f - 45, slotWidth, slotHeight);
        GUI.Box(slotRect, "");
        if (playersEquipment.GetComponent<Equipment>().weapon.itemName != null)
        {
            GUI.DrawTexture(slotRect, playersEquipment.GetComponent<Equipment>().weapon.icon);
            if (slotRect.Contains(Event.current.mousePosition))
            {
                GUI.Box(infoRect, DisplayInfo(playersEquipment.GetComponent<Equipment>().weapon.itemID), guiStyle);
                containsInfo = true;
                if (e.button == 0 && e.type == EventType.mouseDrag && !draggingItem)
                {
                    draggingItem = true;
                    prevIndex = 1008;
                    draggedItem = playersEquipment.GetComponent<Equipment>().weapon;
                    playersEquipment.GetComponent<Equipment>().weapon = new Item();
                }
                if (e.type == EventType.mouseUp && draggingItem && draggedItem.itemType == Item.ItemType.weapon)
                {
                    switch (prevIndex)
                    {
                        case 1008:
                            playersEquipment.GetComponent<Equipment>().weapon = playersEquipment.GetComponent<Equipment>().weapon;
                            playersEquipment.GetComponent<Equipment>().weapon = draggedItem;
                            draggingItem = false;
                            draggedItem = null;
                            break;
                        default:
                            if (displayAll)
                            {
                                items[prevIndex] = playersEquipment.GetComponent<Equipment>().weapon;
                                playersEquipment.GetComponent<Equipment>().weapon = draggedItem;
                                draggingItem = false;
                                draggedItem = null;
                            }
                            else if (displayEquipment)
                            {
                                equipment[prevIndex] = playersEquipment.GetComponent<Equipment>().weapon;
                                playersEquipment.GetComponent<Equipment>().weapon = draggedItem;
                                draggingItem = false;
                                draggedItem = null;
                            }
                            break;
                    }
                }
            }
        }
        else
        {
            if (slotRect.Contains(e.mousePosition))
            {
                if (e.type == EventType.mouseUp && draggingItem && draggedItem.itemType == Item.ItemType.weapon)
                {
                    playersEquipment.GetComponent<Equipment>().weapon = draggedItem;
                    draggingItem = false;
                    draggedItem = null;
                }
            }
        }

        //Draws HealthPotion slot
        slotRect = new Rect(Screen.width * equipmentRightAnchorX - 275, Screen.height * .5f + 30, slotWidth, slotHeight);
        GUI.Box(slotRect, "");
        if (playersEquipment.GetComponent<Equipment>().healthPotions.itemName != null)
        {
            GUI.DrawTexture(slotRect, playersEquipment.GetComponent<Equipment>().healthPotions.icon);
            if (slotRect.Contains(Event.current.mousePosition))
            {
                GUI.Box(infoRect, DisplayInfo(playersEquipment.GetComponent<Equipment>().healthPotions.itemID), guiStyle);
                containsInfo = true;
                if (e.button == 0 && e.type == EventType.mouseDrag && !draggingItem)
                {
                    draggingItem = true;
                    prevIndex = 1009;
                    draggedItem = playersEquipment.GetComponent<Equipment>().healthPotions;
                    playersEquipment.GetComponent<Equipment>().healthPotions= new Item();
                }
                if (e.type == EventType.mouseUp && draggingItem && draggedItem.itemType == Item.ItemType.consumable && draggedItem.itemName.Contains("Healing Potion"))
                {
                    switch (prevIndex)
                    {
                        case 1009:
                            playersEquipment.GetComponent<Equipment>().healthPotions = playersEquipment.GetComponent<Equipment>().healthPotions;
                            playersEquipment.GetComponent<Equipment>().healthPotions = draggedItem;
                            draggingItem = false;
                            draggedItem = null;
                            break;
                        default:
                            if (displayAll)
                            {
                                items[prevIndex] = playersEquipment.GetComponent<Equipment>().healthPotions;
                                playersEquipment.GetComponent<Equipment>().healthPotions = draggedItem;
                                draggingItem = false;
                                draggedItem = null;
                            }
                            else if (displayConsumables)
                            {
                                consumables[prevIndex] = playersEquipment.GetComponent<Equipment>().healthPotions;
                                playersEquipment.GetComponent<Equipment>().healthPotions = draggedItem;
                                draggingItem = false;
                                draggedItem = null;
                            }
                            break;
                    }
                }
            }
        }
        else
        {
            if (slotRect.Contains(e.mousePosition))
            {
                if (e.type == EventType.mouseUp && draggingItem && draggedItem.itemType == Item.ItemType.consumable && draggedItem.itemName.Contains("Healing Potion"))
                {
                    playersEquipment.GetComponent<Equipment>().healthPotions = draggedItem;
                    draggingItem = false;
                    draggedItem = null;
                }
            }
        }

        //Draws Armor slot
        slotRect = new Rect(Screen.width * equipmentRightAnchorX - 175, Screen.height * .5f - 25, slotWidth, slotHeight);
        GUI.Box(slotRect, "");
        if (playersEquipment.GetComponent<Equipment>().armor.itemName != null)
        {
            GUI.DrawTexture(slotRect, playersEquipment.GetComponent<Equipment>().armor.icon);
            if (slotRect.Contains(Event.current.mousePosition))
            {
                GUI.Box(infoRect, DisplayInfo(playersEquipment.GetComponent<Equipment>().armor.itemID), guiStyle);
                containsInfo = true;
                if (e.button == 0 && e.type == EventType.mouseDrag && !draggingItem)
                {
                    draggingItem = true;
                    prevIndex = 1010;
                    draggedItem = playersEquipment.GetComponent<Equipment>().armor;
                    playersEquipment.GetComponent<Equipment>().armor = new Item();
                }
                if (e.type == EventType.mouseUp && draggingItem && draggedItem.itemType == Item.ItemType.armor)
                {
                    switch (prevIndex)
                    {
                        case 1010:
                            playersEquipment.GetComponent<Equipment>().armor = playersEquipment.GetComponent<Equipment>().armor;
                            playersEquipment.GetComponent<Equipment>().armor = draggedItem;
                            draggingItem = false;
                            draggedItem = null;
                            break;
                        default:
                            if (displayAll)
                            {
                                items[prevIndex] = playersEquipment.GetComponent<Equipment>().armor;
                                playersEquipment.GetComponent<Equipment>().armor = draggedItem;
                                draggingItem = false;
                                draggedItem = null;
                            }
                            else if (displayEquipment)
                            {
                                equipment[prevIndex] = playersEquipment.GetComponent<Equipment>().armor;
                                playersEquipment.GetComponent<Equipment>().armor = draggedItem;
                                draggingItem = false;
                                draggedItem = null;
                            }
                            break;
                    }
                }
            }
        }
        else
        {
            if (slotRect.Contains(e.mousePosition))
            {
                if (e.type == EventType.mouseUp && draggingItem && draggedItem.itemType == Item.ItemType.armor)
                {
                    playersEquipment.GetComponent<Equipment>().armor = draggedItem;
                    draggingItem = false;
                    draggedItem = null;
                }
            }
        }

        //Draws Shield slot
        slotRect = new Rect(Screen.width * equipmentRightAnchorX - 65, Screen.height * .5f - 45, slotWidth, slotHeight);
        GUI.Box(slotRect, "");
        if (playersEquipment.GetComponent<Equipment>().shield.itemName != null)
        {
            GUI.DrawTexture(slotRect, playersEquipment.GetComponent<Equipment>().shield.icon);
            if (slotRect.Contains(Event.current.mousePosition))
            {
                GUI.Box(infoRect, DisplayInfo(playersEquipment.GetComponent<Equipment>().shield.itemID), guiStyle);
                containsInfo = true;
                if (e.button == 0 && e.type == EventType.mouseDrag && !draggingItem)
                {
                    draggingItem = true;
                    prevIndex = 1011;
                    draggedItem = playersEquipment.GetComponent<Equipment>().shield;
                    playersEquipment.GetComponent<Equipment>().shield = new Item();
                }
                if (e.type == EventType.mouseUp && draggingItem && draggedItem.itemType == Item.ItemType.shield)
                {
                    switch (prevIndex)
                    {
                        case 1011:
                            playersEquipment.GetComponent<Equipment>().shield = playersEquipment.GetComponent<Equipment>().shield;
                            playersEquipment.GetComponent<Equipment>().shield = draggedItem;
                            draggingItem = false;
                            draggedItem = null;
                            break;
                        default:
                            if (displayAll)
                            {
                                items[prevIndex] = playersEquipment.GetComponent<Equipment>().shield;
                                playersEquipment.GetComponent<Equipment>().shield = draggedItem;
                                draggingItem = false;
                                draggedItem = null;
                            }
                            else if (displayEquipment)
                            {
                                equipment[prevIndex] = playersEquipment.GetComponent<Equipment>().shield;
                                playersEquipment.GetComponent<Equipment>().shield = draggedItem;
                                draggingItem = false;
                                draggedItem = null;
                            }
                            break;
                    }
                }
            }
        }
        else
        {
            if (slotRect.Contains(e.mousePosition))
            {
                if (e.type == EventType.mouseUp && draggingItem && draggedItem.itemType == Item.ItemType.shield)
                {
                    playersEquipment.GetComponent<Equipment>().shield = draggedItem;
                    draggingItem = false;
                    draggedItem = null;
                }
            }
        }

        //Draws SigilPotion slot
        slotRect = new Rect(Screen.width * equipmentRightAnchorX - 80, Screen.height * .5f + 30, slotWidth, slotHeight);
        GUI.Box(slotRect, "");
        if (playersEquipment.GetComponent<Equipment>().sigilPotions.itemName != null)
        {
            GUI.DrawTexture(slotRect, playersEquipment.GetComponent<Equipment>().sigilPotions.icon);
            if (slotRect.Contains(Event.current.mousePosition))
            {
                GUI.Box(infoRect, DisplayInfo(playersEquipment.GetComponent<Equipment>().sigilPotions.itemID), guiStyle);
                containsInfo = true;
                if (e.button == 0 && e.type == EventType.mouseDrag && !draggingItem)
                {
                    draggingItem = true;
                    prevIndex = 1012;
                    draggedItem = playersEquipment.GetComponent<Equipment>().sigilPotions;
                    playersEquipment.GetComponent<Equipment>().sigilPotions = new Item();
                }
            }
            if (e.type == EventType.mouseUp && draggingItem && draggedItem.itemType == Item.ItemType.consumable && draggedItem.itemName.Contains("Sigil Potion"))
            {
                switch (prevIndex)
                {
                    case 1009:
                        playersEquipment.GetComponent<Equipment>().sigilPotions = playersEquipment.GetComponent<Equipment>().sigilPotions;
                        playersEquipment.GetComponent<Equipment>().sigilPotions = draggedItem;
                        draggingItem = false;
                        draggedItem = null;
                        break;
                    default:
                        if (displayAll)
                        {
                            items[prevIndex] = playersEquipment.GetComponent<Equipment>().sigilPotions;
                            playersEquipment.GetComponent<Equipment>().sigilPotions = draggedItem;
                            draggingItem = false;
                            draggedItem = null;
                        }
                        else if (displayConsumables)
                        {
                            consumables[prevIndex] = playersEquipment.GetComponent<Equipment>().sigilPotions;
                            playersEquipment.GetComponent<Equipment>().sigilPotions = draggedItem;
                            draggingItem = false;
                            draggedItem = null;
                        }
                        break;
                }
            }
        }
        else
        {
            if (slotRect.Contains(e.mousePosition))
            {
                if (e.type == EventType.mouseUp && draggingItem && draggedItem.itemType == Item.ItemType.consumable && draggedItem.itemName.Contains("Sigil Potion"))
                {
                    playersEquipment.GetComponent<Equipment>().sigilPotions = draggedItem;
                    draggingItem = false;
                    draggedItem = null;
                }
            }
        }

        //Draws Buttons, and Switches tabs
        Rect buttonRect = new Rect(Screen.width * inventoryGridUpperLeftAnchorX, Screen.height * inventoryGridUpperLeftAnchorY - tabHeight, tabWidth, tabHeight);
        if (GUI.Button(buttonRect, "All"))
        {
            displayAll = true;
            displayEquipment = false;
            displaySigils = false;
            displayConsumables = false;
            displayKeyItems = false;
        }
        buttonRect = new Rect(Screen.width * inventoryGridUpperLeftAnchorX + 95, Screen.height * inventoryGridUpperLeftAnchorY - tabHeight, tabWidth, tabHeight);
        if (GUI.Button(buttonRect, "Equipment"))
        {
            displayAll = false;
            displayEquipment = true;
            displaySigils = false;
            displayConsumables = false;
            displayKeyItems = false;
        }
        buttonRect = new Rect(Screen.width * inventoryGridUpperLeftAnchorX + tabWidth * 2, Screen.height * inventoryGridUpperLeftAnchorY - tabHeight, tabWidth, tabHeight);
        if (GUI.Button(buttonRect, "Sigils"))
        {
            displayAll = false;
            displayEquipment = false;
            displaySigils = true;
            displayConsumables = false;
            displayKeyItems = false;
        }
        buttonRect = new Rect(Screen.width * inventoryGridUpperLeftAnchorX + tabWidth * 3, Screen.height * inventoryGridUpperLeftAnchorY - tabHeight, tabWidth, tabHeight);
        if (GUI.Button(buttonRect, "Consumables"))
        {
            displayAll = false;
            displayEquipment = false;
            displaySigils = false;
            displayConsumables = true;
            displayKeyItems = false;
        }
        buttonRect = new Rect(Screen.width * inventoryGridUpperLeftAnchorX + tabWidth * 4, Screen.height * inventoryGridUpperLeftAnchorY - tabHeight, tabWidth, tabHeight);
        if (GUI.Button(buttonRect, "Key Items"))
        {
            displayAll = false;
            displayEquipment = false;
            displaySigils = false;
            displayConsumables = false;
            displayKeyItems = true;
        }
        
        //Draws grid of inventory
        for (int y = 0; y < slotsY; y++)
        {
            for (int x = 0; x < slotsX; x++)
            {
                //Draws all inventory
                if (displayAll)
                {
                    slotRect = new Rect(x * slotWidth + Screen.width * inventoryGridUpperLeftAnchorX, y * slotHeight + Screen.height * inventoryGridUpperLeftAnchorY, slotWidth, slotHeight);
                    GUI.Box(slotRect, "");
                    if (i < items.Count)
                    {
                        slots[i] = items[i];
                    }
                    if (slots[i].itemName != null)
                    {
                        GUI.DrawTexture(slotRect, slots[i].icon);
                        if (slots[i].itemName != null)
                        {
                            GUI.DrawTexture(slotRect, slots[i].icon);
                            if (slotRect.Contains(Event.current.mousePosition))
                            {
                                GUI.Box(infoRect, DisplayInfo(slots[i].itemID), guiStyle);
                                containsInfo = true;
                                if (e.button == 0 && e.type == EventType.mouseDrag && !draggingItem)
                                {
                                    draggingItem = true;
                                    prevIndex = i;
                                    draggedItem = slots[i];
                                    items[i] = new Item();
                                }
                                if (e.type == EventType.mouseUp && draggingItem)
                                {
                                    switch (prevIndex)
                                    {
                                        case 1001:
                                            playersEquipment.GetComponent<Equipment>().activeSigil1 = items[i];
                                            items[i] = draggedItem;
                                            draggingItem = false;
                                            draggedItem = null;
                                            break;
                                        case 1002:
                                            playersEquipment.GetComponent<Equipment>().activeSigil2 = items[i];
                                            items[i] = draggedItem;
                                            draggingItem = false;
                                            draggedItem = null;
                                            break;
                                        case 1003:
                                            playersEquipment.GetComponent<Equipment>().activeSigil3 = items[i];
                                            items[i] = draggedItem;
                                            draggingItem = false;
                                            draggedItem = null;
                                            break;
                                        case 1004:
                                            playersEquipment.GetComponent<Equipment>().activeSigil4 = items[i];
                                            items[i] = draggedItem;
                                            draggingItem = false;
                                            draggedItem = null;
                                            break;
                                        case 1005:
                                            playersEquipment.GetComponent<Equipment>().passiveSigil1 = items[i];
                                            items[i] = draggedItem;
                                            draggingItem = false;
                                            draggedItem = null;
                                            break;
                                        case 1006:
                                            playersEquipment.GetComponent<Equipment>().passiveSigil2 = items[i];
                                            items[i] = draggedItem;
                                            draggingItem = false;
                                            draggedItem = null;
                                            break;
                                        case 1007:
                                            playersEquipment.GetComponent<Equipment>().passiveSigil3 = items[i];
                                            items[i] = draggedItem;
                                            draggingItem = false;
                                            draggedItem = null;
                                            break;
                                        case 1008:
                                            playersEquipment.GetComponent<Equipment>().weapon = items[i];
                                            items[i] = draggedItem;
                                            draggingItem = false;
                                            draggedItem = null;
                                            break;
                                        case 1009:
                                            playersEquipment.GetComponent<Equipment>().healthPotions = items[i];
                                            items[i] = draggedItem;
                                            draggingItem = false;
                                            draggedItem = null;
                                            break;
                                        case 1010:
                                            playersEquipment.GetComponent<Equipment>().armor = items[i];
                                            items[i] = draggedItem;
                                            draggingItem = false;
                                            draggedItem = null;
                                            break;
                                        case 1011:
                                            playersEquipment.GetComponent<Equipment>().shield = items[i];
                                            items[i] = draggedItem;
                                            draggingItem = false;
                                            draggedItem = null;
                                            break;
                                        case 1012:
                                            playersEquipment.GetComponent<Equipment>().sigilPotions = items[i];
                                            items[i] = draggedItem;
                                            draggingItem = false;
                                            draggedItem = null;
                                            break;
                                        default:
                                            items[prevIndex] = items[i];
                                            items[i] = draggedItem;
                                            draggingItem = false;
                                            draggedItem = null;
                                            break;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (slotRect.Contains(e.mousePosition))
                        {
                            if (e.type == EventType.mouseUp && draggingItem)
                            {
                                items[i] = draggedItem;
                                draggingItem = false;
                                draggedItem = null;
                            }
                        }
                    }
                }

                //Draws equipment inventory
                else if (displayEquipment)
                {
                    slotRect = new Rect(x * slotWidth + Screen.width * inventoryGridUpperLeftAnchorX, y * slotHeight + Screen.height * inventoryGridUpperLeftAnchorY, slotWidth, slotHeight);
                    GUI.Box(slotRect, "");
                    if (i < items.Count)
                    {
                        slots[i] = equipment[i];
                    }
                    if (slots[i].itemName != null)
                    {
                        GUI.DrawTexture(slotRect, slots[i].icon);
                        if (slots[i].itemName != null)
                        {
                            GUI.DrawTexture(slotRect, slots[i].icon);
                            if (slotRect.Contains(Event.current.mousePosition))
                            {
                                GUI.Box(infoRect, DisplayInfo(slots[i].itemID), guiStyle);
                                containsInfo = true;
                                if (e.button == 0 && e.type == EventType.mouseDrag && !draggingItem)
                                {
                                    draggingItem = true;
                                    prevIndex = i;
                                    draggedItem = slots[i];
                                    equipment[i] = new Item();
                                }
                                if (e.type == EventType.mouseUp && draggingItem && (draggedItem.itemType == Item.ItemType.weapon || draggedItem.itemType == Item.ItemType.armor || draggedItem.itemType == Item.ItemType.shield))
                                {
                                    switch (prevIndex)
                                    {
                                        case 1008:
                                            playersEquipment.GetComponent<Equipment>().weapon = equipment[i];
                                            equipment[i] = draggedItem;
                                            draggingItem = false;
                                            draggedItem = null;
                                            break;
                                        case 1010:
                                            playersEquipment.GetComponent<Equipment>().armor = equipment[i];
                                            equipment[i] = draggedItem;
                                            draggingItem = false;
                                            draggedItem = null;
                                            break;
                                        case 1011:
                                            playersEquipment.GetComponent<Equipment>().shield = equipment[i];
                                            equipment[i] = draggedItem;
                                            draggingItem = false;
                                            draggedItem = null;
                                            break;
                                        default:
                                            equipment[prevIndex] = equipment[i];
                                            equipment[i] = draggedItem;
                                            draggingItem = false;
                                            draggedItem = null;
                                            break;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (slotRect.Contains(e.mousePosition))
                        {
                            if (e.type == EventType.mouseUp && draggingItem && (draggedItem.itemType == Item.ItemType.weapon || draggedItem.itemType == Item.ItemType.armor || draggedItem.itemType == Item.ItemType.shield))
                            {
                                equipment[i] = draggedItem;
                                draggingItem = false;
                                draggedItem = null;
                            }
                        }
                    }
                }

                //Draws sigils inventory
                else if (displaySigils)
                {
                    slotRect = new Rect(x * slotWidth + Screen.width * inventoryGridUpperLeftAnchorX, y * slotHeight + Screen.height * inventoryGridUpperLeftAnchorY, slotWidth, slotHeight);
                    GUI.Box(slotRect, "");
                    if (i < items.Count)
                    {
                        slots[i] = sigils[i];
                    }
                    if (slots[i].itemName != null)
                    {
                        GUI.DrawTexture(slotRect, slots[i].icon);
                        if (slots[i].itemName != null)
                        {
                            GUI.DrawTexture(slotRect, slots[i].icon);
                            if (slotRect.Contains(Event.current.mousePosition))
                            {
                                GUI.Box(infoRect, DisplayInfo(slots[i].itemID), guiStyle);
                                containsInfo = true;
                                if (e.button == 0 && e.type == EventType.mouseDrag && !draggingItem)
                                {
                                    draggingItem = true;
                                    prevIndex = i;
                                    draggedItem = slots[i];
                                    sigils[i] = new Item();
                                }
                                if (e.type == EventType.mouseUp && draggingItem && draggedItem.itemType == Item.ItemType.sigil)
                                {
                                    switch (prevIndex)
                                    {
                                        case 1001:
                                            playersEquipment.GetComponent<Equipment>().activeSigil1 = sigils[i];
                                            sigils[i] = draggedItem;
                                            draggingItem = false;
                                            draggedItem = null;
                                            break;
                                        case 1002:
                                            playersEquipment.GetComponent<Equipment>().activeSigil2 = sigils[i];
                                            sigils[i] = draggedItem;
                                            draggingItem = false;
                                            draggedItem = null;
                                            break;
                                        case 1003:
                                            playersEquipment.GetComponent<Equipment>().activeSigil3 = sigils[i];
                                            sigils[i] = draggedItem;
                                            draggingItem = false;
                                            draggedItem = null;
                                            break;
                                        case 1004:
                                            playersEquipment.GetComponent<Equipment>().activeSigil4 = sigils[i];
                                            sigils[i] = draggedItem;
                                            draggingItem = false;
                                            draggedItem = null;
                                            break;
                                        case 1005:
                                            playersEquipment.GetComponent<Equipment>().passiveSigil1 = sigils[i];
                                            sigils[i] = draggedItem;
                                            draggingItem = false;
                                            draggedItem = null;
                                            break;
                                        case 1006:
                                            playersEquipment.GetComponent<Equipment>().passiveSigil2 = sigils[i];
                                            sigils[i] = draggedItem;
                                            draggingItem = false;
                                            draggedItem = null;
                                            break;
                                        case 1007:
                                            playersEquipment.GetComponent<Equipment>().passiveSigil3 = sigils[i];
                                            sigils[i] = draggedItem;
                                            draggingItem = false;
                                            draggedItem = null;
                                            break;
                                        default:
                                            sigils[prevIndex] = sigils[i];
                                            sigils[i] = draggedItem;
                                            draggingItem = false;
                                            draggedItem = null;
                                            break;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (slotRect.Contains(e.mousePosition))
                        {
                            if (e.type == EventType.mouseUp && draggingItem && draggedItem.itemType == Item.ItemType.sigil)
                            {
                                sigils[i] = draggedItem;
                                draggingItem = false;
                                draggedItem = null;
                            }
                        }
                    }
                }

                //Draws consumables inventory
                else if (displayConsumables)
                {
                    slotRect = new Rect(x * slotWidth + Screen.width * inventoryGridUpperLeftAnchorX, y * slotHeight + Screen.height * inventoryGridUpperLeftAnchorY, slotWidth, slotHeight);
                    GUI.Box(slotRect, "");
                    if (i < items.Count)
                    {
                        slots[i] = consumables[i];
                    }
                    if (slots[i].itemName != null)
                    {
                        GUI.DrawTexture(slotRect, slots[i].icon);
                        if (slots[i].itemName != null)
                        {
                            GUI.DrawTexture(slotRect, slots[i].icon);
                            if (slotRect.Contains(Event.current.mousePosition))
                            {
                                GUI.Box(infoRect, DisplayInfo(slots[i].itemID), guiStyle);
                                containsInfo = true;
                                if (e.button == 0 && e.type == EventType.mouseDrag && !draggingItem)
                                {
                                    draggingItem = true;
                                    prevIndex = i;
                                    draggedItem = slots[i];
                                    consumables[i] = new Item();
                                }
                                if (e.type == EventType.mouseUp && draggingItem && draggedItem.itemType == Item.ItemType.consumable)
                                {
                                    switch (prevIndex)
                                    {
                                        case 1009:
                                            playersEquipment.GetComponent<Equipment>().healthPotions = consumables[i];
                                            consumables[i] = draggedItem;
                                            draggingItem = false;
                                            draggedItem = null;
                                            break;
                                        case 1012:
                                            playersEquipment.GetComponent<Equipment>().sigilPotions = consumables[i];
                                            consumables[i] = draggedItem;
                                            draggingItem = false;
                                            draggedItem = null;
                                            break;
                                        default:
                                            consumables[prevIndex] = consumables[i];
                                            consumables[i] = draggedItem;
                                            draggingItem = false;
                                            draggedItem = null;
                                            break;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (slotRect.Contains(e.mousePosition))
                        {
                            if (e.type == EventType.mouseUp && draggingItem && draggedItem.itemType == Item.ItemType.consumable)
                            {
                                consumables[i] = draggedItem;
                                draggingItem = false;
                                draggedItem = null;
                            }
                        }
                    }
                }

                //Draws key items inventory
                else if (displayKeyItems)
                {
                    slotRect = new Rect(x * slotWidth + Screen.width * inventoryGridUpperLeftAnchorX, y * slotHeight + Screen.height * inventoryGridUpperLeftAnchorY, slotWidth, slotHeight);
                    GUI.Box(slotRect, "");
                    if (i < items.Count)
                    {
                        slots[i] = keyItems[i];
                    }
                    if (slots[i].itemName != null)
                    {
                        GUI.DrawTexture(slotRect, slots[i].icon);
                        if (slots[i].itemName != null)
                        {
                            GUI.DrawTexture(slotRect, slots[i].icon);
                            if (slotRect.Contains(Event.current.mousePosition))
                            {
                                GUI.Box(infoRect, DisplayInfo(slots[i].itemID), guiStyle);
                                containsInfo = true;
                                if (e.button == 0 && e.type == EventType.mouseDrag && !draggingItem)
                                {
                                    draggingItem = true;
                                    prevIndex = i;
                                    draggedItem = slots[i];
                                    keyItems[i] = new Item();
                                }
                                if (e.type == EventType.mouseUp && draggingItem && draggedItem.itemType == Item.ItemType.keyItem)
                                {
                                    switch (prevIndex)
                                    {
                                        default:
                                            keyItems[prevIndex] = keyItems[i];
                                            keyItems[i] = draggedItem;
                                            draggingItem = false;
                                            draggedItem = null;
                                            break;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if(slotRect.Contains(e.mousePosition))
                        {
                            if(e.type == EventType.mouseUp && draggingItem && draggedItem.itemType == Item.ItemType.keyItem)
                            {
                                keyItems[i] = draggedItem;
                                draggingItem = false;
                                draggedItem = null;
                            }
                        }
                    }
                }
                i++;
            }
        }

        //Only draws info box when no item is hovered over
        if (!containsInfo)
            GUI.Box(infoRect, "");

        containsInfo = false;
    }

    string DisplayInfo(int itemID)
    {
        string info = "";
        info += itemDatabase.GetComponent<ItemDatabase>().items[itemID].itemName;
        info += "\n";
        info += itemDatabase.GetComponent<ItemDatabase>().items[itemID].itemDesc;
        return info;
    }

    int[] FindItem(int itemID)
    {
        int[] itemLocations = new int[5];

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].itemID == itemID)
            {
                itemLocations[0] = i;
                break;
            }
            else
            {
                itemLocations[0] = -1;
            }
        }

        for (int i = 0; i < equipment.Count; i++)
        {
            if (equipment[i].itemID == itemID)
            {
                itemLocations[1] = i;
                break;
            }
            else
            {
                itemLocations[0] = -1;
            }
        }

        for (int i = 0; i < sigils.Count; i++)
        {
            if (sigils[i].itemID == itemID)
            {
                itemLocations[2] = i;
                break;
            }
            else
            {
                itemLocations[0] = -1;
            }
        }

        for (int i = 0; i < consumables.Count; i++)
        {
            if (consumables[i].itemID == itemID)
            {
                itemLocations[3] = i;
                break;
            }
            else
            {
                itemLocations[0] = -1;
            }
        }

        for (int i = 0; i < keyItems.Count; i++)
        {
            if (keyItems[i].itemID == itemID)
            {
                itemLocations[4] = i;
                break;
            }
            else
            {
                itemLocations[0] = -1;
            }
        }

        return itemLocations;
    }

    //Called by ItemDrop.cs, adds an item the inventory when the player picks up an item
    //Stores item in correct section (All and one of the four other specific categories)
    public void AddItemFromDrop(int itemID)
    {
        Item newItem = itemDatabase.GetComponent<ItemDatabase>().items[itemID];

        for(int i = 0; i < items.Count; i++)
        {
            if(items[i].itemName == null)
            {
                items[i] = newItem;
                break;
            }
        }

        items.Add(newItem);

        switch(newItem.itemType)
        {
            case Item.ItemType.weapon:
            case Item.ItemType.armor:
            case Item.ItemType.shield:
                for (int i = 0; i < equipment.Count; i++)
                {
                    if (equipment[i].itemName == null)
                    {
                        equipment[i] = newItem;
                        break;
                    }
                }
                break;
            case Item.ItemType.sigil:
                for (int i = 0; i < sigils.Count; i++)
                {
                    if (sigils[i].itemName == null)
                    {
                        sigils[i] = newItem;
                        break;
                    }
                }
                break;
            case Item.ItemType.consumable:
                for (int i = 0; i < consumables.Count; i++)
                {
                    if (consumables[i].itemName == null)
                    {
                        consumables[i] = newItem;
                        break;
                    }
                }
                break;
            case Item.ItemType.keyItem:
                for (int i = 0; i < keyItems.Count; i++)
                {
                    if (keyItems[i].itemName == null)
                    {
                        keyItems[i] = newItem;
                        break;
                    }
                }
                break;
        }
    }
}
