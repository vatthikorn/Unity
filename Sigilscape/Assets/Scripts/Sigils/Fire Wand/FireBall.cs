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
    public bool impact = false;
    public const float animTime = .32f;

    public Animator anim;

    void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();
    }

    //Maintains velocity
    void Update()
    {
        if(!impact)
        {
            anim.SetBool("impact", false);
            this.gameObject.GetComponent<Rigidbody2D>().velocity = velocity;
        }
        else
        {
            anim.SetBool("impact", true);
            this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    //Destroys itself on impact
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            impact = true;
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Sign(velocity.x) * knockBack.x, knockBack.y));
            other.gameObject.GetComponent<Enemy>().ReceiveDamage(damage);
            Invoke("Impact", animTime);
        }

        else if(other.gameObject.tag == "Wall" || other.gameObject.tag == "Ground")
        {
            impact = true;
            Invoke("Impact", animTime);
        }
    }

    //Gets direction
    public void Direction(bool facingRight)
    {
        if (facingRight)
        {
            velocity = speed;
        }            
        else
        {
            velocity = new Vector2(-speed.x, speed.y);
            this.transform.localScale = (new Vector2(-this.transform.localScale.x, this.transform.localScale.y));
        }
    }

    void Impact()
    {
        Destroy(this.gameObject);
    }
}
