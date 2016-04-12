/*
    Nathan Cruz

    Handles damage reduction by shield.
    Handles collisions with shield and any enemy, enemy's projectile, enemy's attack.
    Handles KnockBack applied to player when shield interacts with anything enemy.

    Dependencies:
    EnemyProjectile.cs - get the damage upon collision (damage)
    Enemy.cs - get the damage upon collision (strength)
    Player.cs - apply damage upon collision (KnockBack(), ReceiveDamage())
    Equipment.cs & Item.cs - get the damage mitigation (shield)

    Remember To:
    Set isTrigger to true.
    Give the shield a RigidBody2D

*/
using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {

    //Needs to be referenced prior
    public GameObject Equipment;
    public GameObject Player;

    //Detects collision, reduces damage before applying it to player, applies KnockBack to player
	void OnTriggerEnter2D(Collider2D other)
    {
        Item Shield = Equipment.GetComponent<Equipment>().shield;

        if (other.gameObject.tag == "Enemy Projectile")
        {
            Player.GetComponent<Player>().KnockBackToPlayer(other);
            Player.GetComponent<Player>().ReceiveDamage((int) (other.gameObject.GetComponent<EnemyProjectile>().damage * (1.0f - (float) Shield.damageMitigation)));
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Enemy")
        {
            Player.GetComponent<Player>().KnockBackToPlayer(other);
            Player.GetComponent<Player>().ReceiveDamage((int)(other.gameObject.GetComponent<Enemy>().strength * (1.0f - (float) Shield.damageMitigation)));
        }
    }
}
