using UnityEngine;
using System.Collections;

public class BossBattleActivate : MonoBehaviour {

    public GameObject wall;
    public GameObject boss;

    void Start()
    {
        wall.SetActive(false);
    }

    void Update()
    {
        if(boss == null)
        {
            wall.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("My ass");

        if(other.gameObject.tag == "Player")
        {
            Debug.Log("IT SHOULD BE FUCKING ONNNNNNN FUCCUCUCUCUCK");
            wall.SetActive(true);
            
            if(boss != null)
            {
                boss.GetComponent<Enemy>().ExecuteHunting();
            }
        }
    }
}
