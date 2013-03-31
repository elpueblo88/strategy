using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MakeGround : MonoBehaviour {
	
	/* Terrain Types
	 * Start Area: Start
	 * Goal (radioactive) Area: Goal
	 * Basic Area: Default
	 * Hill: Hill
	 * Rubble (impassible): Rubble
	 * Forest: Forest
	*/
	
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
	
	public GameObject[,] grid;
	public float xSize;
	public float zSize;
	TerrainScript terrainScript;
	
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
				
				setTerrainBlock(j, i, "Default", false);
			}
		}
	}
	
	void modifyGrid()
	{
		startModifyGrid();
		addFeatures();
	}
	
	void startModifyGrid()
	{
		int i, j;
		Component temp;
		
		makeGoalSpots();
		
		int zStart = (zLength/2) - (zStartSize/2);
		for(i = zStart; i < zStart + zStartSize; i++)
		{
			for(j = 0; j < xStartSize; j++)
			{
				setTerrainBlock(j,i, "Start", false);
				
				player1StartLocations[(i-zStart)*xStartSize + j] = new Vector2(j,i);
			}
		}
		
		for(i = zStart; i < zStart + zStartSize; i++)
		{
			for(j = xLength - 1; j + xStartSize > xLength - 1; j--)
			{
				setTerrainBlock(j,i, "Start", false);
				
				player2StartLocations[(i-zStart)*xStartSize + (j - xLength + xStartSize)] = new Vector2(j,i);
			}
		}
	}
	
	void makeGoalSpots()
	{
		int x, z;
		z = zLength/2;
		x = xLength/2;
		setTerrainBlock(x, z, "Goal", false);
		goalLocations[0] = new Vector2(z,x);
		
		z = zLength/3 - 2;
		x = xLength/3 - 1;
		setTerrainBlock(x, z, "Goal", false);
		goalLocations[1] = new Vector2(z,x);
		
		x = xLength*2/3;
		setTerrainBlock(x, z, "Goal", false);
		goalLocations[2] = new Vector2(z,x);
		
		z = zLength*2/3 + 1;
		setTerrainBlock(x, z, "Goal", false);
		goalLocations[3] = new Vector2(z,x);
		
		x = xLength/3 - 1;
		setTerrainBlock(x, z, "Goal", false);
		goalLocations[4] = new Vector2(z,x);
	}

	void addFeatures()
	{
		int xRand, zRand;
		int hillsLeft, rubblesLeft, forestsLeft;
		
		
		hillsLeft = hills;
		
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
	void setTerrainBlock(int x, int z, string type, bool childFlag, GameObject child = null)
	{
		TerrainScript t;
		
		GameObject tmp;
		
		tmp = childFlag ? child : grid[z,x];
		

		t = tmp.GetComponent("TerrainScript") as TerrainScript;
		
		if(t == null)
		{
			t = tmp.gameObject.AddComponent("TerrainScript") as TerrainScript;	
		}
		
		t.terrainType = type;
		t.movementCost = mCost[type];
		t.isAccessible = isAccessible[type];
		t.defenseBonus = defenseBonus[type];
		t.zValue = z;
		t.xValue = x;
		t.height = BLcorner.y + grid[z,x].transform.localScale.y/2;
		
		tmp.renderer.material = Resources.Load(materials[type], typeof(Material)) as Material;
		
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
	
	void setTerrainBlockIfGoal(int x, int z)
	{
		Component temp;	
		temp = grid[z, x].AddComponent("ParticleSystem");
		temp.particleSystem.startColor = new Color(0, 255, 0);
		temp.particleSystem.startLifetime = 3;
	}
	
	void setTerrainBlockIfHill(int x, int z)
	{
		GameObject hillClone = Instantiate(hill) as GameObject;
		hillClone.transform.parent = grid[z,x].gameObject.transform;
		hillClone.transform.localPosition = new Vector3(0, 1, 0);
		setTerrainBlock(x, z, "Hill", true, hillClone);
	}
	
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
