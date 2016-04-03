/*
    Nathan Cruz

    This is attached to the weapon the player has. When the enemy comes into contact, this script will look into the player's equipment and see what weapon they have, look at the damage,
    and call the Enemy script to reduce the enemy's health. Look at knockBack of the weapon and apply the force to the enemy.
    Handles KnockBack.
    Handles Damage dealt to Enemy.
    Handles Critcal Chance.

    Dependencies:
    N/A

    Required:
    The enemy GameObjects need to have the "Enemy" tag.
    The attack gameObjects should be a child to a gameObject that is a child to the Player gameObect (The parent of the attack is just a gameObject group together and organized all of the attack objects).

    Remembet to:
    Set isTrigger for all Melee Object to true
*/
using UnityEngine;
using System.Collections;
using System;

public class PlayerAttack : MonoBehaviour {

    //KnockBack in vertical
    public const float knockBackY = 100f;

    //KnockBack in horizontal
    public const float smallestKnockBack = 100f;
    public const float smallerKnockBack = 150f;
    public const float smallKnockBack = 200f;
    public const float mediumKnockBack = 250f;
    public const float largeKnockBack = 300f;
    public const float largestKnockBack = 350f;

    //Critical Hit Multiplier
    public const float criticalHitMultiplier = 2.0f;

    int direction;//Facing right = 1, Facing left = -1

    //Hits an enemy:
    //Applies knockback based on direction of player and knockback stat of equipped weapon
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            //Check direction of player
            if(this.transform.parent.transform.parent.GetComponent<PlayerController>().facingRight)
            {
                direction = 1;
            }
            else
            {
                direction = -1;
            }

            //Rolls for critical chance
            //Apply damage to enemy and if crit hit successful
            if(UnityEngine.Random.Range(0f, 1f) < this.transform.parent.transform.parent.GetComponent<Player>().equipment.GetComponent<Equipment>().weapon.criticalChance)
            {
                other.gameObject.GetComponent<Enemy>().ReceiveDamage((int) (this.transform.parent.transform.parent.GetComponent<Player>().equipment.GetComponent<Equipment>().weapon.damage * criticalHitMultiplier));
            }
            else
            {
                other.gameObject.GetComponent<Enemy>().ReceiveDamage(this.transform.parent.transform.parent.GetComponent<Player>().equipment.GetComponent<Equipment>().weapon.damage);
            }

            //Apply KnockBack to enemy
            switch (this.transform.parent.transform.parent.GetComponent<Player>().equipment.GetComponent<Equipment>().weapon.knockback)
            {
                case Item.Knockback.smallest:
                    other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction * smallestKnockBack, knockBackY));
                    break;
                case Item.Knockback.smaller:
                    other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction * smallerKnockBack, knockBackY));
                    break;
                case Item.Knockback.small:
                    other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction * smallKnockBack, knockBackY));
                    break;
                case Item.Knockback.medium:
                    other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction * mediumKnockBack, knockBackY));
                    break;
                case Item.Knockback.large:
                    other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction * largeKnockBack, knockBackY));
                    break;
                case Item.Knockback.largest:
                    other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction * largestKnockBack, knockBackY));
                    break;
            }
        }
    }
}
