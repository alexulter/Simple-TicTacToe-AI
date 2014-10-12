using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class game_logic : MonoBehaviour {

	private static float SCR_HEIGHT = Screen.height, SCR_WIDTH = Screen.width;
	public int SetBoard_Size_X = 3, SetBoard_Size_Y = 3, SetWIN_LENGTH = 3;
	private int Board_Size_X, Board_Size_Y, WIN_LENGTH;
	private static float BUTTON_SIZE = 50;
	private string[,] TicTacToeBoard;
	public bool isOnTorus = false;
	
	private int FreeTiles;
	public bool isAIfirst = false;
	public bool AIClean = false;
	private Player CurrentPlayer;
	private Player player1;
	private Player player2;
	public PlayerType Player1type;
	public PlayerType Player2type;
	


	// Use this for initialization
	void Start () 
	{
		Board_Size_X = SetBoard_Size_X; Board_Size_Y = SetBoard_Size_Y; WIN_LENGTH = SetWIN_LENGTH;
		//turnAI = isAIfirst;
		TicTacToeBoard = new string[Board_Size_X, Board_Size_Y];
		MakePlayers();
		GenerateNewBoard ();
		CurrentPlayer = player1;
		Debug.Log(CurrentPlayer.Symbol);
	}
	
	void MakePlayers()
	{
		GameObject go1 = new GameObject("Player1");
		GameObject go2 = new GameObject("Player2");
		player1 = go1.AddComponent<Player>();
		player2 = go2.AddComponent<Player>();
		player1.type = Player1type;
		player2.type = Player2type;
		player1.Symbol = "X";
		player2.Symbol = "O";
	}
	
	// Update is called once per frame
	void Update () {
		if (FreeTiles <= 0) 
		{
			//AISystem.FinishAI(-1);
			Debug.Log("DRAW");
			GenerateNewBoard ();
		}
		//if (turnAI && FreeTiles > 0) MoveAI ();
	}


	void OnGUI () {
		//Draw buttons and catch clicks
		SizeButtonsToScreen ();
		if (GUI.Button (new Rect (SCR_WIDTH / 2 - BUTTON_SIZE * 3 / 2, 0, BUTTON_SIZE * 3, BUTTON_SIZE), "New Game")) 
						{
						//AISystem.FinishAI(0);
						GenerateNewBoard ();
						}
		for (int i = 0; i < Board_Size_X; i++)
		for (int j = 0; j < Board_Size_Y; j++) {
			if (GUI.Button (new Rect (SCR_WIDTH/2 - BUTTON_SIZE*Board_Size_X/2 + i*BUTTON_SIZE, BUTTON_SIZE*(j+1), 
				BUTTON_SIZE, BUTTON_SIZE), TicTacToeBoard [i, j])) 
			    	OnPressTile(i,j);
		}

	}
	
	void OnPressTile (int i, int j)
	{
		if (CurrentPlayer.isHuman && TicTacToeBoard [i, j] == " ")
		{
			TicTacToeBoard [i, j] = CurrentPlayer.Symbol;
			if (CheckWin(i,j)) 
			{
				//AISystem.FinishAI(-1);
				if (CurrentPlayer == player1) Debug.Log("PLAYER 1 WON");
				else Debug.Log("PLAYER 2 WON");
				GenerateNewBoard ();
			}
			else NextPlayer();
			FreeTiles--;
		}
	}
	
	void NextPlayer()
	{
		if (CurrentPlayer == player1) CurrentPlayer = player2;
		else if (CurrentPlayer == player2) CurrentPlayer = player1;
		else Debug.LogError("Error: Current player has wrong instance");
	}
	
	private void GenerateNewBoard()
	{
		if (TicTacToeBoard == null) TicTacToeBoard = new string[Board_Size_X, Board_Size_Y];
		//turnAI = isAIfirst;
		for (int i = 0; i < Board_Size_X; i++)
			for (int j = 0; j < Board_Size_Y; j++)
				TicTacToeBoard [i, j] = " ";
		FreeTiles = Board_Size_X * Board_Size_Y;
		CurrentPlayer = player1;
	}

	private void SizeButtonsToScreen()
	{
		SCR_HEIGHT = Screen.height;
		SCR_WIDTH = Screen.width;
		BUTTON_SIZE = Mathf.Min (SCR_WIDTH / Board_Size_X, SCR_HEIGHT / (Board_Size_Y+1));
	}


	private bool CheckWin (int I, int J)
	{
		string INPUT = TicTacToeBoard [I, J];
		int i, j;
		int[,] DIRECTION_SUM = {{0,0,0},{0,0,0},{0,0,0}};

		for (int m = -1; m <= 1; m++)
		for (int n = -1; n <= 1; n++)
		if (!(m == 0 && n == 0)) 
			{
				//Initialize DIRECTION_SUM for chosen direction(m,n)
				DIRECTION_SUM [m+1, n+1] = -1;
				i = I;
				j = J;
				//keep going in direction(m,n) until 
				while ((isInRange(i,j))&&(TicTacToeBoard[i,j] == INPUT))
				{
					//Debug.Log(i.ToString()+" "+j.ToString()+" "+TicTacToeBoard[i,j]);
					DIRECTION_SUM [m+1, n+1]++;
					i = i + m;
					j = j + n;
					//if Torus, then fix the coordinates to the correct ones, placing them in range
					if (isOnTorus)
					{
						if (i == -1) i = Board_Size_X - 1;
						else if (i == Board_Size_X) i = 0;
						if (j == -1) j = Board_Size_Y - 1;
						else if (j == Board_Size_Y) j = 0;
						if ((i == I)&&(j == J)) break;
					}
				}
				if ((DIRECTION_SUM[m+1,n+1] + DIRECTION_SUM[-m+1,-n+1] + 1) >= WIN_LENGTH) return true;
			}
		return false;
	}

	private bool isInRange(int I, int J)
	{
		if (I >= 0 && J >= 0 && I < Board_Size_X && J < Board_Size_Y)
						return true;
				else
						return false;
	}

	public string TicTacToe_board2line(string[,] board, int Xmax, int Ymax)
	{
		string line = "";
		for (int i = 0; i < Xmax; i++)
		for (int j = 0; j < Ymax; j++) {
			line += board [i, j];
		}
		return line;
	}

}
