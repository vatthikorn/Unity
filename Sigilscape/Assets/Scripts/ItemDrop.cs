

using UnityEngine;
using System.Collections;

public class ItemDrop : MonoBehaviour {

    public GameObject itemDatabase;

    public int itemID;

    public float time = 0.5f;

    void Start()
    {
        itemID = this.transform.parent.GetComponent<Chest>().itemID;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = Sprite.Create(itemDatabase.GetComponent<ItemDatabase>().items[itemID].icon, new Rect(0, 0, itemDatabase.GetComponent<ItemDatabase>().items[itemID].icon.width, itemDatabase.GetComponent<ItemDatabase>().items[itemID].icon.height), new Vector2(0.5f,0.5f));
        this.gameObject.transform.localScale = new Vector3(.25f, .25f, 1);
    }

    void FixedUpdate()
    {
        
    }

    void FloatingUpAndDown()
    {

    }
}
