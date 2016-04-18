/*
    Nathan Cruz

    Spawned by the The Fister (TheFisterSigil.cs) when activated by the player.

    Hurts enemies and knocks them back far.
*/
using UnityEngine;
using System.Collections;

public class TheFist : MonoBehaviour {
    
    public const int damage = 30;

    //Applies damage to enemy
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<Enemy>().ReceiveDamage(damage);
        }
    }
}
