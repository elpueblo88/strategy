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
	
	//gdm37::necessary for movement communication
	InteractionControl interaction;
	UnitScript uScript;

	// Use this for initialization
	void Start () 
	{
		gameMaster = GameObject.Find("GameMaster");
		
		//gdm37
		interaction = gameMaster.GetComponent<InteractionControl>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	//changes color of ground
	void UnitSelected (GameObject unit)
	{
		uScript = unit.GetComponent<UnitScript>();
		PlayerMovement playMove = new PlayerMovement(xValue, zValue, uScript.moveSpeed, uScript.team);
		gameMaster.SendMessage("changeGroundColorMaster", playMove);
	}
	
	//switches occupied state of tile
	void switchOccupied()
	{
		occupied = !occupied;	
	}
	
	//gdm37::Helps handle movement by communicating with the move controller
	void OnMouseOver(){
		if(Input.GetMouseButtonDown(1)){
			Vector2 location;
			location.x = xValue;
			location.y = zValue;
			interaction.SendMessage("NewDestinationChosen", location);
		}
	}
}
