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
