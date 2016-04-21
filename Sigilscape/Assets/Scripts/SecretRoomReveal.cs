/*
    Attach to hidden room object.
    Keeps the room unrevealed when the player
*/
using UnityEngine;
using System.Collections;

public class SecretRoomReveal : MonoBehaviour {

    public GameObject wall;
    
    void Update()
    {
        if(this.gameObject.GetComponent<Room>().roomType != Room.RoomState.hidden)
        {
            wall.SetActive(false);
        }
    }	
	
}
