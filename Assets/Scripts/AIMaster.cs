using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIMaster : MonoBehaviour {
	private const int PRIORITY_ROGUE = 1;
	
	private GameObject[] team1;
	private GameObject[] team2;
	private bool waitForUpdate;
	private GameObject gameMaster;
	
	private Vector2 locationToCheckAgainst;
	// Use this for initialization
	void Start () {
		gameMaster = GameObject.Find("GameMaster");
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	private void runAI(){
		if(gameMaster != null){
			gameMaster.SendMessage("getTeam2");
			
			this.team2 = gameMaster.GetComponent<UnitMaster>().team2;	
		
			foreach(GameObject unit in team2){
				UnitScript unitScript = unit.GetComponent<UnitScript>();
				GameMaster gameMasterScript = gameMaster.GetComponent<GameMaster>();
				MakeGround makeGroundScript = gameMaster.GetComponent<MakeGround>();
				
				LinkedList<Vector2> positionList = gameMasterScript.changeGroundColorMaster(new PlayerMovement((int)unitScript.location.x,(int)unitScript.location.y, unitScript.moveSpeed, 2));
				
				Vector2 nextPosition = new Vector2();
				
				locationToCheckAgainst = unitScript.location;
				if(unitScript.unitType == "radioChild"){
					if(positionList.Count != 0){
						foreach(Vector2 goalLocation in makeGroundScript.goalLocations){
							if(makeGroundScript.grid[(int)goalLocation.y,(int)goalLocation.x].GetComponent<TerrainScript>().taken == 0){
								if(nextPosition == null || (Vector2.Distance(nextPosition,unitScript.location) > Vector2.Distance(unitScript.location,goalLocation))){
									nextPosition = goalLocation;
								}
							}
						}
					}else{
						
					}
					
					
				}else if(unitScript.unitType == "rogue"){
					
					if((	nextPosition = findTarget("bomber")) != null 
						|| (nextPosition = findTarget("brain")) != null 
						|| (nextPosition = findTarget("radioChild")) != null  
						|| (nextPosition = findTarget("rogue")) != null
						|| (nextPosition = findTarget("brute")) != null){
					}
				}else if(unitScript.unitType == "bomber"){
					if((	nextPosition = findTarget("radioChild")) != null 
						|| (nextPosition = findTarget("brute")) != null 
						|| (nextPosition = findTarget("rogue")) != null  
						|| (nextPosition = findTarget("brain")) != null
						|| (nextPosition = findTarget("bomber")) != null){
					}
				}else if(unitScript.unitType == "brain"){
					if((	nextPosition = findTarget("brute")) != null 
						|| (nextPosition = findTarget("radioChild")) != null 
						|| (nextPosition = findTarget("brain")) != null  
						|| (nextPosition = findTarget("bomber")) != null
						|| (nextPosition = findTarget("rogue")) != null){
					}
				}else if(unitScript.unitType == "brute"){
					if((	nextPosition = findTarget("radioChilld")) != null 
						|| (nextPosition = findTarget("rogue")) != null 
						|| (nextPosition = findTarget("brute")) != null  
						|| (nextPosition = findTarget("bomber")) != null
						|| (nextPosition = findTarget("brain")) != null){
					}
				}
				
				if(nextPosition != null){
					unit.SendMessage("moveTo",nextPosition);
				}
				
			
				// attack if possible
			}
		}else{
			gameMaster = GameObject.Find("GameMaster");
			runAI ();
		}
	}
	
	private Vector2 findTarget(string targetType){
		Vector2 nextTarget = new Vector2();
		
		this.team1 = gameMaster.GetComponent<UnitMaster>().team1;	
		foreach(GameObject unit in team1){
			UnitScript unitScript = unit.GetComponent<UnitScript>();
			if(unitScript.unitType == targetType && (nextTarget == null || Vector2.Distance(nextTarget,locationToCheckAgainst) > Vector2.Distance(locationToCheckAgainst,unitScript.location))){
				nextTarget = unitScript.location;
			}
		}
		return nextTarget;
	}
}
