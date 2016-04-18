using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public GameObject player;

	public AudioSource peaceTime;
	public AudioSource combat;
	public AudioSource hiddenRoom;
	public AudioSource boss;
	public AudioSource preBoss;
	public AudioSource spawners;

	// Use this for initialization
	void Start () {

		peaceTime.Play ();

	}



	// Update is called once per frame
	void Update () {

		if (this.player.GetComponent<Player> ().inCombat) {

			Debug.Log ("player in combat");

			if (peaceTime.isPlaying) {

				peaceTime.Stop ();

			}

			if (!combat.isPlaying) {
				combat.Play ();
			}

		} else {

			if (combat.isPlaying) {
				combat.Stop ();
			}

			if (!peaceTime.isPlaying) {
				peaceTime.Play ();
			}
				

		}

	
	}

}

