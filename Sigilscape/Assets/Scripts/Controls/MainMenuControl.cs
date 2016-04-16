using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuControl : MonoBehaviour {

	public GameObject canvas;

	//private GameObject howToPlay;

	void Start() {
	}

	// Use this for initialization
	public void ShowImage() {

		//howToPlay = this.canvas.GetComponent<GameObject> (); 

		Debug.Log ("Show Image here");

		canvas.SetActive (true);

	}


	public void HideImage() {

		Debug.Log ("Hide Image");

		canvas.SetActive (false);

	}

}
