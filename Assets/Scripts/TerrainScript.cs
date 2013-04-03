using UnityEngine;
using System.Collections;

public class TerrainScript : MonoBehaviour {
	
	public string terrainType;
	public int movementCost;
	public bool isAccessible;
	public int defenseBonus;
	public float height;
	public bool occupied;
	
	//terrain grid is of form grid[z,x]
	public int zValue;
	public int xValue;
	
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
	
	void switchOccupied()
	{
		occupied = !occupied;	
	}
}
