using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {
	// Types
	public const int TYPE_NONE = 0;
	public const int TYPE_RADIO_CHILD = 1;
	public const int TYPE_BRAIN = 2;
	public const int TYPE_BRUTE = 3;
	public const int TYPE_BOMBER = 4;
	public const int TYPE_ROGUE = 5;
	
	// Indices for setProperties function
	public const int I_TYPE = 0;
	public const int I_P1_UNIT = 1;
	public const int I_P1_CONTROL = 2;
	public const int I_POSITION_X = 3;
	public const int I_POSITION_Z = 4;
	
	private bool initialized = false;
	
	private GameObject gameMaster;
	
	/* Unit properties
	 * type: unit type
	 * movement: amount of spaces it can move
	 * health: amount of damage it can take before death
	 * power: amount of attack damage it does
	 * defense: amount of attack damage it can ignore
	 * player1Unit: if it belongs to player1
	 * player1Control: if it is being controlled by player1: used for brutes under brain control
	 */
	
	private int type = TYPE_NONE;
	private int movement;
	private float health;
	private float power;
	private float defense;
	private bool player1Unit;
	private bool player1Control;
	
	// Use this for initialization
	void Start () {
		gameMaster = GameObject.Find(GameMaster.TAG);
	}
	
	// Update is called once per frame
	void Update () {
		if(gameMaster == null){
			gameMaster = GameObject.Find(GameMaster.TAG);
		}
		
		
	}
	
	void OnMouseDown(){
		if(gameMaster != null){
			gameMaster.SendMessage("updateUnitInfo",new float[]{this.type,this.health,this.power,this.defense,this.movement});	
		}
	}
	
	// Takes an array of properties: See the above constants that start with "I"
	void setProperties(int[] properties){
		if(properties.Length != 5){
			return;	
		}
		
		if(!initialized){
			initialized = true;	
			type = properties[I_TYPE];
			player1Unit = (properties[I_P1_UNIT] == 1);
			player1Control = (properties[I_P1_CONTROL] == 1);
			this.gameObject.transform.position = new Vector3(properties[I_POSITION_X],10,properties[I_POSITION_Z]);
			switch(type){
			case TYPE_RADIO_CHILD:
				movement = 1;
				health = 1;
				power = 1;
				defense = 1;
				break;
			case TYPE_BRAIN:
				movement = 1;
				health = 1;
				power = 1;
				defense = 1;
				break;
			case TYPE_BRUTE:
				movement = 1;
				health = 1;
				power = 1;
				defense = 1;
				break;
			case TYPE_BOMBER:
				movement = 1;
				health = 1;
				power = 1;
				defense = 1;
				break;
			case TYPE_ROGUE:
				movement = 1;
				health = 1;
				power = 1;
				defense = 1;
				break;
			default:
				initialized = false;
				type = TYPE_NONE;
				break;
			}
		}
	}
}
