using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIMaster : MonoBehaviour {
	private const int PRIORITY_ROGUE = 1;
	
	// Useful information: objects in a team, and the game master
	private GameObject[] team1;
	private GameObject[] team2;
	private GameObject gameMaster;
	
	// Stores a units position
	private Vector2 locationToCheckAgainst;
	
	// Use this for initialization
	void Start () {
		gameMaster = GameObject.Find("GameMaster");
	}
	
	// Called when it is the AI's turn
	private void runAI(){
		if(gameMaster != null){
			// Get the units on this team
			this.team2 = gameMaster.GetComponent<UnitMaster>().team2;	
		
			/*Summary:
			 * For each unit find:
			 * 		a target
			 * 		the closest position to that target that can be moved to
			 * 		move to the position
			 * 		attack the target if it is in range
			 * 
			 * RadioChild objects are trying to move to certain tile
			 */
			GameMaster gameMasterScript = gameMaster.GetComponent<GameMaster>();
			MakeGround makeGroundScript = gameMaster.GetComponent<MakeGround>();
			foreach(GameObject unit in team2){
				if(unit != null){
					// Get the unitscript of the unit, and a couple of other scripts for easy data access
					UnitScript unitScript = unit.GetComponent<UnitScript>();
					
					// Get the positions that this unit can move to
					LinkedList<Vector2> positionList = gameMasterScript.changeGroundColorMaster(new PlayerMovement((int)unitScript.location.x,(int)unitScript.location.y, unitScript.moveSpeed, 2));
					
					/* Determines a target position (for moving to if it is a radiochild, or attacking otherwise)
					 * For every unit type besides the radioChild, the if statement is just a way to iterate through the
					 * 		different priority targets: 
					 * 		Example: for the rogue, it looks for a bomber to attack, and if it can't find one it looks for a brain and so on
					 */
				
					if(positionList.Count != 0){
						Vector2 targetPosition = new Vector2(-1,-1);
						locationToCheckAgainst = unitScript.location;
						if(unitScript.unitType == "radioChild"){
							// Search through the goal locations (radioactive spots) for the closest goal
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
						
						// If a target was found, look for the position that can be moved to which is closest to the target
						if(targetPosition.x != -1){
							Vector2 nextPosition = new Vector2(-1,-1);
							foreach(Vector2 location in positionList){
								if(unitScript.unitType == "radioChild" && location.Equals(unitScript.location)){
									continue;
								}
								
								// The radio child has a particular exception to prevent it from getting stuck (the exception is that its current location is not a valid position to move to)
								if(nextPosition.x == -1 || (Vector2.Distance(nextPosition,targetPosition) > Vector2.Distance(location,targetPosition))){
									nextPosition = location;
								}
							}
							
							// If such a position exists, move to it
							if(nextPosition.x != -1){
								unit.SendMessage("moveTo",nextPosition);
								
								// If a radio child reaches it's target goal, it claims it for the AI team
								if(unitScript.unitType == "radioChild" && nextPosition.x == targetPosition.x && nextPosition.y == targetPosition.y){
									makeGroundScript.grid[(int)targetPosition.y,(int)targetPosition.x].GetComponent<TerrainScript>().SendMessage("setTaken",2);
								}
							}
						}
					}
				
					// attack if possible
					
					//** Did not get combat to work
				}
			}
		}else{
			gameMaster = GameObject.Find("GameMaster");
			runAI ();
		}
		
		// signal that the AI is done
		this.gameObject.SendMessage("changeFromPlayerTwo");
	}
	
	// Method for finding a target based on its name
	private Vector2 findTarget(string targetType){
		Vector2 nextTarget = new Vector2(-1,-1);
		
		// Finds the closest target to the unit's position: unit's position is stored under locaitonToCheckAgainst
		this.team1 = gameMaster.GetComponent<UnitMaster>().team1;	
		foreach(GameObject unit in team1){
			if(unit != null){
				UnitScript unitScript = unit.GetComponent<UnitScript>();
				if(unitScript.unitType == targetType && (nextTarget.x == -1 || Vector2.Distance(nextTarget,locationToCheckAgainst) > Vector2.Distance(locationToCheckAgainst,unitScript.location))){
					nextTarget = unitScript.location;
				}
			}
		}
		return nextTarget;
	}
}
