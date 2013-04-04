using UnityEngine;
using System.Collections;

public class UnitScript : MonoBehaviour
{
	MakeGround ground;
	TerrainScript terrain;
	
	public string unitType;
	public int hp;
	public int moveSpeed;
	public int team;
	public Vector2 location;
	
	public GameObject Brute;
	public GameObject RadioChild;
	public GameObject Rogue;
	public GameObject Brain;
	public GameObject Bomber;
	public GameObject gameMaster;
	
	Color storeColor;
	
	// Use this for initialization
	void Start ()
	{
		gameMaster = GameObject.FindGameObjectWithTag("GameMaster");
		ground = gameMaster.GetComponent<MakeGround>();
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		//ground.grid[(int)location.y, (int)location.x].renderer.material.color = 
		if(team == 1){
			ground.grid[(int)location.y, (int)location.x].renderer.material.color = Color.magenta;
		}
		if(team == 2){
			ground.grid[(int)location.y, (int)location.x].renderer.material.color = Color.yellow;
		}
		
	}
	
	void OnMouseDown(){
		storeColor = gameObject.renderer.material.color;
		gameObject.renderer.material.color = Color.white;
		terrain = ground.grid[(int)location.y, (int)location.x].GetComponent<TerrainScript>();
		terrain.SendMessage("UnitSelected");
	}
	void OnMouseUp(){
		gameObject.renderer.material.color = storeColor;
	}
	
	void moveTo (Vector2 spot){
		Vector3 destination = ground.grid[(int)spot.x, (int)spot.y].transform.localPosition;
		destination.y += 5;
		location = destination;
	}
}

