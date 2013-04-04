using UnityEngine;
using System.Collections;

public class Interface : MonoBehaviour {
	private const float BOX_X = .50f;
	private const float BOX_Y = .80f;
	private const float BOX_WIDTH = .50f;
	private const float BOX_HEIGHT = .20f;
	
	private const float LABEL_HEIGHT = .04f;
	
	//private float[] unitInfo;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI(){
		/*
		GUI.Box(new Rect(Screen.width*BOX_X,Screen.height*BOX_Y,Screen.width*BOX_WIDTH,Screen.height*BOX_HEIGHT), "");
		
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
}
