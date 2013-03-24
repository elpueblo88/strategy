using UnityEngine;
using System.Collections;

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
