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
        items[0].addItemInfo(0, "Iron Sword", "Sword made out of iron. The most basic of weapons that you can get.", Item.ItemType.weapon, 10, 0.05f, Item.AttackSpeed.medium, Item.Range.medium, Item.Knockback.medium, Item.WeaponType.melee);
        items[1].addItemInfo(1, "Steel Sword", "Somewhat better than the commoner's sword.", Item.ItemType.weapon, 20, 0.05f, Item.AttackSpeed.medium, Item.Range.medium, Item.Knockback.medium, Item.WeaponType.melee);
        items[2].addItemInfo(2, "Silver Sword", "The concept of a silver sword was at first thought absurd... At least until the Invasion of 1984 when the an army of werewolves laid siege to the kingdom.", Item.ItemType.weapon, 30, 0.05f, Item.AttackSpeed.medium, Item.Range.medium, Item.Knockback.medium, Item.WeaponType.melee);
        items[3].addItemInfo(3, "Admantium Sword", "A sword that can only be legally forged for those of nobel descent.", Item.ItemType.weapon, 50, 0.05f, Item.AttackSpeed.medium, Item.Range.medium, Item.Knockback.medium, Item.WeaponType.melee);
        items[4].addItemInfo(4, "Diamond Sword", "Warriors scoffed while the nobility were furious, until used against a dragon and was noted for its effectiveness in penetrating its tough exterior.", Item.ItemType.weapon, 70, 0.05f, Item.AttackSpeed.medium, Item.Range.medium, Item.Knockback.medium, Item.WeaponType.melee);
        items[5].addItemInfo(5, "Sigil Sword", "A sword embued with the mysterious energies of the world.", Item.ItemType.weapon, 100, 0.05f, Item.AttackSpeed.medium, Item.Range.medium, Item.Knockback.medium, Item.WeaponType.melee);
        items[6].addItemInfo(6, "Light Saber", "Only one is known to exist, and its origin is unknown. Astronomers theorize it came from a galaxy far, fary away.", Item.ItemType.weapon, 9999, 0.05f, Item.AttackSpeed.medium, Item.Range.medium, Item.Knockback.medium, Item.WeaponType.melee);
       
        //Maces
        items[7].addItemInfo(7, "Wooden Club", "Welp", Item.ItemType.weapon, 15, 0.025f, Item.AttackSpeed.slowest, Item.Range.medium, Item.Knockback.largest, Item.WeaponType.melee);
        items[8].addItemInfo(8, "Iron Mace", "Nice spear. It's actually pretty decent. Here, let me give you a few pointers. You see that pointy end? Tear open your enemie's guts and spill out there intenstines like a surgeon cuts open a woman during a c-section.", Item.ItemType.weapon, 30, 0.025f, Item.AttackSpeed.slowest, Item.Range.medium, Item.Knockback.largest, Item.WeaponType.melee);
        items[9].addItemInfo(9, "Steel Mace", "That's a sturdy looking bow. It's a shame it doesn't go \"pew pew pew\". Oh, wait. Technology at this time isn't that advanced yet. What am I talking about, you ask? Never mind that. You'll be long dead before technology gets that far.", Item.ItemType.weapon, 45, 0.025f, Item.AttackSpeed.slowest, Item.Range.medium, Item.Knockback.largest, Item.WeaponType.melee);
        items[10].addItemInfo(10, "Steel Hammer", "Hm... Iron doesn't really bend now does it? Dang, man. Have you been working out your forearms? Nice.", Item.ItemType.weapon, 70, 0.025f, Item.AttackSpeed.slowest, Item.Range.medium, Item.Knockback.largest, Item.WeaponType.melee);
        items[11].addItemInfo(11, "Silver War Hammer", "... Can I feel your forearms?", Item.ItemType.weapon, 85, 0.025f, Item.AttackSpeed.slowest, Item.Range.medium, Item.Knockback.largest, Item.WeaponType.melee);
        items[12].addItemInfo(12, "Adamantium Crusher", "I would insult you for your lack of class. But this is real edgy of you, and I like it.", Item.ItemType.weapon, 100, 0.025f, Item.AttackSpeed.slowest, Item.Range.medium, Item.Knockback.largest, Item.WeaponType.melee);
        items[13].addItemInfo(13, "Diamond Destroyer", "To be honest with you, I prefer daggers for stabbing not throwing. But, I guess nothing strikes fear into the hearts of your enemies as you masterfully throws daggers at them. Not many people can do that, and it is awesome.", Item.ItemType.weapon, 120, 0.025f, Item.AttackSpeed.slowest, Item.Range.medium, Item.Knockback.largest, Item.WeaponType.melee);
        items[14].addItemInfo(14, "Sigil Apocalypse", "Just like the Iron Throwing Dagger, but nicer.", Item.ItemType.weapon, 150, 0.025f, Item.AttackSpeed.slowest, Item.Range.medium, Item.Knockback.largest, Item.WeaponType.melee);

        //Spears
        items[15].addItemInfo(15, "Wooden Spear", "Might as well be naked... Could you be naked? I think people would like that. Your enemies might show some mercy...", Item.ItemType.weapon, 10, 0.07f, Item.AttackSpeed.slow, Item.Range.largest, Item.Knockback.smallest, Item.WeaponType.melee);
        items[16].addItemInfo(16, "Iron Spear", "Oh. This isn't no ordinary key. Baby, you got a sigil, and this thing unlocks every single thing you can find.", Item.ItemType.weapon, 20, 0.07f, Item.AttackSpeed.slow, Item.Range.largest, Item.Knockback.smallest, Item.WeaponType.melee);
        items[17].addItemInfo(17, "Steel Spear", "Don't worry, mate. No one cares if you're a virgin. They just want to kill you that's all.", Item.ItemType.weapon, 35, 0.07f, Item.AttackSpeed.slow, Item.Range.largest, Item.Knockback.smallest, Item.WeaponType.melee);
        items[18].addItemInfo(18, "Silver Spear", "Test shield.", Item.ItemType.weapon, 55, 0.07f, Item.AttackSpeed.slow, Item.Range.largest, Item.Knockback.smallest, Item.WeaponType.melee);
        items[19].addItemInfo(19, "Adamantium Spear", "Good for small booboos.", Item.ItemType.weapon, 65, 0.07f, Item.AttackSpeed.slow, Item.Range.largest, Item.Knockback.smallest, Item.WeaponType.melee);
        items[20].addItemInfo(20, "Diamond Spear", "Shoots fire!", Item.ItemType.weapon, 80, 0.07f, Item.AttackSpeed.slow, Item.Range.largest, Item.Knockback.smallest, Item.WeaponType.melee);
        items[21].addItemInfo(21, "Sigil Spear", "Good for small booboos.", Item.ItemType.weapon, 110, 0.07f, Item.AttackSpeed.slow, Item.Range.largest, Item.Knockback.smallest, Item.WeaponType.melee);

        //Bows
        items[22].addItemInfo(22, "Wooden Bow", "Good for small booboos.", Item.ItemType.weapon, 5, 0.04f, Item.AttackSpeed.fast, Item.Range.longest, Item.Knockback.small, Item.WeaponType.range);
        items[23].addItemInfo(23, "Iron Bow", "Good for small booboos.", Item.ItemType.weapon, 10, 0.04f, Item.AttackSpeed.fast, Item.Range.longest, Item.Knockback.small, Item.WeaponType.range);
        items[24].addItemInfo(24, "Steel Bow", "Sword made out of iron. How basic can you get?", Item.ItemType.weapon, 20, 0.04f, Item.AttackSpeed.fast, Item.Range.longest, Item.Knockback.small, Item.WeaponType.range);
        items[25].addItemInfo(25, "Silver Bow", "Sword made out of steel. Well now. Slightly better than a commoner's sword. Only slightly, thought.", Item.ItemType.weapon, 30, 0.04f, Item.AttackSpeed.fast, Item.Range.longest, Item.Knockback.small, Item.WeaponType.range);
        items[26].addItemInfo(26, "Adamantium Bow", "Who makes swords out of silver? I mean, it would look so much nicer as ring. Whatever. Turns out silver is actually stronger than steel? And hey, if you ever come across werewolves, you are in good hands... or you have a good sword in your hands... Yes.", Item.ItemType.weapon, 50, 0.04f, Item.AttackSpeed.fast, Item.Range.longest, Item.Knockback.small, Item.WeaponType.range);
        items[27].addItemInfo(27, "Diamond Bow", "... You really just don't care about class do you?", Item.ItemType.weapon, 70, 0.04f, Item.AttackSpeed.fast, Item.Range.longest, Item.Knockback.small, Item.WeaponType.range);
        items[28].addItemInfo(28, "Sigil Bow", "Smash your foe's face with this. Make them use their dental insurance like they're supposed to. Teach them to take care of their teeth.", Item.ItemType.weapon, 90, 0.04f, Item.AttackSpeed.fast, Item.Range.longest, Item.Knockback.small, Item.WeaponType.range);

        //Throwing Dagger
        items[29].addItemInfo(29, "Sharp Metal", "A steel mace... I am turned on.", Item.ItemType.weapon, 2, 0.08f, Item.AttackSpeed.fastest, Item.Range.longs, Item.Knockback.smaller, Item.WeaponType.range);
        items[30].addItemInfo(30, "Iron Throwing Dagger", "Look, buddy... I love spears as much as life itself... But this is pretty poopy.", Item.ItemType.weapon, 6, 0.08f, Item.AttackSpeed.fastest, Item.Range.longs, Item.Knockback.smaller, Item.WeaponType.range);
        items[31].addItemInfo(31, "Steel Throwing Dagger", "Nice spear. Where'd you get it? At the dollar store?", Item.ItemType.weapon, 12, 0.08f, Item.AttackSpeed.fastest, Item.Range.longs, Item.Knockback.smaller, Item.WeaponType.range);
        items[32].addItemInfo(32, "Silver Throwing Dagger", "Nice spear. It's actually pretty decent. Here, let me give you a few pointers. You see that pointy end? Tear open your enemie's guts and spill out there intenstines like a surgeon cuts open a woman during a c-section.", Item.ItemType.weapon, 18, 0.08f, Item.AttackSpeed.fastest, Item.Range.longs, Item.Knockback.smaller, Item.WeaponType.range);
        items[33].addItemInfo(33, "Adamantium Beheader", "That's a sturdy looking bow. It's a shame it doesn't go \"pew pew pew\". Oh, wait. Technology at this time isn't that advanced yet. What am I talking about, you ask? Never mind that. You'll be long dead before technology gets that far.", Item.ItemType.weapon, 36, 0.08f, Item.AttackSpeed.fastest, Item.Range.longs, Item.Knockback.smaller, Item.WeaponType.range);
        items[34].addItemInfo(34, "Diamond Eviscerator", "Hm... Iron doesn't really bend now does it? Dang, man. Have you been working out your forearms? Nice.", Item.ItemType.weapon, 45, 0.08f, Item.AttackSpeed.fastest, Item.Range.longs, Item.Knockback.smaller, Item.WeaponType.range);
        items[35].addItemInfo(35, "Sigil Annhilator", "... Can I feel your forearms?", Item.ItemType.weapon, 60, 0.08f, Item.AttackSpeed.fastest, Item.Range.longs, Item.Knockback.smaller, Item.WeaponType.range);

        //Armor
        items[36].addItemInfo(36, "Cloth Armor", "I would insult you for your lack of class. But this is real edgy of you, and I like it.", Item.ItemType.armor, 5);
        items[37].addItemInfo(37, "Leather Armor", "To be honest with you, I prefer daggers for stabbing not throwing. But, I guess nothing strikes fear into the hearts of your enemies as you masterfully throws daggers at them. Not many people can do that, and it is awesome.", Item.ItemType.armor, 10);
        items[38].addItemInfo(38, "Iron Armor", "Just like the Iron Throwing Dagger, but nicer.", Item.ItemType.armor, 15);
        items[39].addItemInfo(39, "Steel Armor", "Might as well be naked... Could you be naked? I think people would like that. Your enemies might show some mercy...", Item.ItemType.armor, 30);
        items[40].addItemInfo(40, "Silver Armor", "Oh. This isn't no ordinary key. Baby, you got a sigil, and this thing unlocks every single thing you can find.", Item.ItemType.armor, 45);
        items[41].addItemInfo(41, "Andamantium Armor", "Don't worry, mate. No one cares if you're a virgin. They just want to kill you that's all.", Item.ItemType.armor, 60);
        items[42].addItemInfo(42, "Diamond Armor", "Test shield.", Item.ItemType.armor, 80);
        items[43].addItemInfo(43, "Sigil Armor", "Good for small booboos.", Item.ItemType.armor, 100);

        //Shield
        items[55].addItemInfo(55, "Leather Shield", "", Item.ItemType.shield, .3f);
        items[56].addItemInfo(56, "Small Wooden Shield", "", Item.ItemType.shield, .4f);
        items[57].addItemInfo(57, "Wooden Shield", "", Item.ItemType.shield, .5f);
        items[58].addItemInfo(58, "Kite Shield", "", Item.ItemType.shield, .6f);
        items[59].addItemInfo(59, "Iron Cast Shield", "", Item.ItemType.shield, .7f);

        //Active Sigils
        items[44].addItemInfo(44, "The Key", "Shoots fire!", Item.ItemType.sigil);
        items[45].addItemInfo(45, "Fire Wand", "Good for small booboos.", Item.ItemType.sigil);
        items[46].addItemInfo(46, "The Fister", "Good for small booboos.", Item.ItemType.sigil);
        items[47].addItemInfo(47, "Meteorite Targeting System", "Good for small booboos.", Item.ItemType.sigil);

        //Passive Sigils
        items[48].addItemInfo(48, "Moon shoes", "Good for small booboos.", Item.ItemType.sigil);
        items[49].addItemInfo(49, "Mask of Fear", "Shoots fire!", Item.ItemType.sigil);
        items[50].addItemInfo(50, "Eterna's Blessing", "Good for small booboos.", Item.ItemType.sigil);

        //Potions
        items[51].addItemInfo(51, "Minor Healing Potion", "Good for small booboos.", Item.ItemType.consumable);
        items[52].addItemInfo(52, "Healing Potion", "Good for small booboos.", Item.ItemType.consumable);
        items[53].addItemInfo(53, "Greater Healing Potion", "Good for small booboos.", Item.ItemType.consumable);
        items[54].addItemInfo(54, "Sigil Potion", "Good for small booboos.", Item.ItemType.consumable);
    }
}
