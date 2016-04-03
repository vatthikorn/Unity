/*
    Nathan Cruz

    NOT COMPLETE. NEEDS A LOT OF REFINEMENT TOO FOR THE THINGS ALREADY IMPLEMENTED. LIKE FOR EXAMPLE:
    NOT LETING THE PLAYER MASH THAT ATTACK BUTTON. IT DISGRACEFUL AND SHAMEFUL FOR A GAMER TO RESULT TO BUTTON MASHING.
    SERIOUSLY HAVE SOME CLASS. NEED SOMETHING TO RESTRICT THAT.

    Controls (Fight Screen):
    Attack - Left Click or K                    
    Defend - Right Click or J                   
    Dodge - Directional Button + Left Shift     NOT AT ALL IMPLEMENTED
    Activate Sigil - {1,2,3,4}                  NOT IMPLEMENTED
    Use Healing Potion - Q                      NOT IMPLEMENTED
    Use Sigil Potion - E                        NOT IMPLEMENTED
    Movement - WASD || Up/Left/Down/Right          
    Inventory - I  
    Map - M                                     
    Menu - ESC

    Dependencies:
    Player.cs

    Required:
    Attached to the player object.
    Anything that can be used as a floor is in the "Ground" layer (for jumping).
    pauseScreen, inventoryScreen, largeMap, miniMap (all canvases) are referenced.
    Player.cs and PlayerController.cs are attached to the same Player GameObject.
*/
using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour {

    public enum ScreenState { fight, inventory, pause };//If the player is in combat, in the invnetory, or the pause menu
    public enum MapState { none, mini, large };//If the player has the no map, the miniMap, or largeMap on display

    //All of these must be referenced
    public GameObject player;
    public GameObject pauseScreen;
    public GameObject inventoryScreen;
    public GameObject largeMap;
    public GameObject miniMap;
    public Transform groundCheck;//Object that is placed underneath the player

    //Limits movement
    public const float moveForce = 300f;//Horizontal Force
    public const float maxSpeed = 5f;//Horizontal Speed
    public const float jumpForce = 2000f;

    //How to place melee attack objects and shield away from player
    const float smallestMeleeOffsetRight = 0.36f;
    const float smallMeleeOffsetRight = 0.86f;
    const float mediumMeleeOffsetRight = 1.29f;
    const float largeMeleeOffsetRight = 1.5f;
    const float largestMeleeOffsetRight = 1.73f;
    const float shieldOffsetRight = 1.8f;

    const float smallestMeleeOffsetLeft = -2.7f;
    const float smallMeleeOffsetLeft = -3.33f;
    const float mediumMeleeOffsetLeft = -3.73f;
    const float largeMeleeOffsetLeft = -3.84f;
    const float largestMeleeOffsetLeft = -4.22f;
    const float shieldOffsetLeft = -1.85f;

    //Restricts the actions the player can take for attacking, sigil use, shielding, dodging, using items to allow animation to complete
    public bool action = true;

    //Determines if the player can jump or is jumping
    bool grounded = false;
    bool jump = false;
    public bool facingRight = true;

    //Deteremines what is displayed and if the game is paused or not (Default settings)
    MapState mapState = MapState.mini;
    ScreenState screenState = ScreenState.fight;
    bool pauseGame = false;
    
    private Rigidbody2D rb;

    //Dislays miniMap, fight screen on startup
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        MiniMapView();
        DisablePauseScreen();
        DisableInventoryScreen();
    }
	
    //Determines if player can jump, the game is paused, and what screen the player is on and to which screen they can switch to
	void Update () {

        //Down - Jump: Checks if the player is on the ground to enable jumping
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if(!pauseGame)
        {
            //Jump
            if ((Input.GetAxis("Vertical") > 0 || Input.GetKeyDown(KeyCode.Space)) && grounded)
            {
                jump = true;
            }
            //Sigil Buttons
            else if(Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1))
            {
                Debug.Log(1);
            }
            else if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2))
            {
                Debug.Log(2);
            }
            else if (Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3))
            {
                Debug.Log(3);
            }
            else if (Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Alpha4))
            {
                Debug.Log(4);
            }
            //Consumables (Health and Sigil potion)
            else if(Input.GetKeyDown(KeyCode.Q))
            {
                Debug.Log("Q");
            }
            else if(Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("E");
            }
            //Attack and defend
            else if(action && (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.K)))
            {
                action = false;
                this.gameObject.GetComponent<Player>().Attack();
            }
            else if (action && (Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.J)))
            {
                action = false;
                this.gameObject.GetComponent<Player>().Shield();
            }
        }

        //Menu Screen
        if(screenState != ScreenState.inventory && Input.GetKeyDown(KeyCode.Escape))
        {
            pauseGame = !pauseGame;

            if (pauseGame)
            {
                Time.timeScale = 0;
                EnablePauseScreen();
            }
            else
            {
                Time.timeScale = 1;
                DisablePauseScreen();
            }
        }

        //Iventory Screen
        if (screenState != ScreenState.pause && Input.GetKeyDown("i"))
        {
            pauseGame = !pauseGame;

            if (pauseGame)
            {
                Time.timeScale = 0;
                EnableInventoryScreen();
            }
            else
            {
                Time.timeScale = 1;
                DisableInventoryScreen();
            }
        }

        //Alternating maps views while on the fight screen
        if(screenState == ScreenState.fight && Input.GetKeyDown(KeyCode.M))
        {
            AlterMapView();
        }
	}

    //Calculates force of movement
    void FixedUpdate()
    {
        if(!pauseGame)
        {
            //Left/Right - Move
            float h = Input.GetAxis("Horizontal");

            //Switches the side the weapon and shield is on pending on which direction the player is facing
            if(h > 0)
            {
                facingRight = true;
                this.GetComponent<Player>().smallestMelee.transform.localPosition = new Vector2(smallestMeleeOffsetRight, 0);
                this.GetComponent<Player>().smallMelee.transform.localPosition = new Vector2(smallMeleeOffsetRight, 0);
                this.GetComponent<Player>().mediumMelee.transform.localPosition = new Vector2(mediumMeleeOffsetRight, 0);
                this.GetComponent<Player>().largeMelee.transform.localPosition = new Vector2(largeMeleeOffsetRight, 0);
                this.GetComponent<Player>().largestMelee.transform.localPosition = new Vector2(largestMeleeOffsetRight, 0);
                this.GetComponent<Player>().shield.transform.localPosition = new Vector2(shieldOffsetRight, 0);
            }
            else if (h < 0)
            {
                facingRight = false;
                this.GetComponent<Player>().smallestMelee.transform.localPosition = new Vector2(smallestMeleeOffsetLeft, 0);
                this.GetComponent<Player>().smallMelee.transform.localPosition = new Vector2(smallMeleeOffsetLeft, 0);
                this.GetComponent<Player>().mediumMelee.transform.localPosition = new Vector2(mediumMeleeOffsetLeft, 0);
                this.GetComponent<Player>().largeMelee.transform.localPosition = new Vector2(largeMeleeOffsetLeft, 0);
                this.GetComponent<Player>().largestMelee.transform.localPosition = new Vector2(largestMeleeOffsetLeft, 0);
                this.GetComponent<Player>().shield.transform.localPosition = new Vector2(shieldOffsetLeft, 0);
            }

            //Limits horizontal speed
            if (h * rb.velocity.x < maxSpeed)
            {
                rb.AddForce(Vector2.right * h * moveForce);
            }

            if (Mathf.Abs(rb.velocity.x) > maxSpeed)
            {
                rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
            }

            if (jump)
            {
                rb.AddForce(new Vector2(0f, jumpForce));
                jump = false;
            }
        }        
    }

    //Section deals with screens

    void EnablePauseScreen()
    {
        DisableMap();
        screenState = ScreenState.pause;
        pauseScreen.GetComponent<Canvas>().enabled = true;
    }

    void DisablePauseScreen()
    {
        EnableMap();
        screenState = ScreenState.fight;
        pauseScreen.GetComponent<Canvas>().enabled = false;
    }

    void EnableInventoryScreen()
    {
        DisableMap();
        screenState = ScreenState.inventory;
        inventoryScreen.GetComponent<Canvas>().enabled = true;
    }

    void DisableInventoryScreen()
    {
        EnableMap();
        screenState = ScreenState.fight;
        inventoryScreen.GetComponent<Canvas>().enabled = false;
    }

    //Section deals with the map

    void AlterMapView()
    {
        switch (mapState)
        {
            case MapState.none:
                MiniMapView();
                break;
            case MapState.mini:
                LargeMapView();
                break;
            case MapState.large:
                NoMapView();
                break;
        }
    }

    void NoMapView()
    {
        mapState = MapState.none;
        miniMap.GetComponent<Canvas>().enabled = false;
        largeMap.GetComponent<Canvas>().enabled = false;
    }

    void MiniMapView()
    {
        mapState = MapState.mini;
        miniMap.GetComponent<Canvas>().enabled = true;
        largeMap.GetComponent<Canvas>().enabled = false;
    }

    void LargeMapView()
    {
        mapState = MapState.large;
        miniMap.GetComponent<Canvas>().enabled = false;
        largeMap.GetComponent<Canvas>().enabled = true;
    }

    //These are only used when the player switches between the fight screen and inventor or menu

    void DisableMap()
    {
        miniMap.GetComponent<Canvas>().enabled = false;
        largeMap.GetComponent<Canvas>().enabled = false;
    }

    void EnableMap()
    {
        switch (mapState)
        {
            case MapState.none:
                NoMapView();
                break;
            case MapState.mini:
                MiniMapView();
                break;
            case MapState.large:
                LargeMapView();
                break;
        }
    }
}
