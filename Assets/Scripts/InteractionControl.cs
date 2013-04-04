using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InteractionControl : MonoBehaviour {
	
	public Vector2 destination;
	public GameObject mover;
	public LinkedList<Vector2> available;
	
	public bool viableFlag;
	
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
		if(destination != Vector2.zero && mover != null && available != null){
			MoveControl ();
		}
	}
	
	void NewDestinationChosen (Vector2 position){
//		Debug.Log ("Destination Recieved");
		destination = position;
	}
	
	void NewUnitSelected (GameObject selected){
//		Debug.Log ("Unit Selected");
		mover = selected;
	}
	
	void SetViableMoves (LinkedList<Vector2> destinations) {
		available = destinations;
	}
	
	void MoveControl (){
		unit = mover.GetComponent<UnitScript>();
		
		foreach(Vector2 node in available){
			if(node == destination)
				viableFlag = true;
		}
		if(viableFlag){
			unit.SendMessage("moveTo", destination);
			//reset values
			destination = Vector2.zero;
			mover = null;
			available = null;
		}
		viableFlag = false;
//		Debug.Log ("unit moved");
	}
}
