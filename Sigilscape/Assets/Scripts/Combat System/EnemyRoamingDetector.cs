/*
    Nathan Cruz

    Signals to the Enemy when the player has been detected by them.

    Dependencies:
    Enemy.cs - trigger hunting mode (ExecuteHunting())

    Required:
    Attach this to empty GameObject with a Collider2D
    Parent of the gameobject should be to an Enemy GameObject
    Player must have the "Player" tag
*/

using UnityEngine;
using System.Collections;

public class EnemyRoamingDetector : MonoBehaviour {

    //Must be referenced
    public GameObject enemy;

    //Player has been detected, proceed to hunt for their butt
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            enemy.GetComponent<Enemy>().ExecuteHunting();
        }
    }
}
