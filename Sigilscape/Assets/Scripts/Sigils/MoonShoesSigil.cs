/*
    /*
    Nathan Cruz

    Controls the Moon Shoes sigil.
    Boosts jump height
*/
using UnityEngine;
using System.Collections;

public class MoonShoesSigil : Sigil {

    public GameObject player;
    public GameObject equipment;

    public string sigilName = "Moon shoes";
    static public bool isEquipped = false;
    static public float jumpForce = 22f;
    static public bool jumping = false;
    static public float jumpTime = .5f;
    static public float jumpTimer = 0;

    //Sets up type
    void Start()
    {
        sigilType = SigilType.passive;
    }

    //Activates effect when equipped
    void Update()
    {
        isEquipped = IsEquipped();

        if (jumping)
        {
            jumpTimer -= Time.deltaTime;
            
            //Boosts jump as long as it is held or until boost runs out
            if(Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
            {
                player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
            }

            if(jumpTimer < 0)
            {
                jumpTimer = 0;
                jumping = false;
            }
        }
    }

    override public void Effect()
    {
        //Does nothing.
    }

    static public void Jump()
    {
        if(isEquipped)
        {
            jumping = true;
            jumpTimer = jumpTime;
        }
    }

    //Checks if it is equipped
    bool IsEquipped()
    {
        return (equipment.GetComponent<Equipment>().passiveSigil1.itemName != null && equipment.GetComponent<Equipment>().passiveSigil1.itemName == sigilName) ||
            (equipment.GetComponent<Equipment>().passiveSigil2.itemName != null && equipment.GetComponent<Equipment>().passiveSigil2.itemName == sigilName) ||
            (equipment.GetComponent<Equipment>().passiveSigil3.itemName != null && equipment.GetComponent<Equipment>().passiveSigil3.itemName == sigilName);
    }
}
