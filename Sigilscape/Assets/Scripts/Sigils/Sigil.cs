/*
    Nathan Cruz

    Base class for all sigils.
    Inherited classes must override the Effect()
    The script for the specific sigil should be referenced in the item's Sigil GameObject variable in the ItemDatabase.
*/

using UnityEngine;
using System.Collections;

abstract public class Sigil : MonoBehaviour {

	public enum SigilType { passive, targetAOE, directional };

    public SigilType sigilType;
    public float coolDown;
    public float timer = 0;
    public bool enabledSigil = true;

    //Updates timer
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
    }

    //Called by Equipment.cs to activate effect
    public void activated()
    {
        Disable();
        SetTimer();
        Effect();
    }

    void Enable()
    {
        enabledSigil = true;
    }

    void Disable()
    {
        enabledSigil = false;
    }

    void SetTimer()
    {
        timer = coolDown;
    }

    abstract public void Effect();

    void CountDown()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            timer = 0;
        }
    }    
}
