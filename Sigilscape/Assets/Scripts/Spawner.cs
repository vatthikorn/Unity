using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {

    public const int maxEnemyCount = 5;
    public float timer = 120f;

    public List<GameObject> enemies = new List<GameObject>();
    public List<Object> enemiesAlive = new List<Object>();
    public GameObject room;
    public bool activated = false;
    public int enemyCount = 0;
    

    void Update()
    {
        if(enemyCount < maxEnemyCount)
        {
            enemyCount++;
            Spawn();
        }

        CheckEnemyCount();
    }

    void CheckEnemyCount()
    {
        List<int> enemyID = new List<int>();

        for(int i = 0; i < enemiesAlive.Count; i++)
        {
            if (enemiesAlive[i] == null)
                enemyID.Add(i);
        }

        for(int i = 0; i < enemyID.Count; i++)
        {
            enemiesAlive.Remove(enemiesAlive[i]);
            enemyCount--;
        }
    }

    void Spawn()
    {
        enemiesAlive.Add(Instantiate(enemies[0], this.gameObject.transform.position, Quaternion.identity));
    }

    /*void countDown()
    {
        timer -= Time.deltaTime;

        if(timer < 0)
        {
            timer = 0;
            activated = false;
        }
    }*/

}
