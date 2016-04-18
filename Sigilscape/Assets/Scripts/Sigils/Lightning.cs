/*
    Nathan Cruz

    Spawned by the Lightning Rod (LightningRodSigil.cs) when activated by the player.

    Hurts enemies.
*/
using UnityEngine;
using System.Collections;

public class Lightning : MonoBehaviour {
    
    //Controls knockback, damage, and time til despawn
    public Vector2 knockBack = new Vector2(100, 100);
    public const int damage = 40;

    public const float animTime = .27f;

    //Destroys itself animTime time later
    void Start()
    {
        Invoke("DestroyMePlease", animTime);
    }

    //Applies knockback and damage to enemy
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if(other.gameObject.transform.position.x > this.transform.position.x)
            {
                other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(knockBack.x, knockBack.y));
            }
            else
            {
                other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-knockBack.x, knockBack.y));
            }
            
            other.gameObject.GetComponent<Enemy>().ReceiveDamage(damage);
        }
    }

    //Destroys itself after set time
    void DestroyMePlease()
    {
        Destroy(this.gameObject);
    }
}
