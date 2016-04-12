using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class GameControl : MonoBehaviour {

	public float sampleHealth = 100;

	// Use this for initialization
	void Start () {
	
		Debug.Log ("Save at " + Application.persistentDataPath);

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//Method to save data to a file
	public void Save() {

		BinaryFormatter bf = new BinaryFormatter ();

		FileStream file = File.Open (Application.persistentDataPath + "/save.dat", FileMode.Open);

		PlayerData data = new PlayerData ();

		data.stuffToSave = sampleHealth;

		//write to the file
		bf.Serialize (file, data);

		file.Close();
	}

	public void Load() {



	}


}

[Serializable]
class PlayerData {

	public float stuffToSave;

}