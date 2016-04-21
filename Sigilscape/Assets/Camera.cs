using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {

    public GameObject player;

    void Update()
    {
        this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 3.73f, -12);
    }
}
