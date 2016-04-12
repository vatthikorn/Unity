/*
    Nathan Cruz

    Inventory for all the items the player has. Manipulates items by picking up objects from the screen, dragging and dropping in the inventory screen.
    
    Holds items.
    Items can be dragged to be repositioned.
    Items can be rearranged.
    Inventory can be expanded when needed.
    Inventory is scrollable when needed.
    Items can be switched from invetory and equipment on the fly.
    
    Instructions (on how to set up the image for the item slot):
    create a GUISkin (name it and remember the name)
    Go under custom style of GUISkin
    Go under normal of custom style
    Go to background under normal, and set up the source image to be the image you are using
    Slice the image. This will make sure it only scales borders, and not make the slot image look nasty
    In the code: In DrawInventory when drawing the slots: new Rect (x, null, skin.GetStyle("StyleName")) // where the "StyleName" is the name of the GUISkin you are using
    Make sure to reference the skin as well!
    
    Interface:
    void AddItemFromDrop(int itemID) - to add items to the invetory (for ItemDrop.cs)
    bool Find(int itemID) - test if the item is in the inventory
    void OthersRemoveItem(int itemID) - remove the item from the inventory

    Dependency:
    Item.cs - information access (eveything*)
    ItemDatabase.cs - access information on items (eveything*)
    PlayerController.cs - to check if the player is on the inventory screen (screenState)
    Equipment.cs - manipulate the player's equipment (eveything*)

    Required:
    Attached to a gameobject "Inventory".
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

    //Skins for slots, player pic, info slot, overall Inventory
    public GUISkin slotSkin;
    public GUISkin inventorySkin;
    public GUISkin playerSkin;
    public GUISkin infoSkin;

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
    int otherIndex;

    //Used to scroll through the inventory
    int iOffSet = 0;

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
        //Draws inventory screen
        if (player.GetComponent<PlayerController>().screenState == PlayerController.ScreenState.inventory)
        {
            DrawInventoryScreen();
        }

        //Draws item on mouse when player is holding it
        if(draggingItem)
        {
            GUI.DrawTexture(new Rect(Event.current.mousePosition.x - slotWidth/2, Event.current.mousePosition.y - slotHeight/2, slotWidth, slotHeight), draggedItem.icon);
        }

        //Player closes inventory screen while holding an items, puts that thing back where it came from or so help me god
        if(draggingItem && player.GetComponent<PlayerController>().screenState != PlayerController.ScreenState.inventory)
        {
            switch (prevIndex)
            {
                case 1001:
                    playersEquipment.GetComponent<Equipment>().activeSigil1 = draggedItem;
                    draggingItem = false;
                    draggedItem = null;
                    break;
                case 1002:
                    playersEquipment.GetComponent<Equipment>().activeSigil2 = draggedItem;
                    draggingItem = false;
                    draggedItem = null;
                    break;
                case 1003:
                    playersEquipment.GetComponent<Equipment>().activeSigil3 = draggedItem;
                    draggingItem = false;
                    draggedItem = null;
                    break;
                case 1004:
                    playersEquipment.GetComponent<Equipment>().activeSigil4 = draggedItem;
                    draggingItem = false;
                    draggedItem = null;
                    break;
                case 1005:
                    playersEquipment.GetComponent<Equipment>().passiveSigil1 = draggedItem;
                    draggingItem = false;
                    draggedItem = null;
                    break;
                case 1006:
                    playersEquipment.GetComponent<Equipment>().passiveSigil2 = draggedItem;
                    draggingItem = false;
                    draggedItem = null;
                    break;
                case 1007:
                    playersEquipment.GetComponent<Equipment>().passiveSigil3 = draggedItem;
                    draggingItem = false;
                    draggedItem = null;
                    break;
                case 1008:
                    playersEquipment.GetComponent<Equipment>().weapon = draggedItem;
                    draggingItem = false;
                    draggedItem = null;
                    break;
                case 1009:
                    playersEquipment.GetComponent<Equipment>().healthPotions = draggedItem;
                    draggingItem = false;
                    draggedItem = null;
                    break;
                case 1010:
                    playersEquipment.GetComponent<Equipment>().armor = draggedItem;
                    draggingItem = false;
                    draggedItem = null;
                    break;
                case 1011:
                    playersEquipment.GetComponent<Equipment>().shield = draggedItem;
                    draggingItem = false;
                    draggedItem = null;
                    break;
                case 1012:
                    playersEquipment.GetComponent<Equipment>().sigilPotions = draggedItem;
                    draggingItem = false;
                    draggedItem = null;
                    break;
                default:
                    if(displayAll)
                    {
                        AddItem(draggedItem, 'a');
                        items[prevIndex] = draggedItem;
                    }
                    else if(displayEquipment)
                    {
                        AddItem(draggedItem, 'e');
                        equipment[prevIndex] = draggedItem;
                    }
                    else if(displaySigils)
                    {
                        AddItem(draggedItem, 's');
                        sigils[prevIndex] = draggedItem;
                    }
                    else if(displayConsumables)
                    {
                        AddItem(draggedItem, 'c');
                        consumables[prevIndex] = draggedItem;
                    }
                    else if(displayKeyItems)
                    {
                        AddItem(draggedItem, 'k');
                        keyItems[prevIndex] = draggedItem;
                    }
                    draggingItem = false;
                    draggedItem = null;
                    break;
            }
        }
    }

    //Draws the entire Inventory Screen
    void DrawInventoryScreen()
    {
        Event e = Event.current;
        int i = 0 + iOffSet;//for drawing the slots

        GUI.skin = infoSkin;
        //Style for Info Box
        GUIStyle guiStyle = GUI.skin.box;
        guiStyle.wordWrap = true;
        guiStyle.alignment = TextAnchor.UpperLeft;
        
        //Switches to inventory skin
        GUI.skin = inventorySkin;

        //Draws the entire panel
        GUI.Box(new Rect(Screen.width * screenUpperLeftAnchorX, Screen.height * screenUpperLeftAnchorY, Screen.width * inventoryWidth, Screen.height * inventoryHeight), "");

        //Switches skin to slot skin
        GUI.skin = slotSkin;

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

        //Draws Buttons, and Switches tabs (disables tab switching when player is dragging item)
        Rect buttonRect = new Rect(Screen.width * inventoryGridUpperLeftAnchorX, Screen.height * inventoryGridUpperLeftAnchorY - tabHeight, tabWidth, tabHeight);
        if (GUI.Button(buttonRect, "All") && !draggingItem)
        {
            iOffSet = 0;
            displayAll = true;
            displayEquipment = false;
            displaySigils = false;
            displayConsumables = false;
            displayKeyItems = false;
        }
        buttonRect = new Rect(Screen.width * inventoryGridUpperLeftAnchorX + 95, Screen.height * inventoryGridUpperLeftAnchorY - tabHeight, tabWidth, tabHeight);
        if (GUI.Button(buttonRect, "Equipment") && !draggingItem)
        {
            iOffSet = 0;
            displayAll = false;
            displayEquipment = true;
            displaySigils = false;
            displayConsumables = false;
            displayKeyItems = false;
        }
        buttonRect = new Rect(Screen.width * inventoryGridUpperLeftAnchorX + tabWidth * 2, Screen.height * inventoryGridUpperLeftAnchorY - tabHeight, tabWidth, tabHeight);
        if (GUI.Button(buttonRect, "Sigils") && !draggingItem)
        {
            iOffSet = 0;
            displayAll = false;
            displayEquipment = false;
            displaySigils = true;
            displayConsumables = false;
            displayKeyItems = false;
        }
        buttonRect = new Rect(Screen.width * inventoryGridUpperLeftAnchorX + tabWidth * 3, Screen.height * inventoryGridUpperLeftAnchorY - tabHeight, tabWidth, tabHeight);
        if (GUI.Button(buttonRect, "Consumables") && !draggingItem)
        {
            iOffSet = 0;
            displayAll = false;
            displayEquipment = false;
            displaySigils = false;
            displayConsumables = true;
            displayKeyItems = false;
        }
        buttonRect = new Rect(Screen.width * inventoryGridUpperLeftAnchorX + tabWidth * 4, Screen.height * inventoryGridUpperLeftAnchorY - tabHeight, tabWidth, tabHeight);
        if (GUI.Button(buttonRect, "Key Items") && !draggingItem)
        {
            iOffSet = 0;
            displayAll = false;
            displayEquipment = false;
            displaySigils = false;
            displayConsumables = false;
            displayKeyItems = true;
        }

        //Enables buttons to move up and down inventory screen, when player becomes a hoarder
        if(displayAll)
        {
            buttonRect = new Rect(Screen.width * inventoryGridUpperLeftAnchorX + slotWidth * slotsX, Screen.height * inventoryGridUpperLeftAnchorY, 20, 20);
            if (iOffSet < items.Count - 50 && GUI.Button(buttonRect, ""))
            {
                iOffSet += slotsX;
            }
            buttonRect = new Rect(Screen.width * inventoryGridUpperLeftAnchorX + slotWidth * slotsX, Screen.height * inventoryGridUpperLeftAnchorY + slotHeight * slotsY - 20, 20, 20);
            if (iOffSet > 0 && GUI.Button(buttonRect, ""))
            {
                iOffSet -= slotsX;
            }
        }
        else if(displayEquipment)
        {
            buttonRect = new Rect(Screen.width * inventoryGridUpperLeftAnchorX + slotWidth * slotsX, Screen.height * inventoryGridUpperLeftAnchorY, 20, 20);
            if (iOffSet < equipment.Count - 50 && GUI.Button(buttonRect, ""))
            {
                iOffSet += slotsX;
            }
            buttonRect = new Rect(Screen.width * inventoryGridUpperLeftAnchorX + slotWidth * slotsX, Screen.height * inventoryGridUpperLeftAnchorY + slotHeight * slotsY - 20, 20, 20);
            if (iOffSet > 0 && GUI.Button(buttonRect, ""))
            {
                iOffSet -= slotsX;
            }
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
                                    RemoveItem(draggedItem, 'a');
                                }
                                if (e.type == EventType.mouseUp && draggingItem)
                                {
                                    switch (prevIndex)
                                    {
                                        case 1001:
                                            AddItem(draggedItem, 'a');
                                            playersEquipment.GetComponent<Equipment>().activeSigil1 = items[i];
                                            items[i] = draggedItem;
                                            draggingItem = false;
                                            draggedItem = null;
                                            break;
                                        case 1002:
                                            AddItem(draggedItem, 'a');
                                            playersEquipment.GetComponent<Equipment>().activeSigil2 = items[i];
                                            items[i] = draggedItem;
                                            draggingItem = false;
                                            draggedItem = null;
                                            break;
                                        case 1003:
                                            AddItem(draggedItem, 'a');
                                            playersEquipment.GetComponent<Equipment>().activeSigil3 = items[i];
                                            items[i] = draggedItem;
                                            draggingItem = false;
                                            draggedItem = null;
                                            break;
                                        case 1004:
                                            AddItem(draggedItem, 'a');
                                            playersEquipment.GetComponent<Equipment>().activeSigil4 = items[i];
                                            items[i] = draggedItem;
                                            draggingItem = false;
                                            draggedItem = null;
                                            break;
                                        case 1005:
                                            AddItem(draggedItem, 'a');
                                            playersEquipment.GetComponent<Equipment>().passiveSigil1 = items[i];
                                            items[i] = draggedItem;
                                            draggingItem = false;
                                            draggedItem = null;
                                            break;
                                        case 1006:
                                            AddItem(draggedItem, 'a');
                                            playersEquipment.GetComponent<Equipment>().passiveSigil2 = items[i];
                                            items[i] = draggedItem;
                                            draggingItem = false;
                                            draggedItem = null;
                                            break;
                                        case 1007:
                                            AddItem(draggedItem, 'a');
                                            playersEquipment.GetComponent<Equipment>().passiveSigil3 = items[i];
                                            items[i] = draggedItem;
                                            draggingItem = false;
                                            draggedItem = null;
                                            break;
                                        case 1008:
                                            AddItem(draggedItem, 'a');
                                            playersEquipment.GetComponent<Equipment>().weapon = items[i];
                                            items[i] = draggedItem;
                                            draggingItem = false;
                                            draggedItem = null;
                                            break;
                                        case 1009:
                                            AddItem(draggedItem, 'a');
                                            playersEquipment.GetComponent<Equipment>().healthPotions = items[i];
                                            items[i] = draggedItem;
                                            draggingItem = false;
                                            draggedItem = null;
                                            break;
                                        case 1010:
                                            AddItem(draggedItem, 'a');
                                            playersEquipment.GetComponent<Equipment>().armor = items[i];
                                            items[i] = draggedItem;
                                            draggingItem = false;
                                            draggedItem = null;
                                            break;
                                        case 1011:
                                            AddItem(draggedItem, 'a');
                                            playersEquipment.GetComponent<Equipment>().shield = items[i];
                                            items[i] = draggedItem;
                                            draggingItem = false;
                                            draggedItem = null;
                                            break;
                                        case 1012:
                                            AddItem(draggedItem, 'a');
                                            playersEquipment.GetComponent<Equipment>().sigilPotions = items[i];
                                            items[i] = draggedItem;
                                            draggingItem = false;
                                            draggedItem = null;
                                            break;
                                        default:
                                            switch (draggedItem.itemType)
                                            {
                                                case Item.ItemType.weapon:
                                                case Item.ItemType.armor:
                                                case Item.ItemType.shield:
                                                    equipment[otherIndex] = draggedItem;
                                                    break;
                                                case Item.ItemType.sigil:
                                                    sigils[otherIndex] = draggedItem;
                                                    break;
                                                case Item.ItemType.consumable:
                                                    consumables[otherIndex] = draggedItem;
                                                    break;
                                                case Item.ItemType.keyItem:
                                                    keyItems[otherIndex] = draggedItem;
                                                    break;
                                            }
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
                                if (prevIndex < 1001)
                                {
                                    switch (draggedItem.itemType)
                                    {
                                        case Item.ItemType.weapon:
                                        case Item.ItemType.armor:
                                        case Item.ItemType.shield:
                                            equipment[otherIndex] = draggedItem;
                                            break;
                                        case Item.ItemType.sigil:
                                            sigils[otherIndex] = draggedItem;
                                            break;
                                        case Item.ItemType.consumable:
                                            consumables[otherIndex] = draggedItem;
                                            break;
                                        case Item.ItemType.keyItem:
                                            keyItems[otherIndex] = draggedItem;
                                            break;
                                    }
                                }
                                else
                                    AddItem(draggedItem, 'a');
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
                                    RemoveItem(draggedItem, 'e');
                                }
                                if (e.type == EventType.mouseUp && draggingItem && (draggedItem.itemType == Item.ItemType.weapon || draggedItem.itemType == Item.ItemType.armor || draggedItem.itemType == Item.ItemType.shield))
                                {
                                    switch (prevIndex)
                                    {
                                        case 1008:
                                            AddItem(draggedItem, 'e');
                                            playersEquipment.GetComponent<Equipment>().weapon = equipment[i];
                                            equipment[i] = draggedItem;
                                            draggingItem = false;
                                            draggedItem = null;
                                            break;
                                        case 1010:
                                            AddItem(draggedItem, 'e');
                                            playersEquipment.GetComponent<Equipment>().armor = equipment[i];
                                            equipment[i] = draggedItem;
                                            draggingItem = false;
                                            draggedItem = null;
                                            break;
                                        case 1011:
                                            AddItem(draggedItem, 'e');
                                            playersEquipment.GetComponent<Equipment>().shield = equipment[i];
                                            equipment[i] = draggedItem;
                                            draggingItem = false;
                                            draggedItem = null;
                                            break;
                                        default:
                                            items[otherIndex] = draggedItem;
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
                                if (prevIndex < 1001)
                                    items[otherIndex] = draggedItem;
                                else
                                    AddItem(draggedItem, 'e');
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
                                    RemoveItem(draggedItem, 's');
                                }
                                if (e.type == EventType.mouseUp && draggingItem && draggedItem.itemType == Item.ItemType.sigil)
                                {
                                    switch (prevIndex)
                                    {
                                        case 1001:
                                            AddItem(draggedItem, 's');
                                            playersEquipment.GetComponent<Equipment>().activeSigil1 = sigils[i];
                                            sigils[i] = draggedItem;
                                            draggingItem = false;
                                            draggedItem = null;
                                            break;
                                        case 1002:
                                            AddItem(draggedItem, 's');
                                            playersEquipment.GetComponent<Equipment>().activeSigil2 = sigils[i];
                                            sigils[i] = draggedItem;
                                            draggingItem = false;
                                            draggedItem = null;
                                            break;
                                        case 1003:
                                            AddItem(draggedItem, 's');
                                            playersEquipment.GetComponent<Equipment>().activeSigil3 = sigils[i];
                                            sigils[i] = draggedItem;
                                            draggingItem = false;
                                            draggedItem = null;
                                            break;
                                        case 1004:
                                            AddItem(draggedItem, 's');
                                            playersEquipment.GetComponent<Equipment>().activeSigil4 = sigils[i];
                                            sigils[i] = draggedItem;
                                            draggingItem = false;
                                            draggedItem = null;
                                            break;
                                        case 1005:
                                            AddItem(draggedItem, 's');
                                            playersEquipment.GetComponent<Equipment>().passiveSigil1 = sigils[i];
                                            sigils[i] = draggedItem;
                                            draggingItem = false;
                                            draggedItem = null;
                                            break;
                                        case 1006:
                                            AddItem(draggedItem, 's');
                                            playersEquipment.GetComponent<Equipment>().passiveSigil2 = sigils[i];
                                            sigils[i] = draggedItem;
                                            draggingItem = false;
                                            draggedItem = null;
                                            break;
                                        case 1007:
                                            AddItem(draggedItem, 's');
                                            playersEquipment.GetComponent<Equipment>().passiveSigil3 = sigils[i];
                                            sigils[i] = draggedItem;
                                            draggingItem = false;
                                            draggedItem = null;
                                            break;
                                        default:
                                            items[otherIndex] = draggedItem;
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
                                if (prevIndex < 1001)
                                    items[otherIndex] = draggedItem;
                                else
                                    AddItem(draggedItem, 's');
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
                                    RemoveItem(draggedItem, 'c');
                                }
                                if (e.type == EventType.mouseUp && draggingItem && draggedItem.itemType == Item.ItemType.consumable)
                                {
                                    switch (prevIndex)
                                    {
                                        case 1009:
                                            AddItem(draggedItem, 'c');
                                            playersEquipment.GetComponent<Equipment>().healthPotions = consumables[i];
                                            consumables[i] = draggedItem;
                                            draggingItem = false;
                                            draggedItem = null;
                                            break;
                                        case 1012:
                                            AddItem(draggedItem, 'c');
                                            playersEquipment.GetComponent<Equipment>().sigilPotions = consumables[i];
                                            consumables[i] = draggedItem;
                                            draggingItem = false;
                                            draggedItem = null;
                                            break;
                                        default:
                                            items[otherIndex] = draggedItem;
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
                                if (prevIndex < 1001)
                                    items[otherIndex] = draggedItem;
                                else
                                    AddItem(draggedItem, 'c');
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
                                    RemoveItem(draggedItem, 'a');
                                }
                                if (e.type == EventType.mouseUp && draggingItem && draggedItem.itemType == Item.ItemType.keyItem)
                                {
                                    switch (prevIndex)
                                    {
                                        default:
                                            items[otherIndex] = draggedItem;
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
                                if(prevIndex < 1001)
                                    items[otherIndex] = draggedItem;
                                else
                                    AddItem(draggedItem, 'k');
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

    //Displays stats as well
    string DisplayInfo(int itemID)
    {
        string info = "";
        info += itemDatabase.GetComponent<ItemDatabase>().items[itemID].itemName;
        info += "\n";
        info += itemDatabase.GetComponent<ItemDatabase>().items[itemID].itemDesc;
        switch(itemDatabase.GetComponent<ItemDatabase>().items[itemID].itemType)
        {
            case Item.ItemType.weapon:
                info += "\n";
                info += "Strength: ";
                info += itemDatabase.GetComponent<ItemDatabase>().items[itemID].damage;
                info += "\t";
                info += "Speed: ";
                info += itemDatabase.GetComponent<ItemDatabase>().items[itemID].attackSpeed;
                info += "\t";
                info += "Critical Chance: ";
                info += itemDatabase.GetComponent<ItemDatabase>().items[itemID].criticalChance;
                info += "\n";
                info += "Range: ";
                if (itemDatabase.GetComponent<ItemDatabase>().items[itemID].range == Item.Range.longs)
                    info += "long";
                else 
                    info += itemDatabase.GetComponent<ItemDatabase>().items[itemID].range;
                info += "\t";
                info += "Knockback: ";
                info += itemDatabase.GetComponent<ItemDatabase>().items[itemID].knockback;
                break;
            case Item.ItemType.armor:
                info += "\n";
                info += "Defense: ";
                info += itemDatabase.GetComponent<ItemDatabase>().items[itemID].defense;
                break;
            case Item.ItemType.shield:
                info += "\n";
                info += "Damage Mitigation: ";
                info += (int) 100*itemDatabase.GetComponent<ItemDatabase>().items[itemID].damageMitigation;
                break;
        }
        return info;
    }

    //Updates other lists when an item added to one
    void AddItem(Item draggedItem, char c)
    {
        switch (c)
        {
            case 'a':
                switch (itemDatabase.GetComponent<ItemDatabase>().items[draggedItem.itemID].itemType)
                {
                    case Item.ItemType.weapon:
                    case Item.ItemType.armor:
                    case Item.ItemType.shield:
                        for (int i = 0; i < equipment.Count; i++)
                        {
                            if (equipment[i].itemName == null)
                            {
                                equipment[i] = draggedItem;
                                break;
                            }
                        }
                        break;
                    case Item.ItemType.sigil:
                        for (int i = 0; i < sigils.Count; i++)
                        {
                            if (sigils[i].itemName == null)
                            {
                                sigils[i] = draggedItem;
                                break;
                            }
                        }
                        break;
                    case Item.ItemType.consumable:
                        for (int i = 0; i < consumables.Count; i++)
                        {
                            if (consumables[i].itemName == null)
                            {
                                consumables[i] = draggedItem;
                                break;
                            }
                        }
                        break;
                    case Item.ItemType.keyItem:
                        for (int i = 0; i < keyItems.Count; i++)
                        {
                            if (keyItems[i].itemName == null)
                            {
                                keyItems[i] = draggedItem;
                                break;
                            }
                        }
                        break;
                }
                break;
            case 'e':
            case 's':
            case 'c':
            case 'k':
                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i].itemName == null)
                    {
                        items[i] = draggedItem;
                        break;
                    }
                }
                break;
        }
    }


    //Removes Item from other lists too
    void RemoveItem(Item item, char c)
    {
        switch (c)
        {
            case 'a':
                switch(item.itemType)
                {
                    case Item.ItemType.weapon:
                    case Item.ItemType.armor:
                    case Item.ItemType.shield:
                        for (int i = 0; i < equipment.Count; i++)
                        {
                            if (equipment[i].itemName == item.itemName)
                            {
                                equipment[i] = new Item();
                                otherIndex = i;
                                break;
                            }
                        }
                        break;
                    case Item.ItemType.sigil:
                        for (int i = 0; i < sigils.Count; i++)
                        {
                            if (sigils[i].itemName == item.itemName)
                            {
                                sigils[i] = new Item();
                                otherIndex = i;
                                break;
                            }
                        }
                        break;
                    case Item.ItemType.consumable:
                        for (int i = 0; i < consumables.Count; i++)
                        {
                            if (consumables[i].itemName == item.itemName)
                            {
                                consumables[i] = new Item();
                                otherIndex = i;
                                break;
                            }
                        }
                        break;
                    case Item.ItemType.keyItem:
                        for (int i = 0; i < keyItems.Count; i++)
                        {
                            if (keyItems[i].itemName == item.itemName)
                            {
                                keyItems[i] = new Item();
                                otherIndex = i;
                                break;
                            }
                        }
                        break;
                }
                break;
            case 'e':
            case 's':
            case 'c':
            case 'k':
                for(int i = 0; i < items.Count; i++)
                {
                    if (items[i].itemName == item.itemName)
                    {
                        items[i] = new Item();
                        otherIndex = i;
                        break;
                    }
                }
                break;
        }
    }

    //Called by ItemDrop.cs, adds an item the inventory when the player picks up an item
    //Stores item in correct section (All and one of the four other specific categories)
    //Exapnds inventory when needed
    public void AddItemFromDrop(int itemID)
    {
        Item newItem = itemDatabase.GetComponent<ItemDatabase>().items[itemID];

        bool needMoreSpace = true;
        for(int i = 0; i < items.Count; i++)
        {
            if(items[i].itemName == null)
            {
                items[i] = newItem;
                needMoreSpace = false;
                break;
            }
        }
        if(needMoreSpace)
        {
            items.Add(newItem);
            for(int i = 0; i < slotsX - 1; i++)
            {
                items.Add(new Item());
            }
            needMoreSpace = false;
        }

        switch(newItem.itemType)
        {
            case Item.ItemType.weapon:
            case Item.ItemType.armor:
            case Item.ItemType.shield:
                needMoreSpace = true;
                for (int i = 0; i < equipment.Count; i++)
                {
                    if (equipment[i].itemName == null)
                    {
                        equipment[i] = newItem;
                        needMoreSpace = false;
                        break;
                    }
                }
                if (needMoreSpace)
                {
                    equipment.Add(newItem);
                    for (int i = 0; i < slotsX - 1; i++)
                    {
                        equipment.Add(new Item());
                    }
                    needMoreSpace = false;
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
                needMoreSpace = true;
                for (int i = 0; i < consumables.Count; i++)
                {
                    if (consumables[i].itemName == null)
                    {
                        consumables[i] = newItem;
                        needMoreSpace = false;
                        break;
                    }
                }
                if (needMoreSpace)
                {
                    consumables.Add(newItem);
                    for (int i = 0; i < slotsX - 1; i++)
                    {
                        consumables.Add(new Item());
                    }
                    needMoreSpace = false;
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

        //Expands slots when needed
        if(slots.Count < items.Count)
        {
            for(int i = 0; i < slotsX; i++)
            {
                slots.Add(new Item());
            }
        }
    }

    //Removes the first item it fines of the same name
    public void OthersRemoveItem(int itemID)
    {
        for(int i = 0; i < items.Count; i++)
        {
            if (items[i].itemName == itemDatabase.GetComponent<ItemDatabase>().items[itemID].itemName)
            {
                items[i] = new Item();
                RemoveItem(items[i], 'a');
                break;
            }
        }
    }

    //Finds the item
    public bool Find(int itemID)
    {
        for(int i = 0; i < items.Count; i++)
        {
            if(items[i].itemName == itemDatabase.GetComponent<ItemDatabase>().items[itemID].itemName)
            {
                return true;
            }
        }

        return false;
    }
}
