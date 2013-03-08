using UnityEngine;
using System.Collections;

public class MakeGround : MonoBehaviour {
	
	public int xLength;
	public int zLength;
	public Vector3 BLcorner;
	public GameObject terrain;
	
	GameObject[,] grid;
	float xSize;
	float zSize;
	
	Vector2[] GoalSpots = {new Vector2(7,7), new Vector2(3,3), new Vector2(3,10), new Vector2(11,3), new Vector2(11,11)};

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
			}
		}
	}
	
	void modifyGrid()
	{
		int i;
		for(i = 0; i < 5; i++)
		{
			grid[(int)GoalSpots[i].y, (int)GoalSpots[i].x].renderer.material = Resources.Load("radioactiveSpot", typeof(Material)) as Material;	
		}
	}
}
