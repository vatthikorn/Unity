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


			//if (peaceTime.isPlaying) {
			peaceTime.Pause();
			//}

			if (combat.isPlaying == false) {
				combat.Play ();
			}

		} else {

			if (combat.isPlaying) {
				combat.Stop ();
			}

			if (!this.preBoss.isPlaying && !peaceTime.isPlaying && !boss.isPlaying && !hiddenRoom.isPlaying) {
				peaceTime.Play ();
			}

		}

	}
		

}

