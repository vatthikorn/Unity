/*
    Nathan Cruz

    Controls (Fight Screen):
    Attack - Left Click or K                    
    Defend - Right Click or J                   
    Dodge - Directional Button + Left Shift     
    Activate Sigil - {1,2,3,4}                  
    Use Healing Potion - Q                      
    Use Sigil Potion - R
    Activate environment - E                        
    Movement - WASD || Up/Left/Down/Right          
    Inventory - I  
    Map - M                                     
    Menu - ESC

    Interface:
    facingRight, action, ranged*  - allows enable/disable action, values concerning ranged attacks location - (Player.cs)
    screenState  - invetory screen - (Inventory.cs)

    Dependencies:
    Player.cs - attacking and blocking (Attack(), Shield())
    PlayerAttack.cs - get direction of force from attack (facingRight)
    Equipment.cs - (activateSigil1(), activateSigil2(), activateSigil3(), activateSigil4(), UseHealthPotion(), UseSigilPotion())

    Required:
    Attached to the player object.
    Anything that can be used as a floor is in the "Ground" layer (for jumping).
    pauseScreen, inventoryScreen, largeMap, miniMap (all canvases) are referenced.
    Player.cs and PlayerController.cs are attached to the same Player GameObject.
*/
using UnityEngine;
using System.Collections;
using System;

//-MS
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public enum ScreenState { fight, inventory, pause };//If the player is in combat, in the invnetory, or the pause menu
    public enum MapState { none, mini, large };//If the player has the no map, the miniMap, or largeMap on display

    public Animator anim;

    //All of these must be referenced
    public GameObject equipment;
    public GameObject player;
    public GameObject largeMap;
    public GameObject miniMap;
    public Transform groundCheck;//Object that is placed underneath the player
    public Transform leftWallCheck;
    public Transform rightWallCheck;

    public float deathAnimTime = .95f;

    //Limits movement
    public const float moveForce = 300f;//Horizontal Force
    public const float maxSpeed = 5f;//Horizontal Speed
    public const float jumpForce = 1000f;
    public const float wallJumpForce = 2000f;

    //How to place melee attack objects and shield away from player
    const float smallestMeleeOffsetRight = 0.36f;
    const float smallMeleeOffsetRight = 0.86f;
    const float mediumMeleeOffsetRight = 1.29f;
    const float largeMeleeOffsetRight = 1.5f;
    const float largestMeleeOffsetRight = 1.73f;
    const float shieldOffsetRight = 1.8f;
    public const float rangedOffsetRight = .88f;

    const float smallestMeleeOffsetLeft = -2.7f;
    const float smallMeleeOffsetLeft = -3.33f;
    const float mediumMeleeOffsetLeft = -3.73f;
    const float largeMeleeOffsetLeft = -3.84f;
    const float largestMeleeOffsetLeft = -4.22f;
    const float shieldOffsetLeft = -1.85f;
    public const float rangedOffsetLeft = -.97f;

    public const float rangedOffsetY = 0.32f;
    public Vector2 rangedSize = new Vector2(0.52f, 0.26f);

    //Restricts the actions the player can take for attacking, sigil use, shielding, dodging, using items to allow animation to complete
    public bool action = true;

    //Determines if the player can jump or is jumping
    public bool grounded = false;
    public bool leftWalled = false;
    public bool rightWalled = false;
    bool jump = false;
    public bool facingRight = true;

    //Deteremines what is displayed and if the game is paused or not (Default settings)
    public MapState mapState = MapState.mini;
    public ScreenState screenState = ScreenState.fight;
    public bool pauseGame = false;
    
	//find the panel for game over -MS
	public GameObject panel;
	public Button restartButton;
	public Button exitButton;

    private Rigidbody2D rb;

    //Dislays miniMap, fight screen on startup
	void Start () {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        MiniMapView();
        DisablePauseScreen();
        DisableInventoryScreen();

		//find panel for gameover -MS
		panel = GameObject.Find("GameOverPanel");
		restartButton = GameObject.Find ("RestartButton").GetComponent<Button>();
		exitButton = GameObject.Find ("ExitButton").GetComponent<Button>();
		panel.SetActive (false);
		restartButton.interactable = false;
		exitButton.interactable = false;
		Time.timeScale = 1;
    }
	
    //Determines if player can jump, the game is paused, and what screen the player is on and to which screen they can switch to
	void Update () {

        //Down - Jump: Checks if the player is on the ground to enable jumping
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        //GROUNDED ANIM
        anim.SetBool("grounded", grounded);

        if(facingRight)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }

        if (!pauseGame)
        {
			//if player health is 0 call game over
			if (player.GetComponent<Player>().health <= 0) {

                action = false;
                anim.SetBool("alive", false);
                Invoke("DeathIsDying", deathAnimTime);
			}

            //Jump
            if (((action && Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))) && grounded)
            {
                jump = true;
                MoonShoesSigil.Jump();
            }
            //Sigil Buttons
            else if(action && Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1))
            {
                equipment.GetComponent<Equipment>().activateSigil1();
            }
            else if (action && Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2))
            {
                equipment.GetComponent<Equipment>().activateSigil2();
            }
            else if (action && Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3))
            {
                equipment.GetComponent<Equipment>().activateSigil3();
            }
            else if (action && Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Alpha4))
            {
                equipment.GetComponent<Equipment>().activateSigil4();
            }
            //Consumables (Health and Sigil potion)
            else if(action && Input.GetKeyDown(KeyCode.Q))
            {
                equipment.GetComponent<Equipment>().UseHealthPotion();
            }
            else if(action && Input.GetKeyDown(KeyCode.R))
            {
                equipment.GetComponent<Equipment>().UseSigilPotion();
            }
            //Attack and defend (only if the player has a shield equipped)
            else if(action && ((Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.K))))
            {
                action = false;
                anim.SetBool("attacking", true);
                this.gameObject.GetComponent<Player>().Attack();
            }
            else if (action && equipment.GetComponent<Equipment>().shield.itemID != 0 && ((Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.J))))
            {
                action = false;
                this.gameObject.GetComponent<Player>().Shield();
            }
            //Dodging
            else if(action && Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.A))
            {
                anim.SetBool("dodging", true);
                action = false;
                facingRight = false;
                this.gameObject.GetComponent<Player>().DodgeLeft();
            }
            else if(action && Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.D))
            {
                anim.SetBool("dodging", true);
                action = false;
                facingRight = true;
                this.gameObject.GetComponent<Player>().DodgeRight();
            }

            //MOVING ANIM
            if (action && Math.Abs(Input.GetAxis("Horizontal")) > 0)
            {
                anim.SetBool("moving", true);
            }
            else
            {
                anim.SetBool("moving", false);
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
            if(action && h > 0)
            {
                facingRight = true;
            }
            else if (action && h < 0)
            {
                facingRight = false;
            }

            //Limits horizontal speed
            if (action && h * rb.velocity.x < maxSpeed)
            {
                rb.AddForce(Vector2.right * h * moveForce);
            }

            if (Mathf.Abs(rb.velocity.x) > maxSpeed)
            {
                rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
            }

            if (action && jump)
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
    }

    void DisablePauseScreen()
    {
        EnableMap();
        screenState = ScreenState.fight;
    }

    void EnableInventoryScreen()
    {
        DisableMap();
        screenState = ScreenState.inventory;
    }

    void DisableInventoryScreen()
    {
        EnableMap();
        screenState = ScreenState.fight;
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

    void DeathIsDying()
    {
        Time.timeScale = 0;
        panel.SetActive(true);
        exitButton.interactable = true;
        restartButton.interactable = true;
    }
}
