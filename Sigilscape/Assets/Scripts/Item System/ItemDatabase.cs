/*
    Nathan Cruz

    This is to keep information of every item in the game for the purposes of inventory and equipment.
    A whole lot of other things depend on this too. Basically everything that deals with items.

    Interface:
    everything here, everything needs everything here

    Required:
    Attached to an empty object.
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour {

    public List<Item> items = new List<Item>();

    void Start()
    {
        //Swords
        items[0].addItemInfo(0, "Iron Sword", "Sword made out of iron. The most basic of weapons that you can get.", Item.ItemType.weapon, 10, 0.05f, Item.AttackSpeed.medium, Item.Range.Medium, Item.Knockback.medium, Item.WeaponType.Melee);
        items[1].addItemInfo(1, "Steel Sword", "Somewhat better than the commoner's sword.", Item.ItemType.weapon, 20, 0.05f, Item.AttackSpeed.medium, Item.Range.Medium, Item.Knockback.medium, Item.WeaponType.Melee);
        items[2].addItemInfo(2, "Silver Sword", "The concept of a silver sword was at first thought absurd... At least until the Invasion of 1984 when the an army of werewolves laid siege to the kingdom.", Item.ItemType.weapon, 30, 0.05f, Item.AttackSpeed.medium, Item.Range.Medium, Item.Knockback.medium, Item.WeaponType.Melee);
        items[3].addItemInfo(3, "Adamantium Sword", "A sword that can only be legally forged for those of nobel descent.", Item.ItemType.weapon, 50, 0.05f, Item.AttackSpeed.medium, Item.Range.Medium, Item.Knockback.medium, Item.WeaponType.Melee);
        items[4].addItemInfo(4, "Diamond Sword", "Warriors scoffed while the nobility were furious, until used against a dragon and was noted for its effectiveness in penetrating its tough exterior.", Item.ItemType.weapon, 70, 0.05f, Item.AttackSpeed.medium, Item.Range.Medium, Item.Knockback.medium, Item.WeaponType.Melee);
        items[5].addItemInfo(5, "Sigil Sword", "A sword embued with the mysterious energies of the world.", Item.ItemType.weapon, 100, 0.05f, Item.AttackSpeed.medium, Item.Range.Medium, Item.Knockback.medium, Item.WeaponType.Melee);
        items[6].addItemInfo(6, "Light Saber", "Only one is known to exist, and its origin is unknown. Astronomers theorize it came from a galaxy far, fary away.", Item.ItemType.weapon, 9999, 0.05f, Item.AttackSpeed.medium, Item.Range.Medium, Item.Knockback.medium, Item.WeaponType.Melee);
       
        //Maces
        items[7].addItemInfo(7, "Wooden Club", "Only barbarians should be seen with this.", Item.ItemType.weapon, 15, 0.025f, Item.AttackSpeed.slowest, Item.Range.Medium, Item.Knockback.largest, Item.WeaponType.Melee);
        items[8].addItemInfo(8, "Iron Mace", "Smash thine enemies face with this.", Item.ItemType.weapon, 30, 0.025f, Item.AttackSpeed.slowest, Item.Range.Medium, Item.Knockback.largest, Item.WeaponType.Melee);
        items[9].addItemInfo(9, "Steel Mace", "Make your foes eat this ten pound steel ball.", Item.ItemType.weapon, 45, 0.025f, Item.AttackSpeed.slowest, Item.Range.Medium, Item.Knockback.largest, Item.WeaponType.Melee);
        items[10].addItemInfo(10, "Steel Hammer", "This is generally not used for construction.", Item.ItemType.weapon, 70, 0.025f, Item.AttackSpeed.slowest, Item.Range.Medium, Item.Knockback.largest, Item.WeaponType.Melee);
        items[11].addItemInfo(11, "Silver War Hammer", "Used by the hero in the Invasion of 1984.", Item.ItemType.weapon, 85, 0.025f, Item.AttackSpeed.slowest, Item.Range.Medium, Item.Knockback.largest, Item.WeaponType.Melee);
        items[12].addItemInfo(12, "Adamantium Crusher", "Crush Crush Crush - Paramore", Item.ItemType.weapon, 100, 0.025f, Item.AttackSpeed.slowest, Item.Range.Medium, Item.Knockback.largest, Item.WeaponType.Melee);
        items[13].addItemInfo(13, "Diamond Destroyer", "Made out of diamonds. Not for the purposes of destroying diamonds.", Item.ItemType.weapon, 120, 0.025f, Item.AttackSpeed.slowest, Item.Range.Medium, Item.Knockback.largest, Item.WeaponType.Melee);
        items[14].addItemInfo(14, "Sigil Apocalypse", "Does not literally bring on the apocalypse. It's just a cool name.", Item.ItemType.weapon, 150, 0.025f, Item.AttackSpeed.slowest, Item.Range.Medium, Item.Knockback.largest, Item.WeaponType.Melee);

        //Spears
        items[15].addItemInfo(15, "Wooden Spear", "Not to be used as a toothpick.", Item.ItemType.weapon, 10, 0.07f, Item.AttackSpeed.slow, Item.Range.Largest, Item.Knockback.smallest, Item.WeaponType.Melee);
        items[16].addItemInfo(16, "Iron Spear", "Commonly seen used by the Roman Legion in like every movie about Rome ever.", Item.ItemType.weapon, 20, 0.07f, Item.AttackSpeed.slow, Item.Range.Largest, Item.Knockback.smallest, Item.WeaponType.Melee);
        items[17].addItemInfo(17, "Steel Spear", "Better than the iron spear.", Item.ItemType.weapon, 35, 0.07f, Item.AttackSpeed.slow, Item.Range.Largest, Item.Knockback.smallest, Item.WeaponType.Melee);
        items[18].addItemInfo(18, "Silver Spear", "Used to torture werewolves in the Invasion of 1984.", Item.ItemType.weapon, 55, 0.07f, Item.AttackSpeed.slow, Item.Range.Largest, Item.Knockback.smallest, Item.WeaponType.Melee);
        items[19].addItemInfo(19, "Adamantium Spear", "You could say the kingdom is adamant on spears.", Item.ItemType.weapon, 65, 0.07f, Item.AttackSpeed.slow, Item.Range.Largest, Item.Knockback.smallest, Item.WeaponType.Melee);
        items[20].addItemInfo(20, "Diamond Spear", "Every women in the kingdom coveths one.", Item.ItemType.weapon, 80, 0.07f, Item.AttackSpeed.slow, Item.Range.Largest, Item.Knockback.smallest, Item.WeaponType.Melee);
        items[21].addItemInfo(21, "Sigil Spear", "Devastatingly sharp.", Item.ItemType.weapon, 110, 0.07f, Item.AttackSpeed.slow, Item.Range.Largest, Item.Knockback.smallest, Item.WeaponType.Melee);

        //Bows
        items[22].addItemInfo(22, "Wooden Bow", "Basic bow issued to the common soldier.", Item.ItemType.weapon, 5, 0.04f, Item.AttackSpeed.fast, Item.Range.Longest, Item.Knockback.small, Item.WeaponType.Range);
        items[23].addItemInfo(23, "Iron Bow", "Only those who work out their forearms can use this.", Item.ItemType.weapon, 10, 0.04f, Item.AttackSpeed.fast, Item.Range.Longest, Item.Knockback.small, Item.WeaponType.Range);
        items[24].addItemInfo(24, "Steel Bow", "Only the elven can forge such beauties.", Item.ItemType.weapon, 20, 0.04f, Item.AttackSpeed.fast, Item.Range.Longest, Item.Knockback.small, Item.WeaponType.Range);
        items[25].addItemInfo(25, "Silver Bow", "Werewolves hated these in the Invasion of 1984.", Item.ItemType.weapon, 30, 0.04f, Item.AttackSpeed.fast, Item.Range.Longest, Item.Knockback.small, Item.WeaponType.Range);
        items[26].addItemInfo(26, "Adamantium Bow", "The third strongest bow in existence.", Item.ItemType.weapon, 50, 0.04f, Item.AttackSpeed.fast, Item.Range.Longest, Item.Knockback.small, Item.WeaponType.Range);
        items[27].addItemInfo(27, "Diamond Bow", "Even the string is made out of diamonds.", Item.ItemType.weapon, 70, 0.04f, Item.AttackSpeed.fast, Item.Range.Longest, Item.Knockback.small, Item.WeaponType.Range);
        items[28].addItemInfo(28, "Sigil Bow", "They say if you floss your teeth with string, they become magical.", Item.ItemType.weapon, 90, 0.04f, Item.AttackSpeed.fast, Item.Range.Longest, Item.Knockback.small, Item.WeaponType.Range);

        //Throwing Dagger
        items[29].addItemInfo(29, "Sharp Metal", "The best improvisation you could do.", Item.ItemType.weapon, 2, 0.08f, Item.AttackSpeed.fastest, Item.Range.Longs, Item.Knockback.smaller, Item.WeaponType.Range);
        items[30].addItemInfo(30, "Iron Throwing Dagger", "Strike fears into your foes as you flawlessly throw knives at them.", Item.ItemType.weapon, 6, 0.08f, Item.AttackSpeed.fastest, Item.Range.Longs, Item.Knockback.smaller, Item.WeaponType.Range);
        items[31].addItemInfo(31, "Steel Throwing Dagger", "Steel thine shovel. Oh, wrong game.", Item.ItemType.weapon, 12, 0.08f, Item.AttackSpeed.fastest, Item.Range.Longs, Item.Knockback.smaller, Item.WeaponType.Range);
        items[32].addItemInfo(32, "Silver Throwing Dagger", "Backstab a werewolf with these.", Item.ItemType.weapon, 18, 0.08f, Item.AttackSpeed.fastest, Item.Range.Longs, Item.Knockback.smaller, Item.WeaponType.Range);
        items[33].addItemInfo(33, "Adamantium Beheader", "Could slice a person's head clean off with this.", Item.ItemType.weapon, 36, 0.08f, Item.AttackSpeed.fastest, Item.Range.Longs, Item.Knockback.smaller, Item.WeaponType.Range);
        items[34].addItemInfo(34, "Diamond Eviscerator", "Could eviscerate someone wihout even trying.", Item.ItemType.weapon, 45, 0.08f, Item.AttackSpeed.fastest, Item.Range.Longs, Item.Knockback.smaller, Item.WeaponType.Range);
        items[35].addItemInfo(35, "Sigil Annhilator", "Disclaimer: Not a sex toy.", Item.ItemType.weapon, 60, 0.08f, Item.AttackSpeed.fastest, Item.Range.Longs, Item.Knockback.smaller, Item.WeaponType.Range);

        //Armor
        items[36].addItemInfo(36, "Cloth Armor", "Almost entirely useless.", Item.ItemType.armor, 5);
        items[37].addItemInfo(37, "Leather Armor", "Also used in S&M sessions.", Item.ItemType.armor, 10);
        items[38].addItemInfo(38, "Iron Armor", "Used by the common soldiers of the kingdom.", Item.ItemType.armor, 15);
        items[39].addItemInfo(39, "Steel Armor", "Steel armor.", Item.ItemType.armor, 30);
        items[40].addItemInfo(40, "Silver Armor", "Used in the Invasion of 1984 to torture werewolves by making them put this on/", Item.ItemType.armor, 45);
        items[41].addItemInfo(41, "Andamantium Armor", "Adamant about protection.", Item.ItemType.armor, 60);
        items[42].addItemInfo(42, "Diamond Armor", "Every noble woman owns one these days.", Item.ItemType.armor, 80);
        items[43].addItemInfo(43, "Sigil Armor", "Almost impenetrable.", Item.ItemType.armor, 100);

        //Shield
        items[55].addItemInfo(55, "Leather Shield", "Used by rogues and thieves and anyone who wants to move fast.", Item.ItemType.shield, .3f);
        items[56].addItemInfo(56, "Small Wooden Shield", "Offer little protection, but makes up for in hardiness and weight.", Item.ItemType.shield, .4f);
        items[57].addItemInfo(57, "Wooden Shield", "Issued to the common soldiers of the kingdom.", Item.ItemType.shield, .5f);
        items[58].addItemInfo(58, "Kite Shield", "Only those who have shone their skills can be given these in the kingdom.", Item.ItemType.shield, .6f);
        items[59].addItemInfo(59, "Iron Cast Shield", "Offers the best protection in the land.", Item.ItemType.shield, .7f);

        //Active Sigils
        items[44].addItemInfo(44, "The Key", "Can unlock anything that requires a key.", Item.ItemType.sigil);
        items[45].addItemInfo(45, "Fire Wand", "Shoots fireballs.", Item.ItemType.sigil);
        items[46].addItemInfo(46, "The Fister", "Charges at your enemies.", Item.ItemType.sigil);
        items[47].addItemInfo(47, "Lightning Rod", "Summons pillars of lightning in front of ye.", Item.ItemType.sigil);

        //Passive Sigils
        items[48].addItemInfo(48, "Moon shoes", "Allows for greater jumps (beware of low cielings)", Item.ItemType.sigil);
        items[49].addItemInfo(49, "Mask of Fear", "Strikes fear into enemies lowering their attacks and defenses.", Item.ItemType.sigil);
        items[50].addItemInfo(50, "Eterna's Blessing", "Grants protection to its wearer.", Item.ItemType.sigil);

        //Potions
        items[51].addItemInfo(51, "Minor Healing Potion", "Good for small booboos.", Item.ItemType.consumable);
        items[52].addItemInfo(52, "Healing Potion", "Good for big booboos.", Item.ItemType.consumable);
        items[53].addItemInfo(53, "Greater Healing Potion", "Good for all booboos.", Item.ItemType.consumable);
        items[54].addItemInfo(54, "Sigil Potion", "Resets the cooldown on sigils for another huzzah.", Item.ItemType.consumable);
    }
}
