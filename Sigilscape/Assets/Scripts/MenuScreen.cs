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
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuScreen : MonoBehaviour {

    public GameObject player;
	public GameControl gameControl;
	public MainMenuControl mainMenu;

    public GUISkin buttonSkin;

    public const float centerAnchor = 0.5f;
    
    public const float topAnchorY = 255;

    public const float buttonWidth = 400;
    public const float buttonHeight = 100;

    public const float buttonSpacingY = 50;

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

		Rect button = new Rect(Screen.width/2 - buttonWidth/2, Screen.height / 2 - topAnchorY, buttonWidth, buttonHeight);
		if (GUI.Button(button, "Save Game"))
		{
			//CALL THE FUNCTION TO SAVE THE GAME HERE
			gameControl.GetComponent<GameControl>().SaveGameData();
			#if UNITY_EDITOR
			EditorUtility.DisplayDialog ("Saving", "Game Data Saved", "OK");
			#endif
		}

		button = new Rect(Screen.width / 2 - buttonWidth / 2, Screen.height / 2 - topAnchorY + buttonHeight + buttonSpacingY, buttonWidth, buttonHeight);
		if(GUI.Button(button, "Load Game"))
		{
			//CALL THE FUNCTION TO SAVE THE GAME HERE
			//CALL THE FUNCTION TO QUT THE GAME HERE TO LOAD THE MAIN MENU SCREEN
//			mainMenu.enabled = true;
//			mainMenu.GetComponent<MainMenuControl>().ShowImage();

			// I changed the button to a "Load Game" button just to test loading
			gameControl.GetComponent<GameControl>().LoadGameData ();
		}

        button = new Rect(Screen.width / 2 - buttonWidth / 2, Screen.height / 2 - topAnchorY + buttonHeight * 2 + buttonSpacingY * 2, buttonWidth, buttonHeight);
        if (GUI.Button(button, "Quit to Main Screen"))
        {
            SceneManager.LoadScene(0);
        }

        button = new Rect(Screen.width / 2 - buttonWidth / 2, Screen.height / 2 - topAnchorY + buttonHeight * 3 + buttonSpacingY * 3, buttonWidth, buttonHeight);
        if (GUI.Button(button, "Quit to Home"))
        {
            Application.Quit();
        }

	}
}
