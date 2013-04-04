using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//	Makes the ground and stores all of the information about the board intself
/// Author Daniel Pfeffer dnp19
public class MakeGround : MonoBehaviour {
	
	/* Terrain Types
	 * Start Area: Start
	 * Goal (radioactive) Area: Goal
	 * Basic Area: Default
	 * Hill: Hill
	 * Rubble (impassible): Rubble
	 * Forest: Forest
	*/
	
	//gets board length and makes gameobjects for the different terrain features
	public int xLength;
	public int zLength;
	public Vector3 BLcorner;
	public GameObject terrain;
	public GameObject hill;
	public GameObject rubbleA;
	public GameObject rubbleB;
	public GameObject rubbleC;
	
	//size of starting area
	public int xStartSize;
	public int zStartSize;
	
	//arrays holding locations of start areas and goals
	public Vector2[] player1StartLocations;
	public Vector2[] player2StartLocations;
	public Vector2[] goalLocations = new Vector2[5];
	
	//Factors that determine grid features
	public int hills;
	public int rubbles;
	public int forests;
	
	//Default grid feature numbers
	int hillsD = 10;
	int rubblesD = 10;
	int forestsD = 10;
	
	//makes grid and terrain script for the individual terrains
	public GameObject[,] grid;
	public float xSize;
	public float zSize;
	TerrainScript terrainScript;
	
	//values for the terrain attributes
	public int mCostDefault;
	public int mCostStart;
	public int mCostGoal;
	public int mCostHill;
	public int mCostForest;
	public int mCostRubble;
	
	public bool isAccessibleDefault;
	public bool isAccessibleStart;
	public bool isAccessibleGoal;
	public bool isAccessibleHill;
	public bool isAccessibleForest;
	public bool isAccessibleRubble;
	
	public int defenseBonusDefault;
	public int defenseBonusStart;
	public int defenseBonusGoal;
	public int defenseBonusHill;
	public int defenseBonusForest;
	public int defenseBonusRubble;
	
	//stores values for the terrains based off of their types
	public Dictionary<string,int> mCost = new Dictionary<string,int>();
	public Dictionary<string,bool> isAccessible = new Dictionary<string,bool>();
	public Dictionary<string,int> defenseBonus = new Dictionary<string,int>();
	public Dictionary<string, string> materials = new Dictionary<string, string>();

	// Use this for initialization
	void Start () 
	{		
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	//sets default values for terrain attribute if none exist
	void setTerrainAttributes()
	{
		if(mCostDefault == 0)
		{
			mCostDefault = 1;
		}
		if(mCostStart == 0)
		{
			mCostStart = 1;	
		}
		if(mCostGoal == 0)
		{
			mCostGoal = 1;
		}
		if(mCostHill == 0)
		{
			mCostHill = 1;
		}
		if(mCostForest == 0)
		{
			mCostForest = 1;
		}
		
		mCost["Default"] = mCostDefault;
		mCost["Start"] = mCostStart;
		mCost["Goal"] = mCostGoal;
		mCost["Hill"] = mCostHill;
		mCost["Forest"] = mCostForest;
		mCost["Rubble"] = 0;
		
		isAccessible["Default"] = isAccessibleDefault;
		isAccessible["Start"] = isAccessibleStart;
		isAccessible["Goal"] = isAccessibleGoal;
		isAccessible["Hill"] = isAccessibleHill;
		isAccessible["Forest"] = isAccessibleForest;
		isAccessible["Rubble"] = isAccessibleRubble;
		
		defenseBonus["Default"] = defenseBonusDefault;
		defenseBonus["Start"] = defenseBonusStart;
		defenseBonus["Goal"] = defenseBonusGoal;
		defenseBonus["Hill"] = defenseBonusHill;
		defenseBonus["Forest"] = defenseBonusForest;
		defenseBonus["Rubble"] = defenseBonusRubble;
		
		materials["Default"] ="Materials/BasicGround";
		materials["Start"] = "Materials/StartingArea";
		materials["Goal"] = "Materials/radioactiveSpot";
		materials["Hill"] = "Materials/Hill";
		materials["Forest"] = "Materials/Forest";	
		materials["Rubble"] = "Materials/Rubble";
	}
	
	//sets lots of default values
	void startMakeGrid()
	{
		if(xLength == 0)
		{
			xLength = 15;	
		}
		if(zLength == 0)
		{
			zLength = 15;	
		}
		
		if(!terrain)
		{
			terrain = (GameObject)Resources.Load("Terrain");
		}
		if(!hill)
		{
			hill = (GameObject)Resources.Load("Hill");
		}
		if(!rubbleA)
		{
			rubbleA = (GameObject)Resources.Load("RubbleA");
		}
		if(!rubbleB)
		{
			rubbleB = (GameObject)Resources.Load("RubbleB");
		}
		if(!rubbleC)
		{
			rubbleC = (GameObject)Resources.Load("RubbleC");
		}
		
		if(xStartSize == 0)
		{
			xStartSize = 3;
		}
		if(zStartSize == 0)
		{
			zStartSize = 5;
		}
		
		player1StartLocations = new Vector2[xStartSize*zStartSize];
		player2StartLocations = new Vector2[xStartSize*zStartSize];
		
		if(hills == 0)
		{
			hills = hillsD;	
		}
		if(rubbles == 0)
		{
			rubbles = rubblesD;	
		}
		if(forests == 0)
		{
			forests = forestsD;	
		}
		
		xSize = terrain.transform.localScale.x;
		zSize = terrain.transform.localScale.z;
		grid = new GameObject[zLength, xLength];
		
		makeGrid ();
		modifyGrid();
	}
	
	//actually makes the base of the grid
	void makeGrid()
	{
		int i, j;
		Vector3 spot = Vector3.zero;
		
		for(i = zLength - 1; i >= 0; i--)
		{
			for(j = 0; j < xLength; j++)
			{
				grid[i,j] = Instantiate(terrain) as GameObject;
				spot.x = (j * xSize + BLcorner.x);
				spot.z = (i * zSize + BLcorner.z);
				spot.y = BLcorner.y;
				grid[i,j].transform.localPosition = spot;
				
				//sets every tile as default
				setTerrainBlock(j, i, "Default", false);
			}
		}
	}
	
	//starts adding features to grid
	void modifyGrid()
	{
		startModifyGrid();
		addFeatures();
	}
	
	//actually adds features to grid
	void startModifyGrid()
	{
		int i, j;
		Component temp;
		
		//makes the goal spots
		makeGoalSpots();
		
		//makes the player 1 starting area
		int zStart = (zLength/2) - (zStartSize/2);
		for(i = zStart; i < zStart + zStartSize; i++)
		{
			for(j = 0; j < xStartSize; j++)
			{
				setTerrainBlock(j,i, "Start", false);
				
				player1StartLocations[(i-zStart)*xStartSize + j] = new Vector2(j,i);
			}
		}
		
		//makes player 2 starting area
		for(i = zStart; i < zStart + zStartSize; i++)
		{
			for(j = xLength - 1; j + xStartSize > xLength - 1; j--)
			{
				setTerrainBlock(j,i, "Start", false);
				
				player2StartLocations[(i-zStart)*xStartSize + (j - xLength + xStartSize)] = new Vector2(j,i);
			}
		}
	}
	
	//method to make goal spots 
	void makeGoalSpots()
	{
		//puts one in direct center and others around it
		//as corners to sqare that take up a third of the length and height
		int x, z;
		z = zLength/2;
		x = xLength/2;
		//setTerrainBlock(x, z, "Goal", false);
		setTerrainBlock(z, x, "Goal", false);
		goalLocations[0] = new Vector2(z,x);
		
		z = zLength/3;// - 2;
		x = xLength/3 - 1;
		//setTerrainBlock(x, z, "Goal", false);
		setTerrainBlock(z, x, "Goal", false);
		goalLocations[1] = new Vector2(z,x);
		
		x = xLength*2/3;
		//setTerrainBlock(x, z, "Goal", false);
		setTerrainBlock(z, x, "Goal", false);
		goalLocations[2] = new Vector2(z,x);
		
		z = zLength*2/3 - 1;
		//setTerrainBlock(x, z, "Goal", false);
		setTerrainBlock(z, x, "Goal", false);
		goalLocations[3] = new Vector2(z,x);
		
		x = xLength/3 - 1;
		//setTerrainBlock(x, z, "Goal", false);
		setTerrainBlock(z, x, "Goal", false);
		goalLocations[4] = new Vector2(z,x);
	}
	
	//goes through and adds hills, rubbles and forests in random spots based off of amount given to make
	void addFeatures()
	{
		int xRand, zRand;
		int hillsLeft, rubblesLeft, forestsLeft;
		
		
		hillsLeft = hills;
		
		//picks a random spot and adds hilss until they are all gone
		while(hillsLeft > 0)
		{
			xRand = Random.Range(0, xLength - 1);
			zRand = Random.Range(0, zLength - 1);
			
			terrainScript = grid[zRand,xRand].gameObject.GetComponent("TerrainScript") as TerrainScript;
			if(terrainScript.terrainType != "Goal" && terrainScript.terrainType != "Start" && terrainScript.terrainType != "Hill")
			{
				hillsLeft--;
				setTerrainBlock(xRand, zRand, "Hill", false);
			}
		}
		
		//adds rubble until they are all made
		rubblesLeft = rubbles;
		GameObject rubblesClone;
		while(rubblesLeft > 0)
		{
			xRand = Random.Range(0, xLength - 1);
			zRand = Random.Range(0, zLength - 1);
			
			terrainScript = grid[zRand,xRand].gameObject.GetComponent("TerrainScript") as TerrainScript;
			if(terrainScript.terrainType != "Goal" && terrainScript.terrainType != "Start" && terrainScript.terrainType != "Hill"  && terrainScript.terrainType != "Rubble")
			{
				rubblesLeft--;
				setTerrainBlock(xRand, zRand, "Rubble", false);
			}			
		}
		
		//adds forests until they are all made
		forestsLeft = forests;
		while(forestsLeft > 0)
		{
			xRand = Random.Range(0, xLength - 1);
			zRand = Random.Range(0, zLength - 1);
			
			terrainScript = grid[zRand,xRand].gameObject.GetComponent("TerrainScript") as TerrainScript;
			if(terrainScript.terrainType != "Goal" && terrainScript.terrainType != "Start" && terrainScript.terrainType != "Hill" && terrainScript.terrainType != "Rubble" && terrainScript.terrainType != "Forest")
			{
				setTerrainBlock(xRand, zRand, "Forest", false);
				forestsLeft--;
			}
		}
		
	}	
	
	//childFlag lets the function know if this is for the child of a terrain block if true
	//method that makes a terrain object at a given location into different terrain types
	void setTerrainBlock(int x, int z, string type, bool childFlag, GameObject child = null)
	{
		TerrainScript t;
		
		GameObject tmp;
		
		//tmp is child if the thing to modify is not the grid location but a child on it
		tmp = childFlag ? child : grid[z,x];
		

		t = tmp.GetComponent("TerrainScript") as TerrainScript;
		
		if(t == null)
		{
			t = tmp.gameObject.AddComponent("TerrainScript") as TerrainScript;	
		}
		
		//modifies attributes of the terrain
		t.terrainType = type;
		t.movementCost = mCost[type];
		t.isAccessible = isAccessible[type];
		t.defenseBonus = defenseBonus[type];
		t.zValue = z;
		t.xValue = x;
		t.height = BLcorner.y + grid[z,x].transform.localScale.y/2;
		t.taken = 0;
		t.occupied = false;
		
		tmp.renderer.material = Resources.Load(materials[type], typeof(Material)) as Material;
		
		//do different things for goals, hills and rubble
		//sets attributes of children or radioactive stuff
		if(type == "Goal")
		{
			setTerrainBlockIfGoal(x,z);
		}
		if(type == "Hill")
		{
			t.height = t.height + hill.transform.localPosition.y/2;
			if(!childFlag)
			{
				setTerrainBlockIfHill(x, z);	
			}
		}
		if(type == "Rubble" && !childFlag)
		{
			setTerrainBlockIfRubble(x, z);	
		}
	}
	
	//makes particle system for goal spot
	void setTerrainBlockIfGoal(int x, int z)
	{
		Component temp;	
		temp = grid[z, x].AddComponent("ParticleSystem");
		temp.particleSystem.startColor = new Color(0, 255, 0);
		temp.particleSystem.startLifetime = 3;
	}
	
	//adds hill for a hill
	void setTerrainBlockIfHill(int x, int z)
	{
		GameObject hillClone = Instantiate(hill) as GameObject;
		hillClone.transform.parent = grid[z,x].gameObject.transform;
		hillClone.transform.localPosition = new Vector3(0, 1, 0);
		setTerrainBlock(x, z, "Hill", true, hillClone);
	}
	
	//adds rubble parts for the rubble
	void setTerrainBlockIfRubble(int x, int z)
	{
		GameObject rubblesClone;
		
		rubblesClone = Instantiate(rubbleA) as GameObject;
		rubblesClone.transform.parent = grid[z,x].gameObject.transform;
		rubblesClone.transform.localPosition = new Vector3(0f, 1f, .3f);
		setTerrainBlock(x,z,"Rubble", true, rubblesClone);
		
		rubblesClone = Instantiate(rubbleB) as GameObject;
		rubblesClone.transform.parent = grid[z,x].gameObject.transform;
		rubblesClone.transform.localPosition = new Vector3(0, 2, 0);
		rubblesClone.transform.localScale = new Vector3(.2f, 1.5f, .2f);
		rubblesClone.transform.localRotation = Quaternion.Euler(15, 0, 0);
		setTerrainBlock(x,z,"Rubble", true, rubblesClone);
		
		rubblesClone = Instantiate(rubbleC) as GameObject;
		rubblesClone.transform.parent = grid[z,x].gameObject.transform;
		rubblesClone.transform.localPosition = new Vector3(0f, 1f, .07f);
		setTerrainBlock(x,z,"Rubble", true, rubblesClone);
	}
}
