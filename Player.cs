/*
    Nathan Cruz

    NOT COMPLETE:
    THIS IS THE COMBAT PART OF THE PLAYER, NOT THE PLAYER CONTROLLER
    THE IDEA IS TO DETECT COLLSIONS WITH ENEMIES, DEDUCT HEALTH, REGEN HEALTH, DO SIGIL STUFF (MAYBE), ENABLE BLOCKS THAT ACT AS ATTACKS AND SHIELD
*/
using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    //Needs to be referenced prior
    public GameObject equipment;

    //GameObjects children to the player object. Disabled at first. Enabled upon attack.
    public GameObject smallestMelee;
    public GameObject smallMelee;
    public GameObject mediumMelee;
    public GameObject largeMelee;
    public GameObject largestMelee;

    public GameObject shield;

    public const float regenTime = 5f;

    public int health;
    public int maxHealth;
    public int healthRegen;
    public float timer = 0;    
    public bool inCombat;

    void Update()
    {
        if(!inCombat)
        {
            timer += Time.deltaTime;

            if(timer > regenTime)
            {
                timer -= regenTime;
                regenerateHealth();
            }
        }
    }

    //Called by the playercontroller, when player attacks
    public void Attack()
    {
        //Access equipment and determines type of range
        Item weapon = equipment.GetComponent<Equipment>().weapon;
        
        if(weapon.weaponType == Item.WeaponType.melee)
        {
            switch (weapon.range)
            {
                case Item.Range.smallest:
                    smallestMelee.SetActive(true);
                    smallestMelee.SetActive(false);
                    break;
                case Item.Range.small:
                    smallMelee.SetActive(true);
                    smallMelee.SetActive(false);
                    break;
                case Item.Range.medium:
                    mediumMelee.SetActive(true);
                    mediumMelee.SetActive(false);
                    break;
                case Item.Range.large:
                    largeMelee.SetActive(true);
                    largeMelee.SetActive(false);
                    break;
                case Item.Range.largest:
                    largestMelee.SetActive(true);
                    largestMelee.SetActive(false);
                    break;
            }
        }
    }

    //Activates shield for 0.2 seconds
    public void Shield()
    {
        shield.SetActive(true);
        Invoke("DisableShield", 0.2f);
    }

    //Disables shield
    void DisableShield()
    {
        shield.SetActive(false);
    }

    void regenerateHealth()
    {
        health += healthRegen;

        if (health > maxHealth)
            health = maxHealth;
    }

    //Detects enemy projectiles, attacks, and enemies, calls the damage receiver and applies force (knockback) to player
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy Projectile")
        {
            ReceiveDamage(other.gameObject.GetComponent<EnemyProjectile>().damage);
            this.gameObject.GetComponent<Rigidbody2D>().AddForce(other.gameObject.GetComponent<EnemyProjectile>().hitForce);
            Destroy(other.gameObject);
        }
        else if(other.gameObject.tag == "Enemy")
        {
            this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-2000, 0));
            ReceiveDamage(other.gameObject.GetComponent<Enemy>().strength);
        }
    }

    //Damage Receiver
    void ReceiveDamage(int damage)
    {
        health -= damage;

        if (health < 0)
            health = 0;

        if(health == 0)
        {
            //CALL GAMEOVER SOMETHING
        }
    }
}
