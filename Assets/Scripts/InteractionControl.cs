using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InteractionControl : MonoBehaviour {
	
	public Vector2 destination;
	public GameObject mover;
	GameObject target;
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
		if(target != null && mover != null)
			CombatRoutine(mover, target);
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
	
	void NewTarget (GameObject victim){
		target = victim;
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
			// rxl244: if statement prevents player from moving enemy units
			if(unit.team != 2){
				unit.SendMessage("moveTo", destination);
			}
			//reset values
			destination = Vector2.zero;
			mover = null;
			available = null;
		}
		viableFlag = false;
//		Debug.Log ("unit moved");
	}
	
	void CombatRoutine (GameObject attacker, GameObject defender){
		UnitScript atk = attacker.GetComponent<UnitScript>();
		UnitScript def = defender.GetComponent<UnitScript>();
		if(atk.location.x == def.location.x){
			if(atk.location.y == def.location.y + 1)
				viableFlag = true;
			if(atk.location.y == def.location.y - 1)
				viableFlag = true;
		}else if(atk.location.y == def.location.y){
			if(atk.location.x == def.location.x + 1)
				viableFlag = true;
			if(atk.location.x == def.location.x - 1)
				viableFlag = true;
		}else viableFlag = false;
		if(viableFlag){
			if(atk.unitType == "brain" && def.unitType == "brute"){
				def.team = atk.team;
			}else if(atk.unitType == "bomber"){
				def.hp -= atk.atkPower;
				atk.hp -= atk.atkPower;
			}else{
				def.hp -= atk.atkPower;
			}
		}
		viableFlag = false;
		destination = Vector2.zero;
		mover = null;
		available = null;
		target = null;
	}
}
