using UnityEngine;
using System.Collections;

public class Interface : MonoBehaviour {
	// The following sets of constants are used to define the starting top left corner (x,y) and the width and heihgt of gui components.
	// They are fractions that get multiplied by the screen width and height
	
	// Used for the main info box at the bottom right of the screen
	private const float BOX_X = .80f;
	private const float BOX_Y = .60f;
	private const float BOX_WIDTH = .20f;
	private const float BOX_HEIGHT = .40f;
	
	// Used for the "end turn" button
	private const float BUTTON_HEIGHT = .10f;
	
	// Used for the "h: help" box at the top right of the screen
	private const float HELP_BOX1_X = .95f;
	private const float HELP_BOX1_Y = 0;
	private const float HELP_BOX1_WIDTH = .05f;
	private const float HELP_BOX1_HEIGHT = .05f;
	
	// Used for the full help menu
	private const float HELP_BOX2_X = .50f;
	private const float HELP_BOX2_Y = 0;
	private const float HELP_BOX2_WIDTH = .50f;
	private const float HELP_BOX2_HEIGHT = BOX_Y;
	
	// The box that displays the win message
	private const float WIN_BOX_X = .25f;
	private const float WIN_BOX_Y = .25f;
	private const float WIN_BOX_WIDTH = .5f;
	private const float WIN_BOX_HEIGHT = .5f;
	
	// Used for the "game over" label
	private const float WIN_LABEL_X = .28f;
	private const float WIN_LABEL_Y = .28f;
	private const float WIN_LABEL_WIDTH = .44f;
	private const float WIN_LABEL_HEIGHT = .44f;
	
	// Used for the specific game over message (win, loss, tie)
	private const float WIN_MESSAGE_X = .28f;
	private const float WIN_MESSAGE_Y = .5f;
	private const float WIN_MESSAGE_WIDTH = .44f;
	private const float WIN_MESSAGE_HEIGHT = .25f;
	
	// Used for the "play again" button in the game over screen
	private const float WIN_BUTTON_X = .28f;
	private const float WIN_BUTTON_Y = .65f;
	private const float WIN_BUTTON_WIDTH = .44f;
	private const float WIN_BUTTON_HEIGHT = .10f;
	
	// The strings that appear in the help menu
	private string[] HELP_LABELS ={
		"Controls:",
		"w,a,s,d: pan camera",
		"click mouse (context sensitive):",
		"\tselect unit",
		"\tmove unit",
		"\tselect terrain (view info)",
		"h: close help",
		"",
		"Objective:",
		"Use your radioactive child to capture 3 radioactive spots.",
		"before the AI captures 3 spots"
	};
	
	// constants used to determine which format the info box will display things in
	private const int INFO_TYPE_NONE = 0;
	private const int INFO_TYPE_UNIT = 1;
	private const int INFO_TYPE_TERRAIN = 2;
	
	// Unit info indices
	private const int I_HEALTH = 0;
	private const int I_POWER = 1;
	private const int I_MOVEMENT = 2;
	private const int I_TEAM = 3;
	
	// Array of labels for unit attributes
	private string[] UNIT_LABELS = new string[]{
		"Health: ",
		"Attack Power: ",
		"Movement: ",
		"Team: "
	};
	
	// array of labels for terrain attributes
	private string[] TERRAIN_LABELS = new string[]{	
		"Movement Cost: ",
		"Team: "
	};
	
	// the keycode for the in game help key
	private const KeyCode KEY_HELP = KeyCode.H;
	
	// determines when to display help and which type of information to display respectively
	private bool displayHelp = false;
	private int infoType;
	
	// used for storing the information to be displayed
	private float[] unitInfo;
	private float[] terrainInfo;
	private string selectedName = "";
	
	// GUI styles for custom text
	private GUIStyle titleStyle = null; 
	private GUIStyle normalStyle = null;
	private GUIStyle superStyle = null;
	
	// constants for determining which win message to display
	//**Note: a tie would occurr if both radioChild objects died, but combat was never implemented
	public const int TEAM_NONE = 0;
	public const int TEAM_PLAYER = 1;
	public const int TEAM_AI = 2;
	public const int TEAM_TIE = 3;
	
	// The UI has a win status because we didn't implement it anywhere else and I needed it
	private int winStatus = TEAM_NONE;
	
	// Update is called once per frame
	void Update () {
		// Checks to see if the help key has been pressed: toggles the help display
		if(Input.GetKeyDown(KEY_HELP)){
			displayHelp = !displayHelp;
		}
	}
	
	void OnGUI(){		
		// If one style is null then they are all null. Initializes my styles (needs to be done in OnGUI and not in OnStart)
		if(titleStyle == null){
			titleStyle = new GUIStyle();
			titleStyle.fontStyle = FontStyle.Bold;
			titleStyle.normal.textColor = Color.white;
			titleStyle.fontSize = 16;
			titleStyle.alignment = TextAnchor.UpperCenter;	
			
			normalStyle = new GUIStyle();
			normalStyle.fontStyle = FontStyle.Normal;
			normalStyle.normal.textColor = Color.white;
			normalStyle.fontSize = 16;
			normalStyle.alignment = TextAnchor.UpperLeft;
			
			superStyle = new GUIStyle();
			superStyle.fontStyle = FontStyle.Normal;
			superStyle.normal.textColor = Color.white;
			superStyle.fontSize = 36;
			superStyle.alignment = TextAnchor.UpperCenter;
		}
		
		// Checks the win status and displays the appropriate information if a winner has been declared
		if(winStatus > TEAM_NONE){
			// Displayes a big label that says "Game Over" inside of a box
			superStyle.normal.textColor = Color.white;
			GUI.Box(new Rect(Screen.width*WIN_BOX_X,Screen.height*WIN_BOX_Y,Screen.width*WIN_BOX_WIDTH,Screen.height*WIN_BOX_HEIGHT),"");
			GUI.Label(new Rect(Screen.width*WIN_LABEL_X,Screen.height*WIN_LABEL_Y,Screen.width*WIN_LABEL_WIDTH,Screen.height*WIN_LABEL_HEIGHT),"Game Over",superStyle);
			
			// Checks the win status and picks the appropriate message
			string winLabel = "";
			switch(winStatus){
			case TEAM_PLAYER:
				superStyle.normal.textColor = Color.green;
				winLabel = "Player Wins!";
				break;
			case TEAM_AI:
				superStyle.normal.textColor = Color.red;
				winLabel = "AI Wins!";
				break;
			case TEAM_TIE:
				winLabel = "Draw";
				break;
			}
			
			// Displays the win message
			GUI.Label(new Rect(Screen.width*WIN_MESSAGE_X,Screen.height*WIN_MESSAGE_Y,Screen.width*WIN_MESSAGE_WIDTH,Screen.height*WIN_MESSAGE_HEIGHT),winLabel,superStyle);
			
			// Button that prompts the user to play again. Reloads the scene if pressed
			if(GUI.Button(new Rect(Screen.width*WIN_BUTTON_X,Screen.height*WIN_BUTTON_Y,Screen.width*WIN_BUTTON_WIDTH,Screen.height*WIN_BUTTON_HEIGHT),"Play Again!")){
				Application.LoadLevel(0);
			}
			
			return;
		}
		
		// Displays the help box, or a smaller box telling the player which key will open the help box
		if(displayHelp){
			// Creates a box to "contain" the information
			GUI.Box(new Rect(Screen.width*HELP_BOX2_X,Screen.height*HELP_BOX2_Y,Screen.width*HELP_BOX2_WIDTH,Screen.height*HELP_BOX2_HEIGHT),"");
			
			// Based on how many strings are in the array of help labels, it computes a uniform size for each label and makes a label for each string
			float labelHeight = HELP_BOX2_HEIGHT/HELP_LABELS.Length;
			for(int i = 0; i < HELP_LABELS.Length; i++){
				GUI.Label(new Rect(Screen.width*(HELP_BOX2_X + .01f),Screen.height*HELP_BOX2_Y + i*Screen.height*labelHeight,Screen.width*HELP_BOX2_WIDTH,Screen.height*labelHeight),HELP_LABELS[i],normalStyle);
			}
		}else{
			// The simple box with "h: help" written on it
			GUI.Box(new Rect(Screen.width*HELP_BOX1_X,Screen.height*HELP_BOX1_Y,Screen.width*HELP_BOX1_WIDTH,Screen.height*HELP_BOX1_HEIGHT),"h: help");
		}
		
		// The normal box for displaying information in game
		GUI.Box(new Rect(Screen.width*BOX_X,Screen.height*BOX_Y,Screen.width*BOX_WIDTH,Screen.height*BOX_HEIGHT), "");
		
		// A button in that box for the player to end their turn
		if(GUI.Button(new Rect(Screen.width*BOX_X,Screen.height*BOX_Y + Screen.height*(BOX_HEIGHT - BUTTON_HEIGHT),Screen.width*BOX_WIDTH,Screen.height*BUTTON_HEIGHT),"End Turn")){
			this.gameObject.SendMessage("changeFromPlayerOne");
		}
		
		// Gets the type of information to be displayed and displays it
		switch(infoType){
		case INFO_TYPE_NONE:
			break;
		case INFO_TYPE_UNIT:
			if(unitInfo != null && unitInfo.Length > 0){
				float labelHeight = (BOX_HEIGHT - BUTTON_HEIGHT)/(unitInfo.Length + 1);
				// prints the name of the unit
				GUI.Label(new Rect(Screen.width*BOX_X,Screen.height*BOX_Y,Screen.width*BOX_WIDTH,Screen.height*labelHeight),selectedName,titleStyle);
				// Using the computed labelHeight, it makes uniform labels for each attribute
				for(int i = 0; i < unitInfo.Length; i++){
					if(i == I_TEAM){
						string team = "Player";
						if(unitInfo[I_TEAM] == 2){
							team = "AI";	
						}
						GUI.Label(new Rect(Screen.width*BOX_X,Screen.height*BOX_Y + Screen.height*(i+1)*labelHeight,Screen.width*BOX_WIDTH,Screen.height*labelHeight),UNIT_LABELS[i] + team,normalStyle);
					}else{
						GUI.Label(new Rect(Screen.width*BOX_X,Screen.height*BOX_Y + Screen.height*(i+1)*labelHeight,Screen.width*BOX_WIDTH,Screen.height*labelHeight),UNIT_LABELS[i] + unitInfo[i],normalStyle);
			
					}
				}
			}
			break;
		case INFO_TYPE_TERRAIN:
			// Works exactly the same as the unitInfo, but uses terrainInfo
			if(terrainInfo != null && terrainInfo.Length > 0){
				float labelHeight = (BOX_HEIGHT - BUTTON_HEIGHT)/(terrainInfo.Length + 4);
				GUI.Label(new Rect(Screen.width*BOX_X,Screen.height*BOX_Y,Screen.width*BOX_WIDTH,Screen.height*labelHeight),selectedName,titleStyle);
				for(int i = 0; i < terrainInfo.Length; i++){
					if(i == 1){
						if(selectedName == "Goal"){
							string teamName = "Player";
							if(terrainInfo[i] == 2){
								teamName = "AI";
							}else if(terrainInfo[i] == 0){
								teamName = "Neutral";	
							}
							GUI.Label(new Rect(Screen.width*BOX_X,Screen.height*BOX_Y + Screen.height*(i+1)*labelHeight,Screen.width*BOX_WIDTH,Screen.height*labelHeight),TERRAIN_LABELS[i] + teamName,normalStyle);					
						}
					}else{
						GUI.Label(new Rect(Screen.width*BOX_X,Screen.height*BOX_Y + Screen.height*(i+1)*labelHeight,Screen.width*BOX_WIDTH,Screen.height*labelHeight),TERRAIN_LABELS[i] + terrainInfo[i],normalStyle);					
					}
				}
			}
			break;
		}
	}
	
	// called by a selected unit to display its information
	private void updateUnitInfo(float[] unitInfo){
		this.unitInfo = unitInfo;
		infoType = INFO_TYPE_UNIT;
	}
	
	// SendMessage only accepts one parameter, and everyone's scripts were storing unit/terrain types as strings
	// this method is expected t0 be called immediately after updateUnitInfo or updateTerrainInfo
	private void updateName(string name){
		if(name != "radioChild" && name != "Default"){
			selectedName = char.ToUpper(name.ToCharArray()[0]) + name.Substring(1);	
		}else if(name == "radioChild"){
			selectedName = "Radio Child";	
		}else{
			selectedName = "Dirt";	
		}
		
	}
	
	// called by selected terrain tiles to store their informaiton to be displayed
	private void updateTerrainInfo(float[] terrainInfo){
		this.terrainInfo = terrainInfo;
		infoType = INFO_TYPE_TERRAIN;
	}
	
	// Clears the info box
	private void cancelInfo(){
		infoType = INFO_TYPE_NONE;
	}
	
	// Sets the win status so a win box will be displayed
	private void winner(int team){
		winStatus = team;
	}
}
