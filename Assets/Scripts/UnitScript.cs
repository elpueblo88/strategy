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
	
	Color storeColor;
	
	// Use this for initialization
	void Start ()
	{
		ground = gameObject.GetComponent<MakeGround>();
		if(team == 1){
			gameObject.renderer.material.color = Color.magenta;
		}
		if(team == 2){
			gameObject.renderer.material.color = Color.yellow;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		
	}
	
	void OnMouseDown(){
		storeColor = gameObject.renderer.material.color;
		gameObject.renderer.material.color = Color.white;
		terrain = ground.grid[(int)location.x, (int)location.y].GetComponent<TerrainScript>();
		terrain.SendMessage("OnMouseDown");
	}
	void OnMouseUp(){
		gameObject.renderer.material.color = storeColor;
	}
	
	void moveTo (int x, int y){
		Vector3 destination = ground.grid[x,y].transform.localPosition;
		destination.y += 5;
		location = destination;
	}
}

