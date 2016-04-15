/*
    Nathan Cruz

    Controls the The Fister sigil.
    Charges at an enemy with a brass fist.
*/
using UnityEngine;
using System.Collections;

public class TheFisterSigil : Sigil {

    //Sets up sigil stats
    void Start()
    {
        sigilType = SigilType.directional;
        coolDown = 4.0f;
    }

    override public void Effect()
    {
        //CHANGE
    }
}
