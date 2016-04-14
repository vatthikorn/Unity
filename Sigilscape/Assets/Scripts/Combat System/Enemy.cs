/*
    Nathan Cruz

    NOT COMPLETE: A WHOLE LOT TO DO
    THE IDEA IS TO HAVE BE SIMILAR TO PLAYER.CS IN WHICH IT DETECT COLLSIONS FROM PLAYER ATTACKS OR PLAYER BULLETS
    UPON DETECTION, WILL RECEIVE DAMAGE, FORCE, OTHER EFFECTS, ETC.
    BE DESTROYED ONCE HEALTH REACHES ZERO
    ALSO TO HAVE AN AI ATTACHED TO THIS

    This was just created for a test for parts of the game, but expand on it please if you have the time.
    WILL BE PART OF A MONSTERMANAGER.CS FOR EASY CLONING

    Handles switching to and fro between roaming and hunting.
    Handles alerting Player.cs when it is in Combat and how many are in combat.
    Handles aggoring enemy when hit.

    Interface:
    void ReceiveDamage(int damage) - applies damage to enemy (PlayerRangedAttack.cs , PlayerAttack.cs)
    void ExecuteHunting() - switches to hunting mode (EnemyRoamingDetector.cs)
    void GoBackToRoaming() - switches to roaming mode (EnemyHuntingDetector.cs)

    Dependencies:
    Player.cs - switches player to battle mode or back to peace mode (Hunted(), Spared())

    Remember to:
    Set the enemy's RigidBody to freeze rotation
*/

using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    //Must be referenced beforehand(only if you want the enemy to drop an item)
    public GameObject itemDrop;

    //Must be referenced beforehand
    public GameObject player;

    public GameObject enemyRoamingDetector;
    public GameObject enemyHuntingDetector;

    public const float hurtAnimationTime = .3f;

    public bool action = true;
    public bool hunting = false;
    public bool alive;
    public int health;
    public int maxHealth;
    public int strength;
    public int defense;
    
    //Disables any item drops, sets up roaming mode
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
    //Aggroes enemy when hurt
    //Disables action temporarily
    public void ReceiveDamage(int damage)
    {
        action = false;
        Invoke("EnableAction", hurtAnimationTime);
        if(!hunting)
        {
            ExecuteHunting();
        }

        health -= damage;

        if (health < 0)
            health = 0;

        //Spawns an item if ther is an item attached, decrement hunting counter, and destroys itself
        if (health == 0)
        {
            if(itemDrop != null)
            {
                itemDrop.SetActive(true);
                Destroy(enemyRoamingDetector);
                Destroy(enemyHuntingDetector);
                this.transform.DetachChildren();   
            }
            player.GetComponent<Player>().Spared();
            Destroy(this.gameObject);
        }
    }

    void EnableAction()
    {
        action = true;
    }

    //Called by EnemyRoamingDetector.cs
    //Signals player is in combat
    public void ExecuteHunting()
    {
        player.GetComponent<Player>().Hunted();
        this.GetComponent<Enemy>().hunting = true;
        enemyRoamingDetector.SetActive(false);
        enemyHuntingDetector.GetComponent<EnemyHuntingDetector>().timer = EnemyHuntingDetector.huntingTimer;
        enemyHuntingDetector.SetActive(true);        
    }

    //Called by EnemyHuntingDetector.cs
    //Signals player is spared
    public void GoBackToRoaming()
    {
        player.GetComponent<Player>().Spared();
        this.GetComponent<Enemy>().hunting = false;
        enemyHuntingDetector.SetActive(false);
        enemyRoamingDetector.SetActive(true);
    }
}
