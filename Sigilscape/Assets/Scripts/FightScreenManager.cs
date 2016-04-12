/*
    Nathan Cruz

    Display health, items and sigils equipped
    Display cooldown for items and sigils equipped

*/
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FightScreenManager : MonoBehaviour {

    public GameObject player;
    public GameObject equipment;

    public GUISkin shadeSkin;
    public GUISkin healthSkin;
    public GUISkin healthBarSkin;
    public GUISkin itemSkin;
    public GUISkin sigilSkin;

    //Sigil Icon's Positions
    public const int SigilY = 25;

    public const int Sigil1X = 225;
    public const int Sigil2X = 300;
    public const int Sigil3X = 375;
    public const int Sigil4X = 450;

    //Potions Icon's Positions
    public const int SigilX = 225;
    public const int Sigil1Y = 25;

    void OnGUI()
    {
        if (player.GetComponent<PlayerController>().screenState == PlayerController.ScreenState.fight)
        {
            DrawFightGUI();
        }
    }

    void DrawFightGUI()
    {
        GUI.skin = sigilSkin;

        //Draws Sigil1 Icon
        Rect slotRect = new Rect(225, 25, 50, 50);
        GUI.Box(slotRect, "");
        if(equipment.GetComponent<Equipment>().activeSigil1.itemID != 0)
        {
            GUI.DrawTexture(slotRect, equipment.GetComponent<Equipment>().activeSigil1.icon);
        }

        //Draws Sigil2 Icon
        slotRect = new Rect(300, 25, 50, 50);
        GUI.Box(slotRect, "");
        if (equipment.GetComponent<Equipment>().activeSigil2.itemID != 0)
        {
            GUI.DrawTexture(slotRect, equipment.GetComponent<Equipment>().activeSigil2.icon);
        }

        //Draws Sigil3 Icon
        slotRect = new Rect(375, 25, 50, 50);
        GUI.Box(slotRect, "");
        if (equipment.GetComponent<Equipment>().activeSigil3.itemID != 0)
        {
            GUI.DrawTexture(slotRect, equipment.GetComponent<Equipment>().activeSigil3.icon);
        }

        //Draws Sigil4 Icon
        slotRect = new Rect(450, 25, 50, 50);
        GUI.Box(slotRect, "");
        if (equipment.GetComponent<Equipment>().activeSigil4.itemID != 0)
        {
            GUI.DrawTexture(slotRect, equipment.GetComponent<Equipment>().activeSigil4.icon);
        }

        GUI.skin = itemSkin;

        //Draws HealthPotion Icon
        slotRect = new Rect(25, 85, 50, 50);
        GUI.Box(slotRect, "");
        if (equipment.GetComponent<Equipment>().healthPotions.itemID != 0)
        {
            GUI.DrawTexture(slotRect, equipment.GetComponent<Equipment>().healthPotions.icon);
        }

        //Draws SigilPotion Icon
        slotRect = new Rect(100, 85, 50, 50);
        GUI.Box(slotRect, "");
        if (equipment.GetComponent<Equipment>().sigilPotions.itemID != 0)
        {

        }

        GUI.skin = healthSkin;

        //Draws Health Bar
        slotRect = new Rect(25, 25, 175, 50);
        GUI.Box(slotRect, "");

        GUI.skin = healthBarSkin;

        //Draws the Health
        float percentHealth = (float) player.GetComponent<Player>().health / player.GetComponent<Player>().maxHealth;
        GUI.contentColor = Color.red;
        slotRect = new Rect(25, 25, 175 * percentHealth, 50);
        GUI.Box(slotRect, "");
    }
}
