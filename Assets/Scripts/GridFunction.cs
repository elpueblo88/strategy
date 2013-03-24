using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridFunction : MonoBehaviour {
	
	MakeGround ground;

	// Use this for initialization
	void Start () 
	{
		ground = gameObject.GetComponent<MakeGround>();
	}
	
	// Update is called once per frame
	void Update () 
	{
			
	}
	
	void changeGroundColor(int x, int z, Color color)
	{
		ground.grid[z,x].renderer.material.color = color;
	}
	
	void colorGroundFromUnit(PlayerMovement playMove)
	{
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
		
		LinkedList<Vector2> list = getMoveableSpots(playMove.x, playMove.z, playMove.moves);
		
		/*
		 * currently not used method of going through linkedlist
		LinkedListNode<Vector2> node = list.First;
		while(node.Next != null)
		{
			node = node.Next;
			System.Console.WriteLine(node.Value.x + " " + node.Value.y);
			changeGroundColor((int)node.Value.x, (int)node.Value.y, color);
		}
		*/
		
		
		foreach(Vector2 node in list)
		{
			System.Console.WriteLine(node.x + " " + node.y);
			changeGroundColor((int)node.x, (int)node.y, color);
		}
		
		
	}
	
	LinkedList<Vector2> getMoveableSpots(int x, int z, int moves)
	{
		LinkedList<Vector2> list = new LinkedList<Vector2>();
		testIndividualSpots(ref list, x, z, moves);
		return list;
	}
	
	void testIndividualSpots(ref LinkedList<Vector2> list, int x, int z, int moves)
	{
		TerrainScript t;
		
		int location = 0;
		
		if(moves <= 0)
		{
			return;	
		}
		
		t = ground.grid[z,x].GetComponent("TerrainScript") as TerrainScript;
		if(!t.isAccessible)
		{
			return;	
		}
			
		if(list.Contains(new Vector2(x,z)) != true)
		{
			list.AddLast(new Vector2(x,z));
		}
			
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
}
