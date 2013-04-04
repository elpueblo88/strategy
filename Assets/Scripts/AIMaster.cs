using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIMaster : MonoBehaviour {

	private GameObject[] team2;
	private bool waitForUpdate;
	private GameObject gameMaster;
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
				
				if(unitScript.unitType == "radioChild"){
					Vector2 bestPosition;
					if(positionList.Count != 0){
						foreach(Vector2 goalLocation in makeGroundScript){
							/*
							if(bestPosition == null && makeGroundScript.grid[goalLocation.y][goalLocation.x].GetComponent<TerrainScript>().){
								bestPosition = goalLocation;
							}else{
								if(Vector2.Distance()	
							}*/
						}
					}else{
						
					}
					
					
				}else if(unitScript.unitType == "rogue"){
					
				}else if(unitScript.unitType == "bomber"){
					
				}else if(unitScript.unitType == "brain"){
					
				}else if(unitScript.unitType == "brute"){
					
				}
			
				// attack if possible
			}
		}else{
			gameMaster = GameObject.Find("GameMaster");
			runAI ();
		}
	}
}
