/*
    Nathan Cruz

    Controls the Meteor Targeting System sigil.
    Summons a meteor to mouses's location.
*/
using UnityEngine;
using System.Collections;

public class MeteoriteTargetingSystemSigil : Sigil {

    //Sets up sigil stats
    void Start()
    {
        sigilType = SigilType.targetAOE;
        coolDown = 10.0f;
    }

    override public void Effect()
    {
        //CHANGE
    }
}
