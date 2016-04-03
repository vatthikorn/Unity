/*
    Nathan Cruz

    NOT COMPLETE:
    THE IDEA IS TO DETECT COLLSIONS WITH ENEMIES, DEDUCT HEALTH, DO SIGIL STUFF (MAYBE), ENABLE BLOCKS THAT ACT AS ATTACKS AND SHIELD

    Handles Weapon's range, attackspeed.
    Handles damage dealt to player, and health regeneration.
    Handles health Regeneration.
    Handles damage reduction by shield.
    Handles KnockPlayer to player from an enemy attack.

    Dependencies:
    Enemy.cs
    Equipment.cs
    PlayerController.cs

    Required:
    Player.cs and PlayerController.cs are attached to the same Player GameObject.

    Remember to:
    Set the player's RigidBody to freeze rotation
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

    //Regenerate healthRegen per regenTime
    public const float regenTime = 5f;

    //Time for delay in attack (attackSpeed), animating attack, shield time
    public const float animationAttackTime = 0.15f;
    public const float slowestAttackTime = 0.35f;
    public const float slowAttackTime = 0.27f;
    public const float mediumAttackTime = 0.20f;
    public const float fastAttackTime = 0.12f;
    public const float fastestAttackTime = 0.05f;
    public const float shieldTime = 0.2f;

    //KnockBack to player by an enemy attack
    public Vector2 PlayerKnockBack = new Vector2(2000f, 100f);

    public int health;
    public int maxHealth;
    public int healthRegen;
    public float timer = 0;    
    public bool inCombat;
    
    int direction;//Facing right = 1, Facing left = -1
   
    void Update()
    {
        //Regenerate health outside of combat
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
        
        //Enables the PlayerAttack gameobject for appropriate range
        if(weapon.weaponType == Item.WeaponType.melee)
        {
            switch (weapon.range)
            {
                case Item.Range.smallest:
                    switch (weapon.attackSpeed)
                    {
                        case Item.AttackSpeed.slowest:
                            Invoke("EnableSmallestMelee", slowestAttackTime);
                            break;
                        case Item.AttackSpeed.slow:
                            Invoke("EnableSmallestMelee", slowAttackTime);
                            break;
                        case Item.AttackSpeed.medium:
                            Invoke("EnableSmallestMelee", mediumAttackTime);
                            break;
                        case Item.AttackSpeed.fast:
                            Invoke("EnableSmallestMelee", fastAttackTime);
                            break;
                        case Item.AttackSpeed.fastest:
                            Invoke("EnableSmallestMelee", fastestAttackTime);
                            break;
                    }
                    break;
                case Item.Range.small:
                    switch (weapon.attackSpeed)
                    {
                        case Item.AttackSpeed.slowest:
                            Invoke("EnableSmallMelee", slowestAttackTime);
                            break;
                        case Item.AttackSpeed.slow:
                            Invoke("EnableSmallMelee", slowAttackTime);
                            break;
                        case Item.AttackSpeed.medium:
                            Invoke("EnableSmallMelee", mediumAttackTime);
                            break;
                        case Item.AttackSpeed.fast:
                            Invoke("EnableSmallMelee", fastAttackTime);
                            break;
                        case Item.AttackSpeed.fastest:
                            Invoke("EnableSmallMelee", fastestAttackTime);
                            break;
                    }
                    break;
                case Item.Range.medium:
                    switch (weapon.attackSpeed)
                    {
                        case Item.AttackSpeed.slowest:
                            Invoke("EnableMediumMelee", slowestAttackTime);
                            break;
                        case Item.AttackSpeed.slow:
                            Invoke("EnableMediumMelee", slowAttackTime);
                            break;
                        case Item.AttackSpeed.medium:
                            Invoke("EnableMediumMelee", mediumAttackTime);
                            break;
                        case Item.AttackSpeed.fast:
                            Invoke("EnableMediumMelee", fastAttackTime);
                            break;
                        case Item.AttackSpeed.fastest:
                            Invoke("EnableMediumMelee", fastestAttackTime);
                            break;
                    }
                    break;
                case Item.Range.large:
                    switch (weapon.attackSpeed)
                    {
                        case Item.AttackSpeed.slowest:
                            Invoke("EnableLargeMelee", slowestAttackTime);
                            break;
                        case Item.AttackSpeed.slow:
                            Invoke("EnableLargeMelee", slowAttackTime);
                            break;
                        case Item.AttackSpeed.medium:
                            Invoke("EnableLargeMelee", mediumAttackTime);
                            break;
                        case Item.AttackSpeed.fast:
                            Invoke("EnableLargeMelee", fastAttackTime);
                            break;
                        case Item.AttackSpeed.fastest:
                            Invoke("EnableLargeMelee", fastestAttackTime);
                            break;
                    }
                    break;
                case Item.Range.largest:
                    switch (weapon.attackSpeed)
                    {
                        case Item.AttackSpeed.slowest:
                            Invoke("EnableLargestMelee", slowestAttackTime);
                            break;
                        case Item.AttackSpeed.slow:
                            Invoke("EnableLargestMelee", slowAttackTime);
                            break;
                        case Item.AttackSpeed.medium:
                            Invoke("EnableLargestMelee", mediumAttackTime);
                            break;
                        case Item.AttackSpeed.fast:
                            Invoke("EnableLargestMelee", fastAttackTime);
                            break;
                        case Item.AttackSpeed.fastest:
                            Invoke("EnableLargestMelee", fastestAttackTime);
                            break;
                    }
                    break;
            }
        }
    }

    //Enables shield, then disables
    public void Shield()
    {
        shield.SetActive(true);
        Invoke("DisableShield", shieldTime);
    }

    //Disables shield, Enables action upon completion.
    void DisableShield()
    {
        shield.SetActive(false);
        this.GetComponent<PlayerController>().action = true;
    }

    //Enables Melee Attack gameobject. Is delayed by AttackSpeed. Calls to Disable with delay
    void EnableSmallestMelee()
    {
        smallestMelee.SetActive(true);
        Invoke("DisableSmallestMelee", animationAttackTime);
    }

    void EnableSmallMelee()
    {
        smallMelee.SetActive(true);
        Invoke("DisableSmallMelee", animationAttackTime);
    }

    void EnableMediumMelee()
    {
        mediumMelee.SetActive(true);
        Invoke("DisableMediumMelee", animationAttackTime);
    }

    void EnableLargeMelee()
    {
        largeMelee.SetActive(true);
        Invoke("DisableLargeMelee", animationAttackTime);
    }

    void EnableLargestMelee()
    {
        largestMelee.SetActive(true);
        Invoke("DisableLargestMelee", animationAttackTime);
    }
    //End Enable Weapons

    //Disables Melee Atack gameobject. Is delayed for animation completion. Enables action upon completion.
    void DisableSmallestMelee()
    {
        smallestMelee.SetActive(false);
        this.GetComponent<PlayerController>().action = true;
    }

    void DisableSmallMelee()
    {
        smallMelee.SetActive(false);
        this.GetComponent<PlayerController>().action = true;
    }

    void DisableMediumMelee()
    {
        mediumMelee.SetActive(false);
        this.GetComponent<PlayerController>().action = true;
    }

    void DisableLargeMelee()
    {
        largeMelee.SetActive(false);
        this.GetComponent<PlayerController>().action = true;
    }

    void DisableLargestMelee()
    {
        largestMelee.SetActive(false);
        this.GetComponent<PlayerController>().action = true;
    }
    //end DisableWeapons
    
    //Applies force and damage of enemy projectiles
    //Applies force and damage of enemy when in contact
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy Projectile")
        {
            KnockBackToPlayer(other);
            ReceiveDamage(other.gameObject.GetComponent<EnemyProjectile>().damage);
            Destroy(other.gameObject);
        }
        else if(other.gameObject.tag == "Enemy")
        {
            KnockBackToPlayer(other);
            ReceiveDamage(other.gameObject.GetComponent<Enemy>().strength);
        }
    }

    //Applies a force depending on point on point of contact to player for KnockBack
    public void KnockBackToPlayer(Collision2D other)
    {
        if (other.contacts[0].point.x > this.transform.position.x)
        {
            this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-PlayerKnockBack.x, PlayerKnockBack.y));
        }
        else
        {
            this.gameObject.GetComponent<Rigidbody2D>().AddForce(PlayerKnockBack);
        }
    }

    public void KnockBackToPlayer(Collider2D other)
    {
        if (other.bounds.center.x > this.transform.position.x)
        {
            this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-PlayerKnockBack.x, PlayerKnockBack.y));
        }
        else
        {
            this.gameObject.GetComponent<Rigidbody2D>().AddForce(PlayerKnockBack);
        }
    }

    //Regenerates health but to a cap
    void regenerateHealth()
    {
        health += healthRegen;

        if (health > maxHealth)
            health = maxHealth;
    }

    //Damage Receiver, Can be called by Shield
    public void ReceiveDamage(int damage)
    {
        int reducedDamage = damageReductionByArmor(damage);

        health -= reducedDamage;

        if (health < 0)
            health = 0;

        if(health == 0)
        {
            //CALL GAMEOVER SOMETHING
        }
    }

    //Handles damage reduction by armor
    int damageReductionByArmor(int damage)
    {
        return (int) (damage * (1.0f - (float) (equipment.GetComponent<Equipment>().armor.defense) / (float) (equipment.GetComponent<Equipment>().armor.defense + 100)));
    }
}
