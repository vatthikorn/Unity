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
        items[0].addItemInfo(0, "Iron Sword", "Sword made out of iron. How basic can you get?", Item.ItemType.weapon, 10, 0.05f, Item.AttackSpeed.slow, Item.Range.small, Item.Knockback.small, Item.WeaponType.melee);
        items[1].addItemInfo(1, "Steel Sword", "Sword made out of steel. Well now. Slightly better than a commoner's sword. Only slightly, thought.", Item.ItemType.weapon, 20, 0.05f, Item.AttackSpeed.slow, Item.Range.small, Item.Knockback.small, Item.WeaponType.melee);
        items[2].addItemInfo(2, "Silver Sword", "Who makes swords out of silver? I mean, it would look so much nicer as ring. Whatever. Turns out silver is actually stronger than steel? And hey, if you ever come across werewolves, you are in good hands... or you have a good sword in your hands... Yes.", Item.ItemType.weapon, 30, 0.05f, Item.AttackSpeed.slow, Item.Range.small, Item.Knockback.small, Item.WeaponType.melee);
        items[3].addItemInfo(3, "Wooden Club", "... You really just don't care about class do you?", Item.ItemType.weapon, 15, 0.025f, Item.AttackSpeed.slowest, Item.Range.small, Item.Knockback.large, Item.WeaponType.melee);
        items[4].addItemInfo(4, "Iron Mace", "Smash your foe's face with this. Make them use their dental insurance like they're supposed to. Teach them to take care of their teeth.", Item.ItemType.weapon, 30, 0.025f, Item.AttackSpeed.slowest, Item.Range.small, Item.Knockback.large, Item.WeaponType.melee);
        items[5].addItemInfo(5, "Steel Mace", "A steel mace... I am turned on.", Item.ItemType.weapon, 45, 0.025f, Item.AttackSpeed.slowest, Item.Range.small, Item.Knockback.large, Item.WeaponType.melee);
        items[6].addItemInfo(6, "Wooden Spear", "Look, buddy... I love spears as much as life itself... But this is pretty poopy.", Item.ItemType.weapon, 10, 0.07f, Item.AttackSpeed.slowest, Item.Range.largest, Item.Knockback.smallest, Item.WeaponType.melee);
        items[7].addItemInfo(7, "Iron Spear", "Nice spear. Where'd you get it? At the dollar store?", Item.ItemType.weapon, 20, 0.07f, Item.AttackSpeed.slowest, Item.Range.largest, Item.Knockback.smallest, Item.WeaponType.melee);
        items[8].addItemInfo(8, "Steel Spear", "Nice spear. It's actually pretty decent. Here, let me give you a few pointers. You see that pointy end? Tear open your enemie's guts and spill out there intenstines like a surgeon cuts open a woman during a c-section.", Item.ItemType.weapon, 45, 0.07f, Item.AttackSpeed.slowest, Item.Range.largest, Item.Knockback.smallest, Item.WeaponType.melee);
        items[9].addItemInfo(9, "Wooden Bow", "That's a sturdy looking bow. It's a shame it doesn't go \"pew pew pew\". Oh, wait. Technology at this time isn't that advanced yet. What am I talking about, you ask? Never mind that. You'll be long dead before technology gets that far.", Item.ItemType.weapon, 5, 0.04f, Item.AttackSpeed.medium, Item.Range.longs, Item.Knockback.smaller, Item.WeaponType.range);
        items[10].addItemInfo(10, "Iron Bow", "Hm... Iron doesn't really bend now does it? Dang, man. Have you been working out your forearms? Nice.", Item.ItemType.weapon, 10, 0.04f, Item.AttackSpeed.medium, Item.Range.longs, Item.Knockback.smaller, Item.WeaponType.range);
        items[11].addItemInfo(11, "Steel Bow", "... Can I feel your forearms?", Item.ItemType.weapon, 20, 0.04f, Item.AttackSpeed.medium, Item.Range.longs, Item.Knockback.smaller, Item.WeaponType.range);
        items[12].addItemInfo(12, "Sharp Metal", "I would insult you for your lack of class. But this is real edgy of you, and I like it.", Item.ItemType.weapon, 2, 0.08f, Item.AttackSpeed.fast, Item.Range.longest, Item.Knockback.smallest, Item.WeaponType.range);
        items[13].addItemInfo(13, "Iron Throwing Dagger", "To be honest with you, I prefer daggers for stabbing not throwing. But, I guess nothing strikes fear into the hearts of your enemies as you masterfully throws daggers at them. Not many people can do that, and it is awesome.", Item.ItemType.weapon, 6, 0.08f, Item.AttackSpeed.fast, Item.Range.longest, Item.Knockback.smallest, Item.WeaponType.range);
        items[14].addItemInfo(14, "Steel Throwing Dagger", "Just like the Iron Throwing Dagger, but nicer.", Item.ItemType.weapon, 12, 0.08f, Item.AttackSpeed.fast, Item.Range.longest, Item.Knockback.smallest, Item.WeaponType.range);
        items[15].addItemInfo(15, "Cloth Armor", "Might as well be naked... Could you be naked? I think people would like that. Your enemies might show some mercy...", Item.ItemType.armor, 5);
        items[16].addItemInfo(16, "The Key", "Oh. This isn't no ordinary key. Baby, you got a sigil, and this thing unlocks every single thing you can find.", Item.ItemType.sigil);
        items[17].addItemInfo(17, "Chastity Belt", "Don't worry, mate. No one cares if you're a virgin. They just want to kill you that's all.", Item.ItemType.sigil);
        items[18].addItemInfo(18, "Basic Shield", "Test shield.", Item.ItemType.shield, 0.5f);
        items[19].addItemInfo(19, "Minor Healing Potion", "Good for small booboos.", Item.ItemType.consumable);
    }
}
