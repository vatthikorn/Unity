/*
    Nathan Cruz

    Draws the GUI for the pause screen.
    Has a button to save the game.
    Has a button to quit the game to the main menu.

    Dependencies:
    PlayerController.cs - to determine when the player is on the pause screen (screenState)
*/

using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuScreen : MonoBehaviour {

    public GameObject player;
	public GameControl gameControl;
	public MainMenuControl mainMenu;

    public GUISkin buttonSkin;

    public const float centerAnchor = 0.5f;

    public const float buttonX = centerAnchor - buttonWidth / 2;
    public const float firstButtonY = 0.3f;

    public const float buttonWidth = 0.35f;
    public const float buttonHeight = 0.1f;

    public const float buttonSpacingY = 1.5f;

	void OnGUI()
    {
        if (player.GetComponent<PlayerController>().screenState == PlayerController.ScreenState.pause)
        {
            DrawMenuScreen();
        }
    }

	void DrawMenuScreen()
	{
		GUI.skin = buttonSkin;

		Rect button = new Rect(buttonX * Screen.width, firstButtonY * Screen.height, buttonWidth * Screen.width, buttonHeight * Screen.height);
		if (GUI.Button(button, "Save Game"))
		{
			//CALL THE FUNCTION TO SAVE THE GAME HERE
			gameControl.GetComponent<GameControl>().SaveGameData();
			#if UNITY_EDITOR
			EditorUtility.DisplayDialog ("Saving", "Game Data Saved", "OK");
			#else
			Application.Quit();
			#endif
		}

		button = new Rect(buttonX * Screen.width, firstButtonY * Screen.height + buttonHeight * Screen.height * buttonSpacingY, buttonWidth * Screen.width, buttonHeight * Screen.height);
		if(GUI.Button(button, "Load Game"))
		{
			//CALL THE FUNCTION TO SAVE THE GAME HERE
			//CALL THE FUNCTION TO QUT THE GAME HERE TO LOAD THE MAIN MENU SCREEN
//			mainMenu.enabled = true;
//			mainMenu.GetComponent<MainMenuControl>().ShowImage();

			// I changed the button to a "Load Game" button just to test loading
			gameControl.GetComponent<GameControl>().LoadGameData ();
		}

	}
}
