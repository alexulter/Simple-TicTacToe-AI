using UnityEngine;
using System.Collections;

public class AI_SimpleWeight : AI {

	private ArrayList moveSequence = new ArrayList();
	public string MYMARK;
	
	public AI_SimpleWeight (string[,]board, int length, bool clean) : base(board, length, clean)
	{}
	
	
	override public int[] Move()
	{
		
		ArrayList empty_tiles = FindEmptyTiles();
		if (empty_tiles.Count <= 0) {Debug.LogError("AI: ERROR"); return new int[]{-1,-1};}
		int I = -1,J = -1;
		int maxmax = -100500;
		foreach (int[] entry in empty_tiles)
			if (entry[0] > maxmax||(entry[0] == maxmax && Random.Range(0,100)>50)) 
		{
			maxmax = entry[0];
			I = entry[1];
			J = entry[2];
		}
		moveSequence.Add (ConvertBoard2Line(GetStateAfterMove(I,J)));
		return new int[]{I, J};
	}
	
	ArrayList FindEmptyTiles()
	{
		ArrayList empty_tiles = new ArrayList();
		for (int i = 0; i < Board_Size_X; i++)
			for (int j = 0; j < Board_Size_Y; j++)
			if (TicTacToeBoard[i,j] == " ") {
				if (isWin(TicTacToeBoard, i,j))
				{
					empty_tiles.Clear();
					empty_tiles.Add(new int[]{data.GetUtility (ConvertBoard2Line (GetStateAfterMove(i,j))),i,j});
					return empty_tiles;
					//return new int[]{i, j};
				}
				empty_tiles.Add(new int[]{data.GetUtility (ConvertBoard2Line (GetStateAfterMove(i,j))),i,j});
			}
		return empty_tiles;
	}
	
	//Add "0" to empty tile {I,J}
	private string[,] GetStateAfterMove(int I, int J)
	{
						//Debug.Log(I.ToString()+" "+J.ToString());
						if (TicTacToeBoard[I,J] != " ") Debug.LogError("Error: target tile was not empty");
						string[,] _VirtualBoard = new string[TicTacToeBoard.GetLength(0), 
							TicTacToeBoard.GetLength(1)];
						for (int i = 0; i < TicTacToeBoard.GetLength(0); i++)
							for (int j = 0; j < TicTacToeBoard.GetLength(1); j++)
								_VirtualBoard [i, j] = TicTacToeBoard [i,j];
						_VirtualBoard [I, J] = MYMARK;
						return _VirtualBoard;
	}
	
	
	bool isWin(string[,] initialBoard, int I, int J)
	{
		int[,] DIRECTION_SUM = {{0,0,0},{0,0,0},{0,0,0}};
		int i,j;
		for (int m = -1; m <= 1; m++)
			for (int n = -1; n <= 1; n++)
				if (!(m == 0 && n == 0)) 
			{
				//Initialize DIRECTION_SUM for chosen direction(m,n)
				DIRECTION_SUM [m+1, n+1] = 0;
				i = I+m;
				j = J+n;
				//keep going in direction(m,n) until 
				while ((isInRange(i,j))&&(initialBoard[i,j] == "O"))
				{
					//Debug.Log(i.ToString()+" "+j.ToString()+" "+TicTacToeBoard[i,j]);
					DIRECTION_SUM [m+1, n+1]++;
					i = i + m;
					j = j + n;
				}
				if ((DIRECTION_SUM[m+1,n+1] + DIRECTION_SUM[-m+1,-n+1] + 1) >= WIN_LENGTH) return true;
			}
			return false;
	}
	
	override public void FinishAI(int gameResult)
	{
		foreach (string entry in moveSequence)
			if (gameResult == 1) data.SetUtility(entry, 1);
		else if (gameResult == -1) data.SetUtility(entry, -1);
		
		moveSequence.Clear();
		data.SaveToFile();
	}
}
