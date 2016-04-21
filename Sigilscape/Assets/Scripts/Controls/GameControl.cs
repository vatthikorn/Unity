/* 
	Jammel Benjamin

	Manages relevant game data for storage and retrieval (Save and Load)

	Dependencies:
		- Inventory.cs: get and set player inventory
		- ChestManager.cs: track map chests and if player opens them
		- Equipment.cs: track players collected equipment
		- ItemDropManager.cs: tracks any dropped items during the game
		- EnemyManager.cs: tracks currently spawned mobs
		- Enemy.cs: get enemy information (health, strength, defense)
		- Player.cs: get player information (health, defense, and position)
*/

using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;

[Serializable]
public struct Game
{
	public List<int> enemies;
	public List<int> rooms;
	public List<int> playerChests;
	public List<int> itemDrops;
	public List<int> inv;
	public List<int> playerEquipment;
	public int playerHealth;
	public float playerPositionx;
	public float playerPositiony;
	public int enemyHealth;
	public int enemyDefense;
	public int enemyStrength;
};


[Serializable]
public class GameControl : MonoBehaviour {

	public GameObject playerItems;
	public GameObject playerChests;
	public GameObject playerEquipment;
	public GameObject playerInventory;
	public GameObject enemyManager;
	public GameObject roomState;
	public GameObject player;
	public GameObject enemy;


	public Game gameData = new Game();

	// Stores statistical game data upon launch
	void Start()
	{
		gameData = GetGameData ();
	}

	// Function to store game data upon request
	public Game GetGameData()
	{
		if (enemy != null) {
			gameData.enemyHealth = enemy.GetComponent<Enemy> ().GetEnemyHealth ();
			gameData.enemyDefense = enemy.GetComponent<Enemy> ().GetEnemyDefense ();
			gameData.enemyStrength = enemy.GetComponent<Enemy> ().GetEnemyStrength ();
		}
		gameData.enemies = enemyManager.GetComponent<EnemyManager> ().SaveEnemies ();
		gameData.rooms = roomState.GetComponent<RoomTracker> ().SaveRooms ();
		gameData.playerChests = playerChests.GetComponent<ChestManager> ().SaveChests ();
		gameData.itemDrops = playerItems.GetComponent<ItemDropManager> ().SaveItemDrops ();
		gameData.inv = playerInventory.GetComponent<Inventory> ().SaveInventory ();
		gameData.playerEquipment = playerEquipment.GetComponent<Equipment> ().SaveEquipment ();
		gameData.playerHealth = player.GetComponent<Player> ().GetPlayerHealth ();
		gameData.playerPositionx = player.GetComponent<Transform> ().position.x;
		gameData.playerPositiony = player.GetComponent<Transform> ().position.y;

		return gameData;

	}

	// Function to alter current game's data upon request
	public void SetGameData(Game data)
	{
		gameData.enemies = data.enemies;
		gameData.rooms = data.rooms;
		gameData.playerChests = data.playerChests;
		gameData.itemDrops = data.itemDrops;
		gameData.inv = data.inv;
		gameData.playerEquipment = data.playerEquipment;
		gameData.playerHealth = data.playerHealth;
		gameData.playerPositionx = data.playerPositionx;
		gameData.playerPositiony = data.playerPositiony;
		gameData.enemyHealth = data.enemyHealth;
		gameData.enemyDefense = data.enemyDefense;
		gameData.enemyStrength = data.enemyStrength;

		player.GetComponent<Player> ().SetHealth (gameData.playerHealth);
		player.GetComponent<Transform> ().position = new Vector3 (gameData.playerPositionx, gameData.playerPositiony, 0);

		if (enemy != null) {
			enemy.GetComponent<Enemy> ().SetEnemyHealth (gameData.enemyHealth);
			enemy.GetComponent<Enemy> ().SetEnemyDefense (gameData.enemyDefense);
			enemy.GetComponent<Enemy> ().SetEnemyStrength (gameData.enemyStrength);
		}

		playerEquipment.GetComponent<Equipment> ().LoadEquipment (gameData.playerEquipment);
		playerItems.GetComponent<ItemDropManager> ().LoadItemDrops (gameData.itemDrops);
		playerChests.GetComponent<ChestManager> ().LoadChests (gameData.playerChests);
		enemyManager.GetComponent<EnemyManager> ().LoadEnemies (gameData.enemies);
		roomState.GetComponent<RoomTracker> ().LoadRooms (gameData.rooms);
		playerInventory.GetComponent<Inventory> ().LoadInventory (gameData.inv);

	}

	// Stores current game data to a file
	public void SaveGameData()
	{
		gameData = GetGameData ();

		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/save.dat");

		//write to the file
		bf.Serialize (file, gameData);

		file.Close();

	}

	// Checks for an existing game data file and loads info
	public void LoadGameData()
	{
		if (File.Exists (Application.persistentDataPath + "/save.dat")) 
		{
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/save.dat", FileMode.Open);

			// Copy data from file and write to current game
			SetGameData((Game)bf.Deserialize(file));

			file.Close ();
		}
	}

}