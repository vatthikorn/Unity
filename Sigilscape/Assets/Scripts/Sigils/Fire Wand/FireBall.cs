/*
    Nathan Cruz

    Spawned by the Fire Wand (FireWandSigil.cs) when activated by the player.

    Hurts enemies.
*/
using UnityEngine;
using System.Collections;

public class FireBall : MonoBehaviour {

    //Fireball stats
    public Vector2 speed = new Vector2(10, 0);
    public Vector2 velocity;
    public Vector2 knockBack = new Vector2(100 , 100);
    public const int damage = 20;

    //Maintains velocity
    void Update()
    {
        this.gameObject.GetComponent<Rigidbody2D>().velocity = velocity;
    }

    //Destroys itself on impact
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Sign(velocity.x) * knockBack.x, knockBack.y));
            other.gameObject.GetComponent<Enemy>().ReceiveDamage(damage);
            
            Destroy(this.gameObject);
        }

        else if(other.gameObject.tag == "Wall" || other.gameObject.tag == "Ground")
        {
            Destroy(this.gameObject);
        }
    }

    //Gets direction
    public void Direction(bool facingRight)
    {
        if (facingRight)
            velocity = speed;
        else
            velocity = new Vector2(-speed.x, speed.y);
    }
}
