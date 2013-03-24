using UnityEngine;
using System.Collections;

public class TerrainScript : MonoBehaviour {
	
	public string terrainType;
	public int movementCost;
	public bool isAccessible;
	public int defenseBonus;
	
	//terrain grid is of form grid[z,x]
	public int zValue;
	public int xValue;
	
	public GameObject groundMaker;

	// Use this for initialization
	void Start () 
	{
		groundMaker = GameObject.Find("GameMaster");
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	void OnMouseDown()
	{
		PlayerMovement playMove = new PlayerMovement(xValue, zValue, 4, 1);
		groundMaker.SendMessage("colorGroundFromUnit", playMove);
	}
}
