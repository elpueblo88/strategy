using UnityEngine;
using System.Collections;

public class UnitScript : MonoBehaviour
{
	MakeGround ground;
	TerrainScript terrain;
	InteractionControl interaction;
	
	public string unitType;
	public int hp;
	public int atkPower;
	public int moveSpeed;
	public int team;
	public Vector2 location;
	
	public GameObject gameMaster;
	
	Color storeColor;
	
	// Use this for initialization
	void Start ()
	{
		gameMaster = GameObject.FindGameObjectWithTag("GameMaster");
		ground = gameMaster.GetComponent<MakeGround>();
		interaction = gameMaster.GetComponent<InteractionControl>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(hp <= 0){
			Object.Destroy(gameObject);
		}else{
			if(team == 1){
				ground.grid[(int)location.y, (int)location.x].renderer.material.color = Color.magenta;
			}
			if(team == 2){
				ground.grid[(int)location.y, (int)location.x].renderer.material.color = Color.yellow;
			}
		}
	}
	
	void OnMouseOver(){
		if(Input.GetMouseButtonDown(1)){
			interaction.SendMessage("NewTarget", gameObject);
		}
	}
	
	void OnMouseDown(){
		storeColor = gameObject.renderer.material.color;
		gameObject.renderer.material.color = Color.white;
		terrain = ground.grid[(int)location.y, (int)location.x].GetComponent<TerrainScript>();
		terrain.SendMessage("UnitSelected", gameObject);
		interaction.SendMessage("NewUnitSelected", gameObject);
		
		// rxl244: update interface when unit is clicked on
		gameMaster.SendMessage("updateUnitInfo",new float[]{this.hp,this.atkPower,this.moveSpeed,this.team});
		gameMaster.SendMessage("updateName",this.unitType);
	}
	void OnMouseUp(){
		gameObject.renderer.material.color = storeColor;
	}
	
	public void moveTo (Vector2 spot){
		terrain = ground.grid[(int)spot.y, (int)spot.x].GetComponent<TerrainScript>();
		//terrain.SendMessage("switchOccupied");
		terrain.switchOccupied();
		terrain = ground.grid[(int)spot.y, (int)spot.x].GetComponent<TerrainScript>();
		//terrain.SendMessage("switchOccupied");
		terrain.switchOccupied();
		Vector3 destination = ground.grid[(int)spot.y, (int)spot.x].transform.localPosition;
		destination.y += 5;
		location = spot;
		gameObject.transform.localPosition = destination;
	}
}

