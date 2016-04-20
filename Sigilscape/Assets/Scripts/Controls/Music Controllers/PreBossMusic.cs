using UnityEngine;
using System.Collections;

public class PreBossMusic : MonoBehaviour {

	public GameObject musicController;

	public AudioSource preBoss;

	private AudioSource peaceTime;

	private AudioSource combat;

	void Awake() {

		peaceTime = this.musicController.GetComponent<SoundManager> ().peaceTime;

		combat = this.musicController.GetComponent<SoundManager> ().combat;

	}

	void OnTriggerStay2D(Collider2D other) {

		if (other.tag == "Player") {
			Debug.Log ("Player enter!");
		
			if (peaceTime.isPlaying) {
				Debug.Log ("PeaceTime Playing");
				peaceTime.Pause ();
			}

			if (combat.isPlaying) {
				Debug.Log ("Combat playing");
				combat.Stop ();
			}
				
			if (preBoss.isPlaying == false) {
				preBoss.Play ();
			}
		}

	}

	void OnTriggerExit2D(Collider2D other) {
		preBoss.Stop ();

		if (this.musicController.GetComponent<SoundManager> ().boss.isPlaying == false) {
			peaceTime.Play ();
		}
	}
		
}
