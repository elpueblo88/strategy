using UnityEngine;
using System.Collections;

public class InteractionControl : MonoBehaviour {
	
	public Vector2 destination;
	public GameObject mover;
	
	public GameObject gMaster;
	MakeGround ground;
	UnitScript unit;
		
	// Use this for initialization
	void Start () {
		gMaster = GameObject.FindGameObjectWithTag("GameMaster");
		ground = gMaster.GetComponent<MakeGround>();
	}
	
	// Update is called once per frame
	void Update () {
		if(destination != Vector2.zero && mover != null){
			MoveControl ();
		}
	}
	
	void NewDestinationChosen (Vector2 position){
		Debug.Log ("Destination Recieved");
		destination = position;
	}
	
	void NewUnitSelected (GameObject selected){
		Debug.Log ("Unit Selected");
		mover = selected;
	}
	
	void MoveControl (){
		unit = mover.GetComponent<UnitScript>();
		unit.SendMessage("moveTo", destination);
		
		//reset values
		destination = Vector2.zero;
		mover = null;
		Debug.Log ("unit moved");
	}
}
