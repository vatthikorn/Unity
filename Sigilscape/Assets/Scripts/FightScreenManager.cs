/*
    Nathan Cruz

    Display health, items and sigils equipped
    Display cooldown for items and sigils equipped
    Handles healing the player
    Handles resetting the cooldowns when sigil potions are used
*/
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class FightScreenManager : MonoBehaviour {

    public GameObject player;
    public GameObject equipment;

    public GUISkin shadeSkin;
    public GUISkin healthSkin;
    public GUISkin healthBarSkin;
    public GUISkin skin;

    //Sigil Icon's Positions
    public const int SigilY = 25;

    public const int Sigil1X = 225;
    public const int Sigil2X = 300;
    public const int Sigil3X = 375;
    public const int Sigil4X = 450;

    //Potions Icon's Positions
    public const int healthX = 25;
    public const int healthY = 85;
    public const int sigilPotionX = 100;
    public const int sigilPotionY = 85;

    //Health Bar
    public const int healthWidth = 173;
    public const int healthHeight = 48;
    public const int healthBarX = 27;
    public const int healthBarY = 27;

    //Slot Size
    public const int slotHeight = 50;
    public const int slotWidth = 50;
    public const int indicHeight = 20;
    public const int indicWidth = 20;

    void OnGUI()
    {
        if (player.GetComponent<PlayerController>().screenState == PlayerController.ScreenState.fight)
        {
            DrawFightGUI();
        }
    }

    void CoolCounterStyle()
    {
        GUI.skin.box.fontSize = 28;
        GUI.skin.box.fontStyle = FontStyle.Bold;
        GUI.skin.box.alignment = TextAnchor.MiddleCenter;
    }

    void DrawFightGUI()
    {
        equipment.GetComponent<Equipment>().UpdateItemCount();

        //Draws Sigil1 Icon
        GUI.skin = skin;
        Rect slotRect = new Rect(Sigil1X, SigilY, slotWidth, slotHeight);
        GUI.Box(slotRect, "", GUI.skin.GetStyle("On Slots"));
        if(equipment.GetComponent<Equipment>().activeSigil1.itemID != 0)
        {
            GUI.DrawTexture(slotRect, equipment.GetComponent<Equipment>().activeSigil1.icon);
        }

        CoolCounterStyle();
        if (equipment.GetComponent<Equipment>().activeSigil1.itemName != null && equipment.GetComponent<Equipment>().activeSigil1.sigil.GetComponent<Sigil>().coolDown != 0)
        {
            if (equipment.GetComponent<Equipment>().activeSigil1.sigil.GetComponent<Sigil>().timer != 0)
            {
                GUI.Box(new Rect(Sigil1X, SigilY, slotWidth, slotHeight), Math.Round(equipment.GetComponent<Equipment>().activeSigil1.sigil.GetComponent<Sigil>().timer).ToString());
            }
        }

        slotRect = new Rect(Sigil1X + slotWidth/2 - indicWidth/2, SigilY + slotHeight - indicHeight/2, indicWidth, indicHeight);
        GUI.Box(slotRect, "1", GUI.skin.GetStyle("On Slots"));


        //Draws Sigil2 Icon
        slotRect = new Rect(Sigil2X, SigilY, slotWidth, slotHeight);
        GUI.Box(slotRect, "", GUI.skin.GetStyle("On Slots"));
        if (equipment.GetComponent<Equipment>().activeSigil2.itemID != 0)
        {
            GUI.DrawTexture(slotRect, equipment.GetComponent<Equipment>().activeSigil2.icon);
        }

        if (equipment.GetComponent<Equipment>().activeSigil2.itemName != null && equipment.GetComponent<Equipment>().activeSigil2.sigil.GetComponent<Sigil>().coolDown != 0)
        {
            if (equipment.GetComponent<Equipment>().activeSigil2.sigil.GetComponent<Sigil>().timer != 0)
            {
                GUI.Box(new Rect(Sigil2X, SigilY, slotWidth, slotHeight), Math.Round(equipment.GetComponent<Equipment>().activeSigil2.sigil.GetComponent<Sigil>().timer).ToString());
            }
        }

        slotRect = new Rect(Sigil2X + slotWidth / 2 - indicWidth / 2, SigilY + slotHeight - indicHeight / 2, indicWidth, indicHeight);
        GUI.Box(slotRect, "2", GUI.skin.GetStyle("On Slots"));

        //Draws Sigil3 Icon
        slotRect = new Rect(Sigil3X, SigilY, slotWidth, slotHeight);
        GUI.Box(slotRect, "", GUI.skin.GetStyle("On Slots"));
        if (equipment.GetComponent<Equipment>().activeSigil3.itemID != 0)
        {
            GUI.DrawTexture(slotRect, equipment.GetComponent<Equipment>().activeSigil3.icon);
        }

        if (equipment.GetComponent<Equipment>().activeSigil3.itemName != null && equipment.GetComponent<Equipment>().activeSigil3.sigil.GetComponent<Sigil>().coolDown != 0)
        {
            if (equipment.GetComponent<Equipment>().activeSigil3.sigil.GetComponent<Sigil>().timer != 0)
            {
                GUI.Box(new Rect(Sigil3X, SigilY, slotWidth, slotHeight), Math.Round(equipment.GetComponent<Equipment>().activeSigil3.sigil.GetComponent<Sigil>().timer).ToString());
            }
        }

        slotRect = new Rect(Sigil3X + slotWidth / 2 - indicWidth / 2, SigilY + slotHeight - indicHeight / 2, indicWidth, indicHeight);
        GUI.Box(slotRect, "3", GUI.skin.GetStyle("On Slots"));

        //Draws Sigil4 Icon
        slotRect = new Rect(Sigil4X, SigilY, slotWidth, slotHeight);
        GUI.Box(slotRect, "", GUI.skin.GetStyle("On Slots"));
        if (equipment.GetComponent<Equipment>().activeSigil4.itemID != 0)
        {
            GUI.DrawTexture(slotRect, equipment.GetComponent<Equipment>().activeSigil4.icon);
        }

        if (equipment.GetComponent<Equipment>().activeSigil4.itemName != null && equipment.GetComponent<Equipment>().activeSigil4.sigil.GetComponent<Sigil>().coolDown != 0)
        {
            if (equipment.GetComponent<Equipment>().activeSigil4.sigil.GetComponent<Sigil>().timer != 0)
            {
                GUI.Box(new Rect(Sigil4X, SigilY, slotWidth, slotHeight), Math.Round(equipment.GetComponent<Equipment>().activeSigil4.sigil.GetComponent<Sigil>().timer).ToString());
            }
        }

        slotRect = new Rect(Sigil4X + slotWidth / 2 - indicWidth / 2, SigilY + slotHeight - indicHeight / 2, indicWidth, indicHeight);
        GUI.Box(slotRect, "4", GUI.skin.GetStyle("On Slots"));

        //GUI.skin = itemSkin;

        //Draws HealthPotion Icon
        slotRect = new Rect(healthX, healthY, slotWidth, slotHeight);
        GUI.Box(slotRect, "", GUI.skin.GetStyle("On Slots"));
        if (equipment.GetComponent<Equipment>().healthPotions.itemID != 0)
        {
            GUI.DrawTexture(slotRect, equipment.GetComponent<Equipment>().healthPotions.icon);
        }
        slotRect = new Rect(healthX + slotWidth / 2 - indicWidth / 2, healthY + slotHeight - indicHeight / 2, indicWidth, indicHeight);
        GUI.Box(slotRect, "Q", GUI.skin.GetStyle("On Slots"));
        //Potion Counter
        slotRect = new Rect(healthX + slotWidth, healthY + slotHeight/2 - indicHeight/2, indicWidth, indicHeight);
        if(equipment.GetComponent<Equipment>().healthPotionCount < 10)
        {
            GUI.Box(slotRect, equipment.GetComponent<Equipment>().healthPotionCount.ToString(), GUI.skin.GetStyle("On Slots"));
        }
        else
        {
            GUI.Box(slotRect, "+", GUI.skin.GetStyle("On Slots"));
        }
        slotRect = new Rect(healthX + slotWidth, healthY + slotHeight / 2 + indicHeight / 2, indicWidth, indicHeight);
        if(equipment.GetComponent<Equipment>().allHealthPotionCount < 10)
        {
            GUI.Box(slotRect, equipment.GetComponent<Equipment>().allHealthPotionCount.ToString(), GUI.skin.GetStyle("On Slots"));
        }
        else
        {
            GUI.Box(slotRect, "+", GUI.skin.GetStyle("On Slots"));
        }
        

        //Draws SigilPotion Icon
        slotRect = new Rect(sigilPotionX, sigilPotionY, slotWidth, slotHeight);
        GUI.Box(slotRect, "", GUI.skin.GetStyle("On Slots"));
        if (equipment.GetComponent<Equipment>().sigilPotions.itemID != 0)
        {
            GUI.DrawTexture(slotRect, equipment.GetComponent<Equipment>().sigilPotions.icon);
        }
        slotRect = new Rect(sigilPotionX + slotWidth / 2 - indicWidth / 2, sigilPotionY + slotHeight - indicHeight / 2, indicWidth, indicHeight);
        GUI.Box(slotRect, "R", GUI.skin.GetStyle("On Slots"));
        //Sigil Potion Counter
        slotRect = new Rect(sigilPotionX + slotWidth, sigilPotionY + slotHeight / 2 + indicHeight / 2, indicWidth, indicHeight);
        if (equipment.GetComponent<Equipment>().sigilPotionCount < 10)
        {
            GUI.Box(slotRect, equipment.GetComponent<Equipment>().sigilPotionCount.ToString(), GUI.skin.GetStyle("On Slots"));
        }
        else
        {
            GUI.Box(slotRect, "+", GUI.skin.GetStyle("On Slots"));
        }

        //GUI.skin = healthSkin;

        //Draws Health Bar
        GUI.skin = healthBarSkin;
        slotRect = new Rect(healthBarX -2, healthBarY - 2, healthWidth + 4, healthHeight + 4);
        GUI.Box(slotRect, "", GUI.skin.GetStyle("Bar Skin"));

        //GUI.skin = healthBarSkin;

        //Draws the Health
        GUI.skin = healthSkin;
        float percentHealth = (float) player.GetComponent<Player>().health / player.GetComponent<Player>().maxHealth;
        slotRect = new Rect(healthBarX, healthBarY, healthWidth * percentHealth, healthHeight);
        GUI.Box(slotRect, "", GUI.skin.GetStyle("Health Skin"));
    }
}
