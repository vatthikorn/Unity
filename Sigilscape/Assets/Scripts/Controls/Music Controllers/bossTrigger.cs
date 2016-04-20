using UnityEngine;
using System.Collections;

public class bossTrigger : MonoBehaviour {

	public GameObject musicController;

	public AudioSource boss;

	private AudioSource peaceTime;

	private AudioSource preBoss;

	void Awake() {

		peaceTime = this.musicController.GetComponent<SoundManager> ().peaceTime;

		preBoss = this.musicController.GetComponent<SoundManager> ().preBoss;

	}

	void OnTriggerEnter2D(Collider2D other) {

		if (other.tag == "Player") {
			//do stuff

			this.peaceTime.Stop ();
			this.preBoss.Stop ();


			this.boss.Play ();


		}
	}

//	void OnTriggerStay2D(Collider2D other) {
//
//		//if (other.tag == "Player") {
//			//Debug.Log ("Player enter!");
//
//			if (peaceTime.isPlaying) {
//				//Debug.Log ("PeaceTime Playing");
//				peaceTime.Pause ();
//			}
//
//			if (combat.isPlaying) {
//				//Debug.Log ("Combat playing");
//				combat.Stop ();
//			}
//
//			if (boss.isPlaying == false) {
//				boss.Play ();
//			}
//		//}
//
//	}

	void OnTriggerExit2D(Collider2D other) {

		if (other.tag == "Player") {

			Debug.Log ("Player exit");

			if (boss.isPlaying) {
				boss.Stop ();
			}

			if (peaceTime.isPlaying == false) {
				peaceTime.Play ();
			}
		}

	}


}
