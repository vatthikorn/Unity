using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections;
using System.Collections.Generic;


public class GameControl : MonoBehaviour {


	public static GameControl control;

	public GameObject player;

	public GameObject inventory;

	//public GameObject playerEquipment;

	private int health;

	private float playerPositionX;
	private float playerPositionY;
	private float playerPositionZ;

	private List<Item> items = new List<Item>();
	private List<Item> equipment = new List<Item>();
	private List<Item> sigils = new List<Item>();
	private List<Item> consumables = new List<Item>();
	private List<Item> keyItems = new List<Item>();

	void Awake () {

		this.player.GetComponents<Player> ();

		this.inventory.GetComponents<Inventory> ();
	
		Debug.Log ("Save at " + Application.persistentDataPath);

		GetData ();
	}

	void GetData() {

		playerPositionX = player.transform.position.x;
		playerPositionY = player.transform.position.y;
		playerPositionZ = player.transform.position.z;

		health = player.GetComponent<Player> ().health;

		items = inventory.GetComponent<Inventory> ().items;
		equipment = inventory.GetComponent<Inventory> ().equipment;
		sigils = inventory.GetComponent<Inventory> ().sigils;
		consumables = inventory.GetComponent<Inventory> ().consumables;
		keyItems = inventory.GetComponent<Inventory> ().keyItems;

		//TODO: Get active sigils

		Debug.Log ("Players data retrieved");


		//Debug.Log("x: " + playerPositionX + ", y: " + playerPositionY + ", z: " + playerPositionZ);

	}

	//Method to save data to a file
	public void Save() {

		Debug.Log ("Saving...");

		BinaryFormatter bf = new BinaryFormatter ();

		FileStream file = File.Create (Application.persistentDataPath + "/save.dat", 1);

		PlayerData data = new PlayerData ();

		data.health = this.health;
		data.playerPositionX = this.playerPositionX;
		data.playerPositionY = this.playerPositionY;
		data.playerPositionZ = this.playerPositionZ;

		data.items = this.items;
		data.equipment = this.equipment;
		data.sigils = this.sigils;
		data.consumables = this.consumables;
		data.keyItems = this.keyItems;

		//write to the file
		bf.Serialize (file, data);

		file.Close();

		Debug.Log ("Player's data saved");
	}

	public void Load() {

		Debug.Log("Trying to load game...");

		if (File.Exists (Application.persistentDataPath + "/save.dat")) {

			BinaryFormatter bf = new BinaryFormatter ();

			FileStream file = File.Open (Application.persistentDataPath + "/save.dat", FileMode.Open);

			PlayerData data = (PlayerData)bf.Deserialize (file);

			file.Close ();

			//sampleHealth = data.stuffToSave;
		}

	}


}

[Serializable]
class PlayerData {

	public float health;

	public float playerPositionX;
	public float playerPositionY;
	public float playerPositionZ;

	public List<Item> items = new List<Item>();
	public List<Item> equipment = new List<Item>();
	public List<Item> sigils = new List<Item>();
	public List<Item> consumables = new List<Item>();
	public List<Item> keyItems = new List<Item>();

}