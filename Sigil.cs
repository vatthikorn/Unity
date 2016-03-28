/*
    Nathan Cruz

    NOT COMPELTE, NOT STARTED, I DON'T EVEN KNOW WHAT I AM DOING HERE. LIKE. EVEN THOUGH I DID THE CLASS DIAGRAMS, I AM BLATANTLY IGNORING THEM CAUSE I SUCK AT FOLLOWING THROUGH WITH PLANS. 
    AND PLUS THE PLAN SUCKED. SO.
    THE IDEA IS TO HAVE THIS KEEP TRACK OF THE COOLDOWN, BEHAVIOR, DAMAGE, TYPE, ETC OF THE SIGIL. BASICALLY EVERYTHING CONCERNINNG SIGILS.
*/

using UnityEngine;
using System.Collections;

public class Sigil : Item
{

	public enum SigilType { passibe, targetAOE, directional };

    public SigilType sigilType;
    public int coolDown;
    public double timer;
    public int damage;
    public bool enabledSigil;
    public GameObject sigil;
    public Animator anim;

    /*void Update()
    {
        if (enabled == false)
        {
            countDown();
        }

        if (timer == 0)
        {
            enable();
        }
    }

    public void activated()
    {
        disable();
        setTimer();
        spawn();
    }

    void setTimer()
    {
        timer = coolDown;
    }

    void disable()
    {
        enabledSigil = false;
    }

    void spawn()
    {

    }

    void countDown()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            timer = 0;
        }
    }

    void enable()
    {
        enabledSigil = true;
    }*/
    
}
