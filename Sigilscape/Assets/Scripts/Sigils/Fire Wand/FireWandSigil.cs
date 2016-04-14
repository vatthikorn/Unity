/*
    Nathan Cruz

    Controls the Fire Wand Sigil.
    Spawns a fire ball in the direction the player is facing.

    Dependencies:
    FireBall.cs - give it direction (Direction())

    Remember to:
    Place this script in an object that referenced in item's Sigil variable in the Database.
*/

using UnityEngine;
using System.Collections;

public class FireWandSigil : Sigil {

    public GameObject player;
    public GameObject fireball;
    
    //Sets up sigil stats
    void Start()
    {
        sigilType = SigilType.directional;
        coolDown = 5.0f;
    }

    //Spawns fire ball in front of player
    override public void Effect()
    {
        GameObject thisBall = (GameObject)Instantiate(fireball, new Vector2(player.transform.position.x + (player.GetComponent<PlayerController>().facingRight ? PlayerController.rangedOffsetRight : PlayerController.rangedOffsetLeft), player.transform.position.y + PlayerController.rangedOffsetY), Quaternion.identity);
        thisBall.GetComponent<FireBall>().Direction(player.GetComponent<PlayerController>().facingRight);
    }
}
