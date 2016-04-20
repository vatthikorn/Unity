/*
        Peter Nguyen

        This is for spawning a mob and after a certain amount of time it respawns if dead.
        It will only spawn 1 mob after 5 seconds then check if the mob is still alive.
        If it is not alive, it will respawn after a certain amount of time later.

        Input: mob

*/


using UnityEngine;
using System.Collections;

public class spawner3 : MonoBehaviour {

    public GameObject whatToSpawn;
    private int numberOfMobs = 1;
    public int enemyCount=0;
    Object enemy;
    public float timer;

	void Update ()
    {
        timer += Time.deltaTime;
        if (timer > 5 && enemyCount < numberOfMobs)
        {
            spawn();
            timer = 0;
            enemyCount++;
        }

        if (timer > 5 && enemy == null)
        {
            enemyCount--;
            timer = -25;
        }   
    }

    void spawn()
    {
        enemy = Instantiate(whatToSpawn, transform.position, Quaternion.identity);
    }

}
