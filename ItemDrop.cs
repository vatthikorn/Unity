/*
    Nathan Cruz

    Any items dropped on screen by either chest or enemy (THE ENEMY PART IS NOT YET IMPLEMENTED).
    The item will appear on screen when "dropped" (SetActive by another object), and then fall to the ground.

    Dependency:
    Chest.cs
    ItemDatabase.cs

    Required:
    The itemDrop needs to be a child of a gameObject with a Chest script or enemy script (ENEMY PART NOT YET IMPLEMENTED)
    The ground needs to be in the "Ground" Layer.
    
    Remember to:
    The itemDrop gameObject should be placed in front of the chest gameObject.
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

    public int itemID;
    bool grounded;

    //Copies itemID of chest, used to determine sprite from ItemDatabase, scales it down appropriately.
    void Start()
    {
        itemID = this.transform.parent.GetComponent<Chest>().itemID;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = Sprite.Create(itemDatabase.GetComponent<ItemDatabase>().items[itemID].icon, new Rect(0, 0, itemDatabase.GetComponent<ItemDatabase>().items[itemID].icon.width, itemDatabase.GetComponent<ItemDatabase>().items[itemID].icon.height), new Vector2(0.5f,0.5f));
        this.gameObject.transform.localScale = new Vector3(.25f, .25f, 1);
    }

    //Allows object to fall to the ground (without a rigid body)
    void Update()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if(!grounded)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 0.1f, this.transform.position.z);
        }
    }

    //Player stand in front, picks it up with "E", it is destroyed from the game, and inventory handles placement.
    void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject.Find(player).transform.FindChild(inventory).GetComponent<Inventory>().AddItemFromDrop(itemID);
            Destroy(this.gameObject);
        }
    }
}
