using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {
	public const string TAG = "GameMaster";
	
	int currentPlayer = 1;
	PlayerMovement lastPlayMove = null;

	bool isBattleGUIActive;
	
	MakeGround ground;
	GridFunction gridFunc;
	
	public GUIStyle bottomArea;
	
	// Use this for initialization
	void Start () 
	{
		ground = gameObject.GetComponent<MakeGround>();
		
		//used to set up attributes of board and then create it
		ground.SendMessage("setTerrainAttributes");
		ground.SendMessage("startMakeGrid");
		
		isBattleGUIActive = false;
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
	
	void changeGroundColorMaster(PlayerMovement play)
	{		
		if(lastPlayMove != null)
		{
			print ("here");
			lastPlayMove.player = 3;
			this.SendMessage("colorGroundFromUnit", lastPlayMove);
		}
		
		this.SendMessage("colorGroundFromUnit", play);
		lastPlayMove = play;
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
