using UnityEngine;
using System.Collections;

public class UnitScript : MonoBehaviour
{
	MakeGround ground;
	
	public string unitType;
	public int hp;
	public int moveSpeed;
	public int team;
	public Vector2 location;
	
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
	}
	void OnMouseUp(){
		gameObject.renderer.material.color = storeColor;
	}
}

