/*
    Nathan Cruz

    NOT COMPLETE: A WHOLE LOT TO DO
    THE IDEA IS TO HAVE BE SIMILAR TO PLAYER.CS IN WHICH IT DETECT COLLSIONS FROM PLAYER ATTACKS OR PLAYER BULLETS
    UPON DETECTION, WILL RECEIVE DAMAGE, FORCE, OTHER EFFECTS, ETC.
    BE DESTROYED ONCE HEALTH REACHES ZERO
    MAYBE DROP AN ITEM AS WELL
    ALSO TO HAVE AN AI ATTACHED TO THIS

    This was just created for a test for parts of the game, but expand on it please if you have the time.
    WILL BE PART OF A MONSTERMANAGER.CS FOR EASY CLONING

    Handles switching to and fro between roaming and hunting.

    Remember to:
    Set the enemy's RigidBody to freeze rotation
*/

using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    //Must be referenced beforehand(only if you want the enemy to drop an item)
    public GameObject itemDrop;

    //Must be referenced beforehand
    public GameObject enemyRoamingDetector;
    public GameObject enemyHuntingDetector;

    public bool hunting = false;
    public bool alive;
    public int health;
    public int maxHealth;
    public int strength;
    public int defense;
    
    //Disables any item drops
    void Start()
    {
        itemDrop.SetActive(false);
        enemyHuntingDetector.SetActive(false);
        enemyRoamingDetector.SetActive(true);
        this.GetComponent<Enemy>().hunting = false;
    }

    //Damage Receiver
    //Drops an item (if enemy has an item) upon defeat
    //Destroys itself upon defeat
    public void ReceiveDamage(int damage)
    {
        health -= damage;

        if (health < 0)
            health = 0;

        //Spawns an item if ther is an item attached, and destroys itself
        if (health == 0)
        {
            if(itemDrop != null)
            {
                itemDrop.SetActive(true);
                this.transform.DetachChildren();   
            }
            Destroy(this.gameObject);
        }
    }

    //Called by EnemyRoamingDetector.cs
    public void ExecuteHunting()
    {
        this.GetComponent<Enemy>().hunting = true;
        enemyRoamingDetector.SetActive(false);
        enemyHuntingDetector.GetComponent<EnemyHuntingDetector>().timer = EnemyHuntingDetector.huntingTimer;
        enemyHuntingDetector.SetActive(true);        
    }

    //Called by EnemyHuntingDetector.cs
    public void GoBackToRoaming()
    {
        this.GetComponent<Enemy>().hunting = false;
        enemyHuntingDetector.SetActive(false);
        enemyRoamingDetector.SetActive(true);
    }
}
