/*
    Nathan Cruz

    NOT COMPLETE: A WHOLE LOT TO DO
    THE IDEA IS TO HAVE BE SIMILAR TO PLAYER.CS IN WHICH IT DETECT COLLSIONS FROM PLAYER ATTACKS OR PLAYER BULLETS
    UPON DETECTION, WILL RECEIVE DAMAGE, FORCE, OTHER EFFECTS, ETC.
    BE DESTROYED ONCE HEALTH REACHES ZERO
    MAYBE DROP AN ITEM AS WELL
    ALSO TO HAVE AN AI ATTACHED TO THIS

    This was just created for a test for parts of the game, but expand on it please if you have the time.
    WILL BE PART OF A MONSTERMANAGER.CS FOR EASY CLONING
*/

using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public bool alive;
    public int health;
    public int maxHealth;
    public int strength;
    public int defense;
}
