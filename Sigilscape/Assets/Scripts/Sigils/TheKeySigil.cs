/*
    Nathan Cruz
    
    Controls the The Key sigil.
    Does nothing.
*/

using UnityEngine;
using System.Collections;

public class TheKeySigil : Sigil {

    //Sets up sigil stats
    void Start()
    {
        sigilType = SigilType.directional;
        coolDown = 0.0f;
    }

    override public void Effect()
    {
        //Does nothing. The Chests handle this in Chest.cs
    }
}
