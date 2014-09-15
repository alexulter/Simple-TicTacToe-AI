using UnityEngine;
using System.Collections;

public class AI_Algorythmed : AI {

	public AI_Algorythmed (string[,]board, int length, bool clean) : base(board, length, clean)
	{
	}
	
	override public int[] Move()
	{
		ArrayList empty_tiles = new ArrayList ();
		for (int i = 0; i < Board_Size_X; i++)
			for (int j = 0; j < Board_Size_Y; j++)
			if (TicTacToeBoard[i,j] == " ") {
				empty_tiles.Add(new int[]{Goodness(i,j),i,j});
			}
		if (empty_tiles.Count <= 0) {Debug.Log("AI: ERROR"); return new int[]{-1,-1};}
		int I = -1,J = -1, maxmax = -1;
		foreach (int[] entry in empty_tiles)
			if (entry[0] > maxmax||(entry[0] == maxmax && Random.Range(0,100)>50)) 
		{
			maxmax = entry[0];
			I = entry[1];
			J = entry[2];
		}
		return new int[]{I, J};
	}
	
	private int Goodness (int I, int J)
	{
		//string[,] VirtualBoard = new string[Board_Size_X, Board_Size_Y];
		if (TicTacToeBoard [I, J] != " ")
			return -1;
		string INPUT = "O";
		int i, j, length, WinWaysCount = 0;
		int[,] DIRECTION_SUM = {{-1,-1,-1},{-1,-1,-1},{-1,-1,-1}};
		
		for (int m = -1; m <= 1; m++)
			for (int n = -1; n <= 1; n++)
				if (!(m == 0 && n == 0)) 
			{
				i = I;
				j = J;
				//keep going in direction(m,n) until 
				while (isInRange(i,j)&&((TicTacToeBoard[i,j] == " ")||(TicTacToeBoard[i,j] == INPUT)))
				{
					DIRECTION_SUM [m+1, n+1]++;
					//if (TicTacToeBoard[i,j] == INPUT) {DIRECTION_SUM [m+1, n+1] += 10;}
					i = i + m;
					j = j + n;
				}
				
				if (DIRECTION_SUM[-m+1,-n+1] >= 0)
				{
					length = DIRECTION_SUM[m+1,n+1] + DIRECTION_SUM[-m+1,-n+1] + 1;
					if (length >= WIN_LENGTH) WinWaysCount += length - WIN_LENGTH + 1;
				}
				
			}
		return WinWaysCount;
	}
}
