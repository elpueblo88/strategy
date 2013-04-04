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
	public void switchOccupied()
	{
		occupied = !occupied;	
	}
	
	// rxl244: called by moving unit to claim a radioactive spot. Also checks win condition.
	private void setTaken(int team){
		if(this.taken > 0){
			return;
		}
		
		this.taken = team;
		
		MakeGround makeGroundScript = GameObject.Find("GameMaster").GetComponent<MakeGround>();
		int takenCount = 0;

		foreach(Vector2 goalLocation in makeGroundScript.goalLocations){
			if(makeGroundScript.grid[(int)goalLocation.y,(int)goalLocation.x].GetComponent<TerrainScript>().taken == team){
				takenCount++;
			}
		}
		
		if(takenCount >= 3){
			gameMaster.SendMessage("winner",team);
		}
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
	
	// rxl244: update interface to view terrain info
	void OnMouseDown(){
		gameMaster.SendMessage("updateTerrainInfo", new float[]{movementCost,taken});
		gameMaster.SendMessage("updateName",this.terrainType);
	}
}
