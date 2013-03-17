using UnityEngine;
using System.Collections;

public class MakeGround : MonoBehaviour {
	
	/* Terrain Types
	 * Start Area: Start
	 * Goal (radioactive) Area: Goal
	 * Basic Area: Default
	 * Hill: Hill
	 * Rubble (impassible): Rubble
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
	
	//Factors that determine grid features
	public int hills;
	public int rubbles;
	public int forests;
	
	//Default grid feature numbers
	int hillsD = 10;
	int rubblesD = 10;
	int forestsD = 10;
	
	GameObject[,] grid;
	float xSize;
	float zSize;
	TerrainScript terrainScript;
	
	
	Vector2[] GoalSpots = {new Vector2(7,7), new Vector2(3,3), new Vector2(3,11), new Vector2(11,3), new Vector2(11,11)};

	// Use this for initialization
	void Start () 
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
	
	// Update is called once per frame
	void Update () 
	{
		
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
				
				terrainScript = grid[i,j].gameObject.AddComponent("TerrainScript") as TerrainScript;
				terrainScript.terrainType = "Default";
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
		for(i = 0; i < 5; i++)
		{
			grid[(int)GoalSpots[i].y, (int)GoalSpots[i].x].renderer.material = Resources.Load("Materials/radioactiveSpot", typeof(Material)) as Material;	
			temp = grid[(int)GoalSpots[i].y, (int)GoalSpots[i].x].AddComponent("ParticleSystem");
			temp.particleSystem.startColor = new Color(0, 255, 0);
			temp.particleSystem.startLifetime = 3;
			
			terrainScript = grid[(int)GoalSpots[i].y, (int)GoalSpots[i].x].GetComponent("TerrainScript") as TerrainScript;;
			terrainScript.terrainType = "Goal";
			
		}
		
		int zStart = (zLength/2) - (zStartSize/2);
		for(i = zStart; i < zStart + zStartSize; i++)
		{
			for(j = 0; j < xStartSize; j++)
			{
				grid[i,j].renderer.material = Resources.Load("Materials/StartingArea", typeof(Material)) as Material;
				terrainScript = grid[i,j].GetComponent("TerrainScript") as TerrainScript;;
				terrainScript.terrainType = "Start";
			}
		}
		
		for(i = zStart; i < zStart + zStartSize; i++)
		{
			for(j = xLength - 1; j + xStartSize > xLength - 1; j--)
			{
				grid[i,j].renderer.material = Resources.Load("Materials/StartingArea", typeof(Material)) as Material;
				terrainScript = grid[i,j].GetComponent("TerrainScript") as TerrainScript;;
				terrainScript.terrainType = "Start";
			}
		}
	}

	void addFeatures()
	{
		int xRand, zRand;
		int hillsLeft, rubblesLeft, forestsLeft;
		
		
		hillsLeft = hills;
		GameObject hillClone;
		
		while(hillsLeft > 0)
		{
			xRand = Random.Range(0, xLength - 1);
			zRand = Random.Range(0, zLength - 1);
			
			terrainScript = grid[zRand,xRand].gameObject.GetComponent("TerrainScript") as TerrainScript;
			if(terrainScript.terrainType != "Goal" && terrainScript.terrainType != "Start" && terrainScript.terrainType != "Hill")
			{
				terrainScript.terrainType = "Hill";
				hillsLeft--;
				
				hillClone = Instantiate(hill) as GameObject;
				hillClone.transform.parent = grid[zRand,xRand].gameObject.transform;
				hillClone.transform.localPosition = new Vector3(0, 1, 0);
				
				grid[zRand,xRand].gameObject.renderer.material = Resources.Load("Materials/Hill", typeof(Material)) as Material;
				hillClone.renderer.material = Resources.Load("Materials/Hill", typeof(Material)) as Material;
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
				terrainScript.terrainType = "Rubble";
				rubblesLeft--;
				
				rubblesClone = Instantiate(rubbleA) as GameObject;
				rubblesClone.transform.parent = grid[zRand,xRand].gameObject.transform;
				rubblesClone.transform.localPosition = new Vector3(0f, 1f, .3f);
				rubblesClone.renderer.material = Resources.Load("Materials/Rubble", typeof(Material)) as Material;
				
				rubblesClone = Instantiate(rubbleB) as GameObject;
				rubblesClone.transform.parent = grid[zRand,xRand].gameObject.transform;
				rubblesClone.transform.localPosition = new Vector3(0, 2, 0);
				rubblesClone.transform.localScale = new Vector3(.2f, 1.5f, .2f);
				rubblesClone.transform.localRotation = Quaternion.Euler(15, 0, 0);
				rubblesClone.renderer.material = Resources.Load("Materials/Rubble", typeof(Material)) as Material;
				
				rubblesClone = Instantiate(rubbleC) as GameObject;
				rubblesClone.transform.parent = grid[zRand,xRand].gameObject.transform;
				rubblesClone.transform.localPosition = new Vector3(0f, 1f, .07f);
				
				grid[zRand,xRand].gameObject.renderer.material = Resources.Load("Materials/Rubble", typeof(Material)) as Material;
				rubblesClone.renderer.material = Resources.Load("Materials/Rubble", typeof(Material)) as Material;
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
				terrainScript.terrainType = "Forest";
				forestsLeft--;
				
				grid[zRand,xRand].gameObject.renderer.material = Resources.Load("Materials/Forest", typeof(Material)) as Material;
			}
		}
		
	}




}
