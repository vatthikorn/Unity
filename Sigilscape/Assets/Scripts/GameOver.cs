using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

	// Use this for initialization
	public void restart (int loadLevel)
	{
		SceneManager.LoadScene (loadLevel);
	}
}
