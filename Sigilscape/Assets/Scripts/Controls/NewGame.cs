using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class NewGame : MonoBehaviour {

	public static GameControl control;

	public void CreateNewGame() {
		SceneManager.LoadScene ("Entire Test");
	}
		
}
