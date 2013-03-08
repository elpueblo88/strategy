using UnityEngine;
using System.Collections;

public class MakeGround : MonoBehaviour {
	
	public int xLength;
	public int zLength;
	public GameObject terrain;
	
	GameObject[,] grid;
	float xSize;
	float zSize;

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
		
		int i, j;
		Vector3 spot = Vector3.zero;
		
		for(i = zLength - 1; i >= 0; i--)
		{
			for(j = 0; j < xLength; j++)
			{
				grid[i,j] = Instantiate(terrain) as GameObject;
				spot.x = (j * xSize);
				spot.z = (i * zSize);
				grid[i,j].transform.localPosition = spot;
			}
		}
		
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
