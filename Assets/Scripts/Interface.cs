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
	
	private const float HELP_BOX2_X = BOX_X;
	private const float HELP_BOX2_Y = 0;
	private const float HELP_BOX2_WIDTH = BOX_WIDTH;
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
		"Use your radioactive child to capture 3",
		"\t radioactive spots.",
		"OR:Kill all enemy units.",
		"Tie: both radio-child units die."
	};
	
	private const int INFO_TYPE_NONE = 0;
	private const int INFO_TYPE_UNIT = 1;
	private const int INFO_TYPE_TERRAIN = 2;
	
	private const KeyCode KEY_HELP = KeyCode.H;
	
	private bool displayHelp = false;
	private int infoType;
	
	//private float[] unitInfo;
	
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
		if(displayHelp){
			GUI.Box(new Rect(Screen.width*HELP_BOX2_X,Screen.height*HELP_BOX2_Y,Screen.width*HELP_BOX2_WIDTH,Screen.height*HELP_BOX2_HEIGHT),"");
			float labelHeight = HELP_BOX2_HEIGHT/HELP_LABELS.Length;
			for(int i = 0; i < HELP_LABELS.Length; i++){
				GUI.Label(new Rect(Screen.width*HELP_BOX2_X,Screen.height*HELP_BOX2_Y + i*Screen.height*labelHeight,Screen.width*HELP_BOX2_WIDTH,Screen.height*labelHeight),HELP_LABELS[i]);
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
			break;
		case INFO_TYPE_TERRAIN:
			break;
		}
		/*
		unitInfo = new float[]{1,10,2,3,1};
		
		string unitName = "";
		string unitHealth = "";
		string unitPower = "";
		string unitDefense = "";
		string unitMovement = "";
		
		if(unitInfo != null){
			int type = (int)unitInfo[0];
			unitHealth = "" + unitInfo[1];
			unitPower = "" + unitInfo[2];
			unitDefense = "" + unitInfo[3];
			unitMovement = "" + unitInfo[4];
			switch(type){
			case Unit.TYPE_RADIO_CHILD:
				unitName = "Radio Child";
				break;
			case Unit.TYPE_BRAIN:
				unitName = "Brain";
				break;
			case Unit.TYPE_BRUTE:
				unitName = "Brute";
				break;
			case Unit.TYPE_ROGUE:
				unitName = "Rogue";
				break;
			case Unit.TYPE_BOMBER:
				unitName = "Bomber";
				break;
			}
		}
		
		GUI.Label(new Rect(Screen.width*BOX_X,Screen.height*BOX_Y,Screen.width*BOX_WIDTH,Screen.height*LABEL_HEIGHT),unitName);
		GUI.Label(new Rect(Screen.width*BOX_X,Screen.height*BOX_Y + Screen.height*LABEL_HEIGHT,Screen.width*BOX_WIDTH,Screen.height*LABEL_HEIGHT),unitHealth);
		GUI.Label(new Rect(Screen.width*BOX_X,Screen.height*BOX_Y + 2*Screen.height*LABEL_HEIGHT,Screen.width*BOX_WIDTH,Screen.height*LABEL_HEIGHT),unitPower);
		GUI.Label(new Rect(Screen.width*BOX_X,Screen.height*BOX_Y + 3*Screen.height*LABEL_HEIGHT,Screen.width*BOX_WIDTH,Screen.height*LABEL_HEIGHT),unitDefense);
		GUI.Label(new Rect(Screen.width*BOX_X,Screen.height*BOX_Y + 4*Screen.height*LABEL_HEIGHT,Screen.width*BOX_WIDTH,Screen.height*LABEL_HEIGHT),unitMovement);
		*/
		
		
	}
	
	private void updateUnitInfo(float[] unitInfo){
		//this.unitInfo = unitInfo;
	}
	
	private void updateTerrainInfo(){
		
	}
	
	private void cancelInfo(){
		
	}
}
