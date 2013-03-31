using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {
	
	int currentPlayer = 1;
	PlayerMovement lastPlayMove = null;
	
	bool isEnemyAI = false;
	
	
	//scripts to talk with
	MakeGround ground;
	GridFunction gridFunc;
	
	//GUI things
	public GUIStyle bottomArea;
	bool isBattleGUIActive;
	
	// Use this for initialization
	void Start () 
	{
		/*
		ground = gameObject.GetComponent<MakeGround>();
		
		//used to set up attributes of board and then create it
		ground.SendMessage("setTerrainAttributes");
		ground.SendMessage("startMakeGrid");
		
		isBattleGUIActive = false;
		*/
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(lastPlayMove != null)
		{
			if (Input.GetMouseButtonDown(1))
			{
				//isBattleGUIActive = false;
				
				lastPlayMove.player = 3;
				this.SendMessage("colorGroundFromUnit", lastPlayMove);				
			}
		}
	}
	
	void actualStart()
	{
		ground = gameObject.GetComponent<MakeGround>();
		
		//used to set up attributes of board and then create it
		ground.SendMessage("setTerrainAttributes");
		ground.SendMessage("startMakeGrid");
		
		isBattleGUIActive = false;
	}
	
	void sendBoardCorners()
	{
		this.actualStart();
		this.SendMessage("returnBoardCorners");	
	}
	
	void changeGroundColorMaster(PlayerMovement play)
	{		
		if(lastPlayMove != null)
		{
			lastPlayMove.player = 3;
			this.SendMessage("colorGroundFromUnit", lastPlayMove);
		}
		
		this.SendMessage("colorGroundFromUnit", play);
		lastPlayMove = play;
	}
	
	//Message Center
	void getStartLocations()
	{
		gridFunc.SendMessage("sendAIStartLocations");
	}
	
	void changeBattleGUIActive(bool newValue)
	{
			isBattleGUIActive = newValue;
	}
	
	void OnGUI()
	{
		if(isBattleGUIActive)
		{
			GUI.BeginGroup (new Rect (0,Screen.height*4/5,Screen.width,Screen.height/5));
			
			GUI.Box (new Rect (0,0,Screen.width,Screen.height/5), "", bottomArea);
			
			GUI.EndGroup();
		}
	}
	
	
}
