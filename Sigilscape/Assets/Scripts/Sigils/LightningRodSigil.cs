/*
    Nathan Cruz

    Controls the Lightning Rod sigil.
    Summons 10 pillars of lightning in front of the player.
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightningRodSigil : Sigil {

    //For reference of location and what to spawn
    public GameObject player;
    public GameObject lightning;

    //Distance from player and to each other to spawn at, time for next bolt to spawn, number of bolts to spawn
    public const float xOffSet = 2.0f;
    public const float xDelta = 1.5f;
    public const float timeDelta = .02f;
    public int lightningCount = 10;

    //Sets up value
    void Start()
    {
        coolDown = 10.0f;
        sigilType = SigilType.directional;
    }

	override public void Effect()
    {
        StartCoroutine(Spawn());
    }

    //Spawns the bolt pending on direction
    IEnumerator Spawn()
    {
        GameObject thisLightning;

        if(player.GetComponent<PlayerController>().facingRight)
        {
            for (int i = 0; i < lightningCount; i++)
            {
                thisLightning = (GameObject)Instantiate(lightning, new Vector2(player.gameObject.transform.position.x + xOffSet + xDelta * i, player.gameObject.transform.position.y), new Quaternion());
                thisLightning.transform.rotation = Quaternion.Euler(0,0,-90);
                yield return new WaitForSeconds(timeDelta);
            }
        }
        else
        {
            for (int i = 0; i < lightningCount; i++)
            {
                thisLightning = (GameObject)Instantiate(lightning, new Vector2(player.gameObject.transform.position.x - xOffSet - xDelta * i, player.gameObject.transform.position.y), new Quaternion());
                thisLightning.transform.rotation = Quaternion.Euler(0, 0, -90);
                yield return new WaitForSeconds(timeDelta);
            }
        }
    }
}
