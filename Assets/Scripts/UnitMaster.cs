using UnityEngine;
using System.Collections;

public class UnitMaster : MonoBehaviour
{
	MakeGround ground;
	
	public GameObject Unit;
	public GameObject GameMaster;
	
	public int teamSize;
	public int radioChild;
	public int rogue;
	public int bomber;
	public int brain;
	public int brute;
	
	public GameObject[] team1;
	public GameObject[] team2;
	// Use this for initialization
	void Start ()
	{
		ground = gameObject.GetComponent<MakeGround>();
		
		
		
	}
	
	void setDefaults () {
		if (teamSize == 0) {
			teamSize = 9;
		}else if(teamSize > 15){
			teamSize = 15;
		}
		if(radioChild == 0){
			radioChild = 1;
		}
		if(rogue == 0){
			rogue = 2;
		}
		if(bomber == 0){
			bomber = 1;
		}
		if(brain == 0){
			brain = 2;
		}
		if(brute == 0){
			brute = teamSize - (radioChild + rogue + bomber + brain);
		}
		if (!Unit) {
			Unit = (GameObject)Resources.Load("Unit");
		}
	}
	
	void teamSetup (){
		Debug.Log("Message recieved");
		setDefaults();
		team1Make();
		team2Make();
	}
	
	void team1Make (){
		Debug.Log ("Team1");
		Vector2[] start = ground.player1StartLocations;
		Vector3 location = Vector3.zero;
		team1 = new GameObject[teamSize];
		for(int i = 0; i < start.Length && i < teamSize; i++){
			team1[i] = Instantiate(Unit) as GameObject;
			location = ground.grid[(int)start[i].y, (int)start[i].x].transform.localPosition;
			location.y += 5;
			team1[i].transform.localPosition = location;
		}
		
		int[] unitTypes = {radioChild, rogue, bomber, brain, brute};
		for(int i = 0; i < teamSize; i++){
			UnitScript u;
			u = team1[i].AddComponent("UnitScript") as UnitScript;
			u.location = start[i];
			if(unitTypes[0] > 0){
				setUnit(team1[i], "radioChild");
				unitTypes[0]--;
			}else if(unitTypes[1] > 0){
				setUnit(team1[i], "rogue");
				unitTypes[1]--;
			}else if(unitTypes[2] > 0){
				setUnit(team1[i], "bomber");
				unitTypes[2]--;
			}else if(unitTypes[3] > 0){
				setUnit(team1[i], "brain");
				unitTypes[3]--;
			}else {
				setUnit(team1[i], "brute");
			}
			setTeam (team1[i], 1);
		}
		Debug.Log("Team1Complete");
	}
	
	void team2Make (){
		Debug.Log ("Team2");
		Vector2[] start = ground.player2StartLocations;
		Vector3 location = Vector3.zero;
		team2 = new GameObject[teamSize];
		for(int i = 0; i < start.Length && i < teamSize; i++){
			team2[i] = Instantiate(Unit) as GameObject;
			location = ground.grid[(int)start[i].y, (int)start[i].x].transform.localPosition;
			location.y += 5;
			team2[i].transform.localPosition = location;
		}
		
		int[] unitTypes = {radioChild, rogue, bomber, brain, brute};
		for(int i = 0; i < teamSize; i++){
			UnitScript u;
			u = team2[i].AddComponent("UnitScript") as UnitScript;
			u.location = start[i];
			if(unitTypes[0] > 0){
				setUnit(team2[i], "radioChild");
				unitTypes[0]--;
			}else if(unitTypes[1] > 0){
				setUnit(team2[i], "rogue");
				unitTypes[1]--;
			}else if(unitTypes[2] > 0){
				setUnit(team2[i], "bomber");
				unitTypes[2]--;
			}else if(unitTypes[3] > 0){
				setUnit(team2[i], "brain");
				unitTypes[3]--;
			}else {
				setUnit(team2[i], "brute");
			}
			setTeam (team2[i], 2);
		}
	}
	
	void setUnit(GameObject unit, string type){
		UnitScript u;
		u = unit.GetComponent("UnitScript") as UnitScript;
		u.unitType = type;
		if(type == "radioChild"){
			u.hp = 100;
			u.moveSpeed = 2;
		}
		if(type == "rogue"){
			u.hp = 500;
			u.moveSpeed = 4;
		}
		if(type == "bomber"){
			u.hp = 300;
			u.moveSpeed = 4;
		}
		if(type == "brain"){
			u.hp = 300;
			u.moveSpeed = 3;
		}
		if(type == "brute"){
			u.hp = 900;
			u.moveSpeed = 2;
		}
	}
	
	void setTeam(GameObject unit, int team){
		UnitScript u = unit.GetComponent("UnitScript") as UnitScript;
		u.team = team;
	}
}

