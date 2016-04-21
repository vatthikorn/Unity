using UnityEngine;
using System.Collections;

public class TNTExplosion : MonoBehaviour {

    public GameObject wall;

	void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name.Contains("Fireball"))
        {
            Destroy(wall);
            Destroy(this.gameObject);
        }
    }

}
