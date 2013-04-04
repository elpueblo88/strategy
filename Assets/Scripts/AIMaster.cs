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
			this.team2 = gameMaster.GetComponent<UnitMaster>().team2;	
		
			foreach(GameObject unit in team2){
				UnitScript unitScript = unit.GetComponent<UnitScript>();
				GameMaster gameMasterScript = gameMaster.GetComponent<GameMaster>();
				MakeGround makeGroundScript = gameMaster.GetComponent<MakeGround>();
				
				LinkedList<Vector2> positionList = gameMasterScript.changeGroundColorMaster(new PlayerMovement((int)unitScript.location.x,(int)unitScript.location.y, unitScript.moveSpeed, 2));
				
				
				/* Determines a target position (for moving to if it is a radiochild, or attacking otherwise)
				 * For every unit type besides the rogue, the if statement is just a way to iterate through the
				 * 		different priority targets: 
				 * 		Example: for the rogue, it looks for a bomber to attack, and if it can't find one it looks for a brain and so on
				 * 		
				 */
			
				if(positionList.Count != 0){
					Vector2 targetPosition = new Vector2(-1,-1);
					locationToCheckAgainst = unitScript.location;
					if(unitScript.unitType == "radioChild"){
						foreach(Vector2 goalLocation in makeGroundScript.goalLocations){
							int taken = makeGroundScript.grid[(int)goalLocation.y,(int)goalLocation.x].GetComponent<TerrainScript>().taken;
							if(taken == 0){
								if(targetPosition.x == -1 || (Vector2.Distance(targetPosition,unitScript.location) > Vector2.Distance(unitScript.location,goalLocation))){
									targetPosition = goalLocation;
								}
							}
						}					
					}else if(unitScript.unitType == "rogue"){
						if((targetPosition = findTarget("bomber")).x != -1 
							|| (targetPosition = findTarget("brain")).x != -1 
							|| (targetPosition = findTarget("radioChild")).x != -1  
							|| (targetPosition = findTarget("rogue")).x != -1
							|| (targetPosition = findTarget("brute")).x != -1){
						}
					}else if(unitScript.unitType == "bomber"){
						if((	targetPosition = findTarget("radioChild")).x != -1 
							|| (targetPosition = findTarget("brute")).x != -1 
							|| (targetPosition = findTarget("rogue")).x != -1  
							|| (targetPosition = findTarget("brain")).x != -1
							|| (targetPosition = findTarget("bomber")).x != -1){
						}
					}else if(unitScript.unitType == "brain"){
						if((	targetPosition = findTarget("brute")).x != -1 
							|| (targetPosition = findTarget("radioChild")).x != -1 
							|| (targetPosition = findTarget("brain")).x != -1  
							|| (targetPosition = findTarget("bomber")).x != -1
							|| (targetPosition = findTarget("rogue")).x != -1){
						}
					}else if(unitScript.unitType == "brute"){
						if((	targetPosition = findTarget("radioChilld")).x != -1 
							|| (targetPosition = findTarget("rogue")).x != -1 
							|| (targetPosition = findTarget("brute")).x != -1  
							|| (targetPosition = findTarget("bomber")).x != -1
							|| (targetPosition = findTarget("brain")).x != -1){
						}
					}
					
					if(targetPosition.x != -1){
						Vector2 nextPosition = new Vector2(-1,-1);
						foreach(Vector2 location in positionList){
							if(unitScript.unitType == "radioChild" && location.Equals(unitScript.location)){
								continue;
							}
							
							if(nextPosition.x == -1 || (Vector2.Distance(nextPosition,targetPosition) > Vector2.Distance(location,targetPosition))){
								nextPosition = location;
							}
						}
						
						if(nextPosition.x != -1){
							unit.SendMessage("moveTo",nextPosition);
							
							if(unitScript.unitType == "radioChild" && nextPosition.x == targetPosition.x && nextPosition.y == targetPosition.y){
								makeGroundScript.grid[(int)targetPosition.y,(int)targetPosition.x].GetComponent<TerrainScript>().SendMessage("setTaken",2);
							}
						}
					}
				}
				
				
			
				// attack if possible
			}
		}else{
			gameMaster = GameObject.Find("GameMaster");
			runAI ();
		}
		
		this.gameObject.SendMessage("changeFromPlayerTwo");
	}
	
	private Vector2 findTarget(string targetType){
		Vector2 nextTarget = new Vector2(-1,-1);
		
		this.team1 = gameMaster.GetComponent<UnitMaster>().team1;	
		foreach(GameObject unit in team1){
			UnitScript unitScript = unit.GetComponent<UnitScript>();
			if(unitScript.unitType == targetType && (nextTarget.x == -1 || Vector2.Distance(nextTarget,locationToCheckAgainst) > Vector2.Distance(locationToCheckAgainst,unitScript.location))){
				nextTarget = unitScript.location;
			}
		}
		return nextTarget;
	}
}
