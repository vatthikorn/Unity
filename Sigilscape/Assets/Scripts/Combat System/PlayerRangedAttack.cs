/*
    Nathan Cruz

    Handles collisions with enemies.
    Handles the range of the ranged weapon.
    Handles ranged weapons in general.

    Dependencies:
    Enemy.cs - apply daamge & knockback to them - (ReceiveDamage())
    Equipment.cs & Item.cs - (Range, damage, critical chance, knockback)

    Required:
    Attached to the projectile sample
*/

using UnityEngine;
using System.Collections;

public class PlayerRangedAttack : AttackVars {

    //Saves stats in case player switches weapon while this attack is still out
    public int damage;
    public float criticalChance;
    public Item.Range range;
    public Item.Knockback knockback;
    public Vector2 rangedWeaponSpeed = new Vector2(15, 0);

    //Limits the range projectile flies
    public const float longsRange = 8.0f;
    public const float longestRange = 12.0f;

    //Used to calculate distance traveled
    public float initialPositionX;
    public float distanceTraveled;

    //Saves the starting position
    void Start()
    {
        initialPositionX = this.gameObject.transform.position.x;
    }

    //Keeps speed steady, calculates traveled distance, despawns when distance reached
    void Update()
    {
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(direction * rangedWeaponSpeed.x, rangedWeaponSpeed.y);
        distanceTraveled = Mathf.Abs(this.gameObject.transform.position.x - initialPositionX);

        if(range == Item.Range.longs && distanceTraveled > longsRange)
        {
            Destroy(this.gameObject);
        }
        else if(range == Item.Range.longest && distanceTraveled > longestRange)
        {
            Destroy(this.gameObject);
        }
    }

    //Collides with enemy
    //Applies damage
    //Applies knockback
    //Destroys itself
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            //Rolls for critical chance
            //Apply damage to enemy and if crit hit successful
            if (UnityEngine.Random.Range(0f, 1f) < criticalChance)
            {
                other.gameObject.GetComponent<Enemy>().ReceiveDamage((int)(damage * criticalHitMultiplier));
            }
            else
            {
                other.gameObject.GetComponent<Enemy>().ReceiveDamage(damage);
            }

            //Apply KnockBack to enemy
            switch (knockback)
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

            //Destroy itself on impact
            Destroy(this.gameObject);
        }
    }
}
