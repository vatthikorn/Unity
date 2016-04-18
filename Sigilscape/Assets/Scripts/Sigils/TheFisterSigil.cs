/*
    Nathan Cruz

    Controls the The Fister sigil.
    Charges at an enemy with a brass fist. Major knockback from players force.
*/
using UnityEngine;
using System.Collections;

public class TheFisterSigil : Sigil {

    public GameObject player;
    public GameObject theFist;

    public GameObject currentFist;

    public const float forceX = 500f;
    public const int xOffset = 1;
    public const float chargeTime = 0.15f;
    public float chargeTimer = 0.0f;
    public bool charging = false;

    //Sets up sigil stats
    void Start()
    {
        sigilType = SigilType.directional;
        coolDown = 7.0f;
    }

    void Update()
    {
        //Counts down timer
        if (enabledSigil == false)
        {
            CountDown();
        }

        //Enables sigil on when timer is 0
        if (timer == 0)
        {
            Enable();
        }

        //Applies a constant force when the player charges, Player is invincible while charging
        if (charging)
        {
            chargeTimer -= Time.deltaTime;
            if(player.GetComponent<PlayerController>().facingRight)
            {
                player.GetComponent<Rigidbody2D>().AddForce(new Vector2(forceX, 0));
                currentFist.transform.position = new Vector2(player.transform.position.x + xOffset, player.transform.position.y);
            }
            else
            {
                player.GetComponent<Rigidbody2D>().AddForce(new Vector2(-forceX, 0));
                currentFist.transform.position = new Vector2(player.transform.position.x - xOffset, player.transform.position.y);
            }

            if(chargeTimer < 0)
            {
                chargeTimer = 0;
                charging = false;
                Destroy(currentFist);
                player.GetComponent<Player>().isInvincible = false;
            }
        }
    }

    //Spawns a fist and charges
    override public void Effect()
    {
        player.GetComponent<Player>().isInvincible = true;
        chargeTimer = chargeTime;
        if(player.GetComponent<PlayerController>().facingRight)
        {
            currentFist = (GameObject)Instantiate(theFist, new Vector2(player.gameObject.transform.position.x + xOffset, player.gameObject.transform.position.y), Quaternion.identity);
        }
        else
        {
            currentFist = (GameObject)Instantiate(theFist, new Vector2(player.gameObject.transform.position.x - xOffset, player.gameObject.transform.position.y), Quaternion.identity);
        }
        charging = true;
    }
}
