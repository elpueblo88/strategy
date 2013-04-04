using UnityEngine;
using System.Collections;

public class UnitMaster : MonoBehaviour
{
	MakeGround ground;
	TerrainScript terrain;
	
	public GameObject Brute;
	public GameObject RadioChild;
	public GameObject Rogue;
	public GameObject Brain;
	public GameObject Bomber;
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
		GameMaster = GameObject.FindGameObjectWithTag("GameMaster");
		ground = GameMaster.GetComponent<MakeGround>();
		if(!ground)
			Debug.Log ("ground not found");
		
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
		if (!RadioChild) {
			RadioChild = (GameObject)Resources.Load("RadioChild");
		}
		if (!Rogue) {
			Rogue = (GameObject)Resources.Load("Rogue");
		}
		if (!Bomber) {
			Bomber = (GameObject)Resources.Load("Bomber");
		}
		if (!Brain) {
			Brain = (GameObject)Resources.Load("Brain");
		}
		if (!Brute) {
			Brute = (GameObject)Resources.Load("Brute");
		}
	}
	
	void teamSetup (){
//		Debug.Log("Message recieved");
		Start ();
		setDefaults();
		team1Make();
		team2Make();
	}
	
	void team1Make (){
//		Debug.Log ("Team1");
		Vector2[] start = ground.player1StartLocations;
		Vector3 location = Vector3.zero;
		team1 = new GameObject[teamSize];
		int[] unitTypes = {radioChild, rogue, bomber, brain, brute};
		
		int loopMax = start.Length;
		if(teamSize < start.Length)
			loopMax = teamSize;
		for(int i = (loopMax - 1); i >= 0 ; i--){
			terrain = ground.grid[(int)start[i].y, (int)start[i].x].GetComponent<TerrainScript>();
			terrain.SendMessage("switchOccupied");
			location = ground.grid[(int)start[i].y, (int)start[i].x].transform.localPosition;
			location.y += 5;
			UnitScript u;
			if(unitTypes[0] > 0){
				team1[i] = Instantiate(RadioChild) as GameObject;
				team1[i].transform.localPosition = location;
				u = team1[i].AddComponent("UnitScript") as UnitScript;
				setUnit(team1[i], "radioChild");
				unitTypes[0]--;
			}else if(unitTypes[1] > 0){
				team1[i] = Instantiate(Rogue) as GameObject;
				team1[i].transform.localPosition = location;
				u = team1[i].AddComponent("UnitScript") as UnitScript;
				setUnit(team1[i], "rogue");
				unitTypes[1]--;
			}else if(unitTypes[2] > 0){
				team1[i] = Instantiate(Bomber) as GameObject;
				team1[i].transform.localPosition = location;
				u = team1[i].AddComponent("UnitScript") as UnitScript;
				setUnit(team1[i], "bomber");
				unitTypes[2]--;
			}else if(unitTypes[3] > 0){
				team1[i] = Instantiate(Brain) as GameObject;
				team1[i].transform.localPosition = location;
				u = team1[i].AddComponent("UnitScript") as UnitScript;
				setUnit(team1[i], "brain");
				unitTypes[3]--;
			}else {
				team1[i] = Instantiate(Brute) as GameObject;
				team1[i].transform.localPosition = location;
				u = team1[i].AddComponent("UnitScript") as UnitScript;
				setUnit(team1[i], "brute");
			}
			u.location = start[i];
			setTeam (team1[i], 1);	
			
//			team1[i].renderer.material.color = Color.blue;
		}
//		Debug.Log("Team1Complete");
	}
	
	void team2Make (){
//		Debug.Log ("Team2");
		Vector2[] start = ground.player2StartLocations;
		Vector3 location = Vector3.zero;
		team2 = new GameObject[teamSize];
		int[] unitTypes = {radioChild, rogue, bomber, brain, brute};
		
		for(int i = 0; i < start.Length && i < teamSize; i++){
			location = ground.grid[(int)start[i].y, (int)start[i].x].transform.localPosition;
			location.y += 5;
			UnitScript u;
			if(unitTypes[0] > 0){
				team2[i] = Instantiate(RadioChild) as GameObject;
				team2[i].transform.localPosition = location;
				u = team2[i].AddComponent("UnitScript") as UnitScript;
				setUnit(team2[i], "radioChild");
				unitTypes[0]--;
			}else if(unitTypes[1] > 0){
				team2[i] = Instantiate(Rogue) as GameObject;
				team2[i].transform.localPosition = location;
				u = team2[i].AddComponent("UnitScript") as UnitScript;
				setUnit(team2[i], "rogue");
				unitTypes[1]--;
			}else if(unitTypes[2] > 0){
				team2[i] = Instantiate(Bomber) as GameObject;
				team2[i].transform.localPosition = location;
				u = team2[i].AddComponent("UnitScript") as UnitScript;
				setUnit(team2[i], "bomber");
				unitTypes[2]--;
			}else if(unitTypes[3] > 0){
				team2[i] = Instantiate(Brain) as GameObject;
				team2[i].transform.localPosition = location;
				u = team2[i].AddComponent("UnitScript") as UnitScript;
				setUnit(team2[i], "brain");
				unitTypes[3]--;
			}else {
				team2[i] = Instantiate(Brute) as GameObject;
				team2[i].transform.localPosition = location;
				u = team2[i].AddComponent("UnitScript") as UnitScript;
				setUnit(team2[i], "brute");
			}
			u.location = start[i];
			setTeam (team2[i], 2);
//			team2[i].renderer.material.color = Color.red;
		}
	}
	
	void setUnit(GameObject unit, string type){
		UnitScript u;
		u = unit.GetComponent("UnitScript") as UnitScript;
		u.unitType = type;
		if(type == "radioChild"){
			u.hp = 100;
			u.moveSpeed = 2;
			u.atkPower = 100;
		}
		if(type == "rogue"){
			u.hp = 500;
			u.moveSpeed = 4;
			u.atkPower = 300;
		}
		if(type == "bomber"){
			u.hp = 300;
			u.moveSpeed = 4;
			u.atkPower = 1000;
		}
		if(type == "brain"){
			u.hp = 300;
			u.moveSpeed = 3;
			u.atkPower = 0;
		}
		if(type == "brute"){
			u.hp = 1000;
			u.moveSpeed = 2;
			u.atkPower = 400;
		}
	}
	
	void setTeam(GameObject unit, int team){
		UnitScript u = unit.GetComponent("UnitScript") as UnitScript;
		u.team = team;
	}
}

