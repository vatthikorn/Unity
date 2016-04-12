/*
    Nathan Cruz

    This is attached to the weapon the player has. When the enemy comes into contact, this script will look into the player's equipment and see what weapon they have, look at the damage,
    and call the Enemy script to reduce the enemy's health. Look at knockBack of the weapon and apply the force to the enemy.
    Handles KnockBack.
    Handles Damage dealt to Enemy.
    Handles Critcal Chance.

    Dependencies:
    Enemy.cs - applies damage (ReceiveDamage())
    PlayerController.cs - to get direction of player (facingRight)
    Player.cs - to get equipment information (equipment)
    Equipment.cs - to get weapon information (weapon)
    Item.cs - (damage, range, criticalChance, knockBack)

    Required:
    The enemy GameObjects need to have the "Enemy" tag.
    The attack gameObjects should be a child to a gameObject that is a child to the Player gameObect (The parent of the attack is just a gameObject group together and organized all of the attack objects).

    Remembet to:
    Set isTrigger for all Melee Object to true
*/
using UnityEngine;
using System.Collections;
using System;

public class PlayerAttack : AttackVars {

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
