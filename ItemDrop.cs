/*
    Nathan Cruz

    Any items dropped on screen by either chest or enemy.
    The item will appear on screen when "dropped" (SetActive by another object), and then fall to the ground.
    This script just handles the how the player will pick the object, and what sprite and size it should have.
    Need to enter the itemID beforehand.

    Dependency:
    ItemDatabase.cs & Item.cs - accesing information (everything*)
    Inventory.cs - placing item in inventory (AddItemFromDrop())

    Required:
    The itemDrop needs to be a child of a gameObject with a Chest script or enemy script (ENEMY PART NOT YET IMPLEMENTED)
    The ground needs to be in the "Ground" Layer.
    
    Remember to:
    The itemDrop gameObject should be placed in front of the chest gameObject (otherwise it will just look weird).
*/

using UnityEngine;
using System.Collections;

public class ItemDrop : MonoBehaviour {

    //Needs to be referenced in Unity
    public GameObject itemDatabase;
    public Transform groundCheck;

    //Needs to be changed based on final Standard naming conventions
    public string player = "Player";
    public string inventory = "Inventory";

    //Change on preference
    public const float fallRate = 0.05f;

    //Important to get item stats across from inventory and item database
    public int itemID;

    //Check for falling
    bool grounded;

    //Copies texture and scales image down to appropriate size
    void Start()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = Sprite.Create(itemDatabase.GetComponent<ItemDatabase>().items[itemID].icon, new Rect(0, 0, itemDatabase.GetComponent<ItemDatabase>().items[itemID].icon.width, itemDatabase.GetComponent<ItemDatabase>().items[itemID].icon.height), new Vector2(0.5f,0.5f));
        this.gameObject.transform.localScale = new Vector3(.25f, .25f, 1);
    }

    //Allows object to fall to the ground (without a rigid body) (we do not want the player to trip over it just because it has a rigidbody)
    void Update()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if(!grounded)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - fallRate, this.transform.position.z);
        }
    }

    //Player stand in front, picks it up with "E", it is destroyed from the game, and inventory handles placement.
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == ("Player") && Input.GetKeyDown(KeyCode.E))
        {
            GameObject.Find(player).transform.FindChild(inventory).GetComponent<Inventory>().AddItemFromDrop(itemID);
            Destroy(this.gameObject);
        }
    }
}
