using UnityEngine;
using System.Collections;

public class HiddenRoomTrigger : MonoBehaviour {


	public AudioSource hiddenRoom;

	public GameObject musicController;


	void OnTriggerEnter2D(Collider2D other) {

		if (other.tag == "Player") {

			this.musicController.GetComponent<SoundManager> ().peaceTime.Pause ();

			hiddenRoom.Play ();

		}

	}


	void OnTriggerExit2D(Collider2D other) {

		if (other.tag == "Player") {

			this.musicController.GetComponent<SoundManager> ().peaceTime.Play ();

			hiddenRoom.Stop ();

		}

	}

}
