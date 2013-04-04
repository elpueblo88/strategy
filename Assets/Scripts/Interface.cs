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
		"Movement Cost: "
	};
	
	private const KeyCode KEY_HELP = KeyCode.H;
	
	private bool displayHelp = false;
	private int infoType;
	
	private float[] unitInfo;
	private float[] terrainInfo;
	private string selectedName = "";
	
	private GUIStyle titleStyle = null; 
	private GUIStyle normalStyle = null;
	
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
			titleStyle.fontSize = 16;
			normalStyle.alignment = TextAnchor.UpperLeft;
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
				float labelHeight = (BOX_HEIGHT - BUTTON_HEIGHT)/(terrainInfo.Length + 1);
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
}
