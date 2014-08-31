using UnityEngine;
using System.Collections;
using System.IO;

public class AI
{

	private int Board_Size_X, Board_Size_Y;
	private int WIN_LENGTH;
	private string[,] TicTacToeBoard;
	public TypeOfAI AIType;

	private string INPUT_FILE = "tictactoe.txt";
	ArrayList state, weight;

	public AI (TypeOfAI type, string[,]board, int x, int y, int length)
	{
		AIType = type;
		TicTacToeBoard = board;
		WIN_LENGTH = length;
		Board_Size_X = x;
		Board_Size_Y = y;
	}
	
	void InitForLearning()
	{
		state = new ArrayList();
		weight = new ArrayList ();
	}
	public string ConvertBoard2Line(string[,] board, int Xmax, int Ymax)
	{
		string line = "";
		for (int i = 0; i < Xmax; i++)
						for (int j = 0; j < Ymax; j++) {
								line += board [i, j];
						}
		return line;
	}
	void FileReaderWriter ()
	{


		string line;
		if (!File.Exists (INPUT_FILE)) 
		{
			Debug.Log ("Input file doesn't exist. Creating one.");
			(File.CreateText(INPUT_FILE)).Close();
		}
		StreamReader reader = File.OpenText (INPUT_FILE);
		while ((line = reader.ReadLine()) != null)
		{
			string[] items = line.Split ('|');
			state.Add (items [0]);
			weight.Add(float.Parse (items [1]));
		}
		reader.Close ();
		}
	public int[] MoveAlgorythmed1()
	{
		ArrayList empty_tiles = new ArrayList ();
		for (int i = 0; i < Board_Size_X; i++)
			for (int j = 0; j < Board_Size_Y; j++)
			if (TicTacToeBoard[i,j] == " ") {
				empty_tiles.Add(new int[]{Goodness1(i,j),i,j});
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
	private int Goodness1 (int I, int J)
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
					if (TicTacToeBoard[i,j] == INPUT) {DIRECTION_SUM [m+1, n+1] += 10;}
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

	private bool isInRange(int I, int J)
	{
		if (I >= 0 && J >= 0 && I < Board_Size_X && J < Board_Size_Y)
			return true;
		else
			return false;
	}

	public int[] MoveRandom ()
	{
	//string TicTacToe_line;
	//TicTacToe_line = AI.TicTacToe_board2line (TicTacToeBoard, Board_Size_X, Board_Size_Y);
		ArrayList empty_tiles = new ArrayList ();
		for (int i = 0; i < Board_Size_X; i++)
		for (int j = 0; j < Board_Size_Y; j++)
		if (TicTacToeBoard[i,j] == " ") {
				empty_tiles.Add(new int[]{i,j});
		}
		if (empty_tiles.Count > 0) {
						int randomnumber = Random.Range (0, empty_tiles.Count);
						return (int[]) empty_tiles[randomnumber];
				} else
						return new int[]{-1,-1};
	}
}