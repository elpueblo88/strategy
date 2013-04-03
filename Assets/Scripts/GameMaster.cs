using UnityEngine;
using System.Collections;

/// <summary>
/// Game master.
/// Does game starting and some message brokering
/// </summary>
public class GameMaster : MonoBehaviour {
	
	//keepts track of current player and last movement
	public int currentPlayer = 1;
	PlayerMovement lastPlayMove = null;
	
	//keeps track of if the enemey
	public bool isEnemyAI = true;
	
	
	//scripts to talk with
	MakeGround ground;
	GridFunction gridFunc;
	
	//GUI things
	public GUIStyle bottomArea;
	bool isBattleGUIActive;
	
	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		if(lastPlayMove != null)
		{
			//checks if the right mouse has been clicked to remove movable color
			if (Input.GetMouseButtonDown(1))
			{
				//isBattleGUIActive = false;
				
				lastPlayMove.player = 3;
				this.SendMessage("colorGroundFromUnit", lastPlayMove);				
			}
		}
	}
	
	//method to actually set evertyhing up
	void actualStart()
	{
		//gets the ground
		ground = gameObject.GetComponent<MakeGround>();
		gridFunc = gameObject.GetComponent<GridFunction>();
		
		//used to set up attributes of board and then create it
		ground.SendMessage("setTerrainAttributes");
		ground.SendMessage("startMakeGrid");
		
		isBattleGUIActive = false;
	}
	
	//used to send corners of the grid to the camera
	void sendBoardCorners()
	{
		this.actualStart();
		this.SendMessage("returnBoardCorners");	
	}
	
	//will call gridFunction to change ground color to show movemnt allowed
	public LinkedList<Vector2> changeGroundColorMaster(PlayerMovement play)
	{		
		//removes last showed movement
		if(lastPlayMove != null)
		{
			lastPlayMove.player = 3;
			this.SendMessage("colorGroundFromUnit", lastPlayMove);
		}
		
		return gridFunc.colorGroundFromUnit(play);
		//this.SendMessage("colorGroundFromUnit", play);
		lastPlayMove = play;
	}
	
	//Message Center

	//changes from player 1 to 2
	void changeFromPlayerOne()
	{
		if(currentPlayer == 1)
		{
			currentPlayer = 2;	
		}
	}
	
	//changes from player 2 to 1
	void changeFromPlayerTwo()
	{
		if(currentPlayer == 2)
		{
			currentPlayer = 1;	
		}
	}
	
	//used to switch if battle gui is up
	void changeBattleGUIActive(bool newValue)
	{
			isBattleGUIActive = newValue;
	}
	
	//used to display gui
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