using UnityEngine;
using System.Collections;

//stores information about each tile on the grid
/// Author Daniel Pfeffer dnp19
public class TerrainScript : MonoBehaviour {
	
	//basic grid info
	public string terrainType;
	public int movementCost;
	public bool isAccessible;
	public int defenseBonus;
	public float height;
	public bool occupied;
	public int taken;
	
	//terrain grid is of form grid[z,x]
	public int zValue;
	public int xValue;
	
	//can send GameMaster inforamtion
	public GameObject gameMaster;

	// Use this for initialization
	void Start () 
	{
		gameMaster = GameObject.Find("GameMaster");
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	//changes color of ground
	void OnMouseDown()
	{
		PlayerMovement playMove = new PlayerMovement(xValue, zValue, 4, 1);
		gameMaster.SendMessage("changeGroundColorMaster", playMove);
	}
	
	//switches occupied state of tile
	void switchOccupied()
	{
		occupied = !occupied;	
	}
}
