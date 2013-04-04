using UnityEngine;
using System.Collections;

//object used to pass information about the position, amount the unit can move and 
//what team the unit is on
//Used for finding out where the unit can go and changing the grid color to show that
/// Author Daniel Pfeffer dnp19
public class PlayerMovement{
	
	public int x;
	public int z;
	public int moves;
	public int player;
	
	public PlayerMovement(int xx, int zz, int move, int play)
	{
		x = xx;
		z = zz;
		moves = move;
		player = play;
	}
}
