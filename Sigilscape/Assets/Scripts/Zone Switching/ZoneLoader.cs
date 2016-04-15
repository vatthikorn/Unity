using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ZoneLoader : MonoBehaviour {


	//Call when a gameobject collides with this object
	void OnTriggerEnter2D(Collider2D other)
	{
		//Debug.Log ("Collided");
		//Check if the collided object is the player
		if (other.gameObject.CompareTag ("Player")) {
			Debug.Log ("Is player");
		
			//If so then load scene2
			SceneManager.LoadScene ("Entire Test 2");

		} else {

			Debug.Log (other.tag);
		}
	}


	// Use this for initialization
	void Awake () {
		//DontDestroyOnLoad (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
