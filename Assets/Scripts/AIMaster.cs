using UnityEngine;
using System.Collections;

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
		}else{
			gameMaster = GameObject.Find("GameMaster");
			runAI ();
		}
	}
	
	private void updateAI(GameObject[] team2){
		this.team2 = team2;	
		
		foreach(GameObject unit in team2){
			UnitScript unitScript = unit.GetComponent<UnitScript>();
			GameMaster gameMasterScript = gameMaster.GetComponent<GameMaster>();
			
			gameMasterScript.changeGroundColorMaster(new PlayerMovement(unitScript.x,unitScript.z, unitScript.movement, 2));
			// find where they should go,
			
			// move them
			
			// attack if possible
		}
	}
}
