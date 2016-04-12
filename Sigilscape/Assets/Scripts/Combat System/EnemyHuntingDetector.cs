/*
    Nathan Cruz

    Signals to the enemy to switch back to roaming if the player is out of range for a certain time

    Dependencies:
    Enemy.cs - continues hunting mode until player escapes and triggers roaming mode (GoBackToRoaming())

    Required:
    Attach this to empty GameObject with a Collider2D
    Parent of the gameobject should be to an Enemy GameObject
    Player must have the "Player" tag
*/
using UnityEngine;
using System.Collections;

public class EnemyHuntingDetector : MonoBehaviour {

    //Must be referenced
    public GameObject enemy;

    //Time player needs to be out of range for enemy to sieze and desist
    public const float huntingTimer = 5.0f;

    //Time player has until enemy stops the chase
    public float timer;
    public bool timerReset = false;

    //Resets timer
    void Start()
    {
        timer = huntingTimer;
    }

    //Updates timer if player is out of range,
    //Timer runs out, enemy goes back to roaming
    void Update()
    {
        if(!timerReset)
        {
            timer -= Time.deltaTime;
        }

        if(timer < 0f)
        {
            timer = 0;
            enemy.GetComponent<Enemy>().GoBackToRoaming();
        }
    }

    //Player is within range, the timer is reset
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            timerReset = true;
            timer = huntingTimer;
        }
    }

    //Player is out of range, proceed countdown
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            timerReset = false;
        }
    }
}
