using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Functions that the grid can do
/// </summary>
public class GridFunction : MonoBehaviour {
	
	MakeGround ground;
	public GameObject cameraControl;
	GameMaster gMaster;
	
	//gets ai
	public GameObject ai;
	//AIscript aiscript;

	// Use this for initialization
	void Start () 
	{
		//gets ground script from MakeGround and it's values
		ground = gameObject.GetComponent<MakeGround>();
		gMaster = gameObject.GetComponent<GameMaster>();
		if(cameraControl == null)
		{
			cameraControl = GameObject.Find("Main Camera");	
		}
		
		//AI stuff
		/*
		if(AI == null)
		{
			ai = GameObject.Find("GameMaster");	
		}
		aiscript = ai.GetComponent<aiscript>();
		*/

	}
	
	// Update is called once per frame
	void Update () 
	{
			
	}
	
	//changes color of ground for a given position and color
	void changeGroundColor(int x, int z, Color color)
	{
		ground.grid[z,x].renderer.material.color = color;
		
		foreach(Transform child in ground.grid[z,x].transform)
		{
			child.transform.gameObject.renderer.material.color = color;	
		}
	}
	
	//given a playerMovement object, it finds where the player can go and changes color
	public LinkedList<Vector2> colorGroundFromUnit(PlayerMovement playMove)
	{
		//picks color for different players
		Color color = Color.white;
		if(playMove.player == 1)
		{
			color = Color.blue;	
		}
		else if(playMove.player == 2)
		{
			color = Color.red;	
		}
		else
		{
			color = Color.white;	
		}
		
		//makes a list of places a unit can move
		LinkedList<Vector2> list = getMoveableSpots(playMove.x, playMove.z, playMove.moves);		
		
		//if it's the AI send it the list
		if(gMaster.currentPlayer == 2 && gMaster.isEnemyAI)
		{
			return list;
		}
		else
		{
			//if a person change the color of the ground it can move to
			foreach(Vector2 node in list)
			{
				System.Console.WriteLine(node.x + " " + node.y);
				changeGroundColor((int)node.x, (int)node.y, color);
			}
			return list;
		}
	}
	
	//wrapper method to start the breadth first seach
	LinkedList<Vector2> getMoveableSpots(int x, int z, int moves)
	{
		LinkedList<Vector2> list = new LinkedList<Vector2>();
		testIndividualSpots(ref list, x, z, moves);
		return list;
	}
	
	//breadth first search for obtainable spots
	void testIndividualSpots(ref LinkedList<Vector2> list, int x, int z, int moves)
	{		
		int location = 0;
		
		//makes sure the spot is non occupied and accessible
		TerrainScript t = ground.grid[z,x].GetComponent("TerrainScript") as TerrainScript;
		if(!t.isAccessible && !t.occupied)
		{
			return;	
		}
		
		//add spot if its good
		if(list.Contains(new Vector2(x,z)) != true)
		{
			list.AddLast(new Vector2(x,z));
		}
		
		if(moves <= 0)
		{
			return;	
		}
			
		//adds more things to list to check if they are good spots
		if(x > 0)
		{
			t = ground.grid[z,x-1].GetComponent("TerrainScript") as TerrainScript;
			if(t.isAccessible)
			{
				testIndividualSpots(ref list, x - 1, z, moves - t.movementCost);
			}
		}
		
		if(x < ground.xLength - 1)
		{
			t = ground.grid[z,x+1].GetComponent("TerrainScript") as TerrainScript;
			if(t.isAccessible)
			{
				testIndividualSpots(ref list, x + 1, z, moves - t.movementCost);
			}
		}
		
		if(z < ground.zLength - 1)
		{
			t = ground.grid[z+1,x].GetComponent("TerrainScript") as TerrainScript;
			if(t.isAccessible)
			{
				testIndividualSpots(ref list, x, z + 1, moves - t.movementCost);
			}
		}
		
		if(z > 0)
		{
			t = ground.grid[z-1,x].GetComponent("TerrainScript") as TerrainScript;
			if(t.isAccessible)
			{
				testIndividualSpots(ref list, x, z - 1, moves - t.movementCost);
			}
		}
	}
	
	//returns location of bottom left and top right corners
	void returnBoardCorners()
	{
		this.Start();
		Vector3[] groundCorners = new Vector3[3];
		groundCorners[0] = ground.BLcorner;
		groundCorners[1] = new Vector3(ground.BLcorner.x + ground.xLength * ground.xSize, ground.BLcorner.y, ground.BLcorner.z + ground.zLength * ground.zSize);
		groundCorners[2] = new Vector3(ground.xLength, ground.grid[0,0].transform.localScale.y, ground.zLength);
		cameraControl.SendMessage("getCorners", groundCorners);
	}
	
}
