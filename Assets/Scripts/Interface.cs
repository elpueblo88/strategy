using UnityEngine;
using System.Collections;

public class Interface : MonoBehaviour {
	private const float BOX_X = .80f;
	private const float BOX_Y = .60f;
	private const float BOX_WIDTH = .20f;
	private const float BOX_HEIGHT = .40f;

	private const float BUTTON_HEIGHT = .10f;
	
	private const float HELP_BOX1_X = .95f;
	private const float HELP_BOX1_Y = 0;
	private const float HELP_BOX1_WIDTH = .05f;
	private const float HELP_BOX1_HEIGHT = .05f;
	
	private const float HELP_BOX2_X = .50f;
	private const float HELP_BOX2_Y = 0;
	private const float HELP_BOX2_WIDTH = .50f;
	private const float HELP_BOX2_HEIGHT = BOX_Y;
	
	private const float WIN_BOX_X = .25f;
	private const float WIN_BOX_Y = .25f;
	private const float WIN_BOX_WIDTH = .5f;
	private const float WIN_BOX_HEIGHT = .5f;
	
	private const float WIN_LABEL_X = .28f;
	private const float WIN_LABEL_Y = .28f;
	private const float WIN_LABEL_WIDTH = .44f;
	private const float WIN_LABEL_HEIGHT = .44f;
	
	private const float WIN_MESSAGE_X = .28f;
	private const float WIN_MESSAGE_Y = .5f;
	private const float WIN_MESSAGE_WIDTH = .44f;
	private const float WIN_MESSAGE_HEIGHT = .25f;

	private const float WIN_BUTTON_X = .28f;
	private const float WIN_BUTTON_Y = .65f;
	private const float WIN_BUTTON_WIDTH = .44f;
	private const float WIN_BUTTON_HEIGHT = .10f;
	
	private string[] HELP_LABELS ={
		"Controls:",
		"w,a,s,d: pan camera",
		"click mouse (context sensitive):",
		"\tselect unit",
		"\tmove unit",
		"\tselect unit to attack",
		"\tselect terrain (view info)",
		"h: close help",
		"",
		"Objective:",
		"Use your radioactive child to capture 3 radioactive spots.",
		"OR: Kill all enemy units.",
		"Tie: both radio-child units die."
	};
	
	private const int INFO_TYPE_NONE = 0;
	private const int INFO_TYPE_UNIT = 1;
	private const int INFO_TYPE_TERRAIN = 2;
	
	// Unit info indices
	private const int I_HEALTH = 0;
	private const int I_POWER = 1;
	private const int I_MOVEMENT = 2;
	private const int I_TEAM = 3;
	
	private string[] UNIT_LABELS = new string[]{
		"Health: ",
		"Attack Power: ",
		"Movement: ",
		"Team: "
	};
	
	private string[] TERRAIN_LABELS = new string[]{	
		"Movement Cost: ",
		"Taken: "
	};
	
	private const KeyCode KEY_HELP = KeyCode.H;
	
	private bool displayHelp = false;
	private int infoType;
	
	private float[] unitInfo;
	private float[] terrainInfo;
	private string selectedName = "";
	
	private GUIStyle titleStyle = null; 
	private GUIStyle normalStyle = null;
	private GUIStyle superStyle = null;
	
	public const int TEAM_NONE = 0;
	public const int TEAM_PLAYER = 1;
	public const int TEAM_AI = 2;
	public const int TEAM_TIE = 3;
	
	private int winStatus = TEAM_NONE;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KEY_HELP)){
			displayHelp = !displayHelp;
		}
	}
	
	void OnGUI(){		
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
		
		if(winStatus > TEAM_NONE){
			superStyle.normal.textColor = Color.white;
			GUI.Box(new Rect(Screen.width*WIN_BOX_X,Screen.height*WIN_BOX_Y,Screen.width*WIN_BOX_WIDTH,Screen.height*WIN_BOX_HEIGHT),"");
			GUI.Label(new Rect(Screen.width*WIN_LABEL_X,Screen.height*WIN_LABEL_Y,Screen.width*WIN_LABEL_WIDTH,Screen.height*WIN_LABEL_HEIGHT),"Game Over",superStyle);
			
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
			
			GUI.Label(new Rect(Screen.width*WIN_MESSAGE_X,Screen.height*WIN_MESSAGE_Y,Screen.width*WIN_MESSAGE_WIDTH,Screen.height*WIN_MESSAGE_HEIGHT),winLabel,superStyle);
			
			if(GUI.Button(new Rect(Screen.width*WIN_BUTTON_X,Screen.height*WIN_BUTTON_Y,Screen.width*WIN_BUTTON_WIDTH,Screen.height*WIN_BUTTON_HEIGHT),"Play Again!")){
				Application.LoadLevel(0);
			}
			
			return;
		}
		
		if(displayHelp){
			GUI.Box(new Rect(Screen.width*HELP_BOX2_X,Screen.height*HELP_BOX2_Y,Screen.width*HELP_BOX2_WIDTH,Screen.height*HELP_BOX2_HEIGHT),"");
			float labelHeight = HELP_BOX2_HEIGHT/HELP_LABELS.Length;
			for(int i = 0; i < HELP_LABELS.Length; i++){
				GUI.Label(new Rect(Screen.width*(HELP_BOX2_X + .01f),Screen.height*HELP_BOX2_Y + i*Screen.height*labelHeight,Screen.width*HELP_BOX2_WIDTH,Screen.height*labelHeight),HELP_LABELS[i],normalStyle);
			}
		}else{
			GUI.Box(new Rect(Screen.width*HELP_BOX1_X,Screen.height*HELP_BOX1_Y,Screen.width*HELP_BOX1_WIDTH,Screen.height*HELP_BOX1_HEIGHT),"h: help");
		}
		
		GUI.Box(new Rect(Screen.width*BOX_X,Screen.height*BOX_Y,Screen.width*BOX_WIDTH,Screen.height*BOX_HEIGHT), "");
		
		if(GUI.Button(new Rect(Screen.width*BOX_X,Screen.height*BOX_Y + Screen.height*(BOX_HEIGHT - BUTTON_HEIGHT),Screen.width*BOX_WIDTH,Screen.height*BUTTON_HEIGHT),"End Turn")){
			this.gameObject.SendMessage("changeFromPlayerOne");
		}
		
		switch(infoType){
		case INFO_TYPE_NONE:
			break;
		case INFO_TYPE_UNIT:
			if(unitInfo != null && unitInfo.Length > 0){
				float labelHeight = (BOX_HEIGHT - BUTTON_HEIGHT)/(unitInfo.Length + 1);
				GUI.Label(new Rect(Screen.width*BOX_X,Screen.height*BOX_Y,Screen.width*BOX_WIDTH,Screen.height*labelHeight),selectedName,titleStyle);
				for(int i = 0; i < unitInfo.Length; i++){
					GUI.Label(new Rect(Screen.width*BOX_X,Screen.height*BOX_Y + Screen.height*(i+1)*labelHeight,Screen.width*BOX_WIDTH,Screen.height*labelHeight),UNIT_LABELS[i] + unitInfo[i],normalStyle);
				}
			}
			break;
		case INFO_TYPE_TERRAIN:
			if(terrainInfo != null && terrainInfo.Length > 0){
				float labelHeight = (BOX_HEIGHT - BUTTON_HEIGHT)/(terrainInfo.Length + 4);
				GUI.Label(new Rect(Screen.width*BOX_X,Screen.height*BOX_Y,Screen.width*BOX_WIDTH,Screen.height*labelHeight),selectedName,titleStyle);
				for(int i = 0; i < terrainInfo.Length; i++){
					GUI.Label(new Rect(Screen.width*BOX_X,Screen.height*BOX_Y + Screen.height*(i+1)*labelHeight,Screen.width*BOX_WIDTH,Screen.height*labelHeight),TERRAIN_LABELS[i] + terrainInfo[i],normalStyle);
				}
			}
			break;
		}
	}
	
	private void updateUnitInfo(float[] unitInfo){
		this.unitInfo = unitInfo;
		infoType = INFO_TYPE_UNIT;
	}
	
	private void updateName(string name){
		if(name != "radioChild" && name != "Default"){
			selectedName = char.ToUpper(name.ToCharArray()[0]) + name.Substring(1);	
		}else if(name == "radioChild"){
			selectedName = "Radio Child";	
		}else{
			selectedName = "Dirt";	
		}
		
	}
	
	private void updateTerrainInfo(float[] terrainInfo){
		this.terrainInfo = terrainInfo;
		infoType = INFO_TYPE_TERRAIN;
	}
	
	private void cancelInfo(){
		infoType = INFO_TYPE_NONE;
	}
	
	private void winner(int team){
		winStatus = team;
	}
}
