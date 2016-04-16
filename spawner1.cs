/*
    Peter Nguyen

    Spawner1 is used to spawn mobs once at a certain location
    
    Use: Create a game object for the spanwer.
         Create a child to the game object, this will be the location of the spawn.
         Add the prefab of the mob into the whatToSpawnPrefab and whatToSpawnClone.
*/



using UnityEngine;
using System.Collections;


public class spawner1 : MonoBehaviour
{

    public Transform[] spawnLocations;
    public GameObject[] whatToSpawnPrefab;
    public GameObject[] whatToSpawnClone;

    public int numberOfMobs;
    public int enemyCount = 1;
    float timer;

    void Start()
    {
        spawn();
    }

    void spawn()
    {
        whatToSpawnClone[0] = Instantiate(whatToSpawnPrefab[0], spawnLocations[0].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
        //whatToSpawnClone[1] = Instantiate(whatToSpawnPrefab[1], spawnLocations[1].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 5 && enemyCount < numberOfMobs - 1)
        {
            spawn();
            timer = 0;
            enemyCount++;
        }
    }
}
