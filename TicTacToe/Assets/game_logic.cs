using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class game_logic : MonoBehaviour {

	private static float SCR_HEIGHT = Screen.height, SCR_WIDTH = Screen.width;
	public int Board_Size_X = 3, Board_Size_Y = 3, WIN_LENGTH = 3;
	private static float BUTTON_SIZE = 50;
	private string[,] TicTacToeBoard;
	private bool turnAI = false;





	// Use this for initialization
	void Start () 
	{
		TicTacToeBoard = new string[Board_Size_X, Board_Size_Y];
		GenerateNewBoard ();
	}
	
	// Update is called once per frame
	void Update () {
		if (turnAI) MoveAI ();
	}


	void OnGUI () {
		//Draw buttons and catch clicks
		SizeButtonsToScreen ();
		if (GUI.Button (new Rect (SCR_WIDTH / 2 - BUTTON_SIZE * 3 / 2, 0, BUTTON_SIZE * 3, BUTTON_SIZE), "New Game"))
						GenerateNewBoard ();
		for (int i = 0; i < Board_Size_X; i++)
		for (int j = 0; j < Board_Size_Y; j++) {
			if(GUI.Button (new Rect (SCR_WIDTH/2 - BUTTON_SIZE*Board_Size_X/2 + i*BUTTON_SIZE, BUTTON_SIZE*(j+1), 
			                         BUTTON_SIZE, BUTTON_SIZE), TicTacToeBoard [i, j]) && !turnAI 
			   && TicTacToeBoard [i, j] == " ")
			{
				TicTacToeBoard [i, j] = "X";
				if (CheckWin(i,j)) GenerateNewBoard ();
				else turnAI = true;
			}
		}

	}

	private void GenerateNewBoard()
	{
		for (int i = 0; i < Board_Size_X; i++)
			for (int j = 0; j < Board_Size_Y; j++)
				TicTacToeBoard [i, j] = " ";
	}

	private void SizeButtonsToScreen()
	{
		SCR_HEIGHT = Screen.height;
		SCR_WIDTH = Screen.width;
		BUTTON_SIZE = Mathf.Min (SCR_WIDTH / Board_Size_X, SCR_HEIGHT / (Board_Size_Y+1));
	}
	private void MoveAI()
	{
		//string TicTacToe_line;
		//TicTacToe_line = AI.TicTacToe_board2line (TicTacToeBoard, Board_Size_X, Board_Size_Y);
		ArrayList empty_tiles = new ArrayList();
		for (int i = 0; i < Board_Size_X; i++)
			for (int j = 0; j < Board_Size_Y; j++)
			if (TicTacToeBoard[i,j] == " ") {
				empty_tiles.Add(new Vector2(i,j));
		}

		Vector2 move_tile;
		if (empty_tiles.Count > 0) {
			int randomnoober = Random.Range(0,empty_tiles.Count);
			move_tile = (Vector2)empty_tiles[randomnoober];
			turnAI = false;
			TicTacToeBoard[(int)move_tile[0], (int)move_tile[1]] = "O";
			if (CheckWin ((int)move_tile[0], (int)move_tile[1]))
				GenerateNewBoard();
				}


	}

	private bool CheckWin (int I, int J)
		{
		string INPUT = TicTacToeBoard [I, J];
		int i, j;
		int[,] STEP = {{0,0,0},{0,0,0},{0,0,0}};

		for (int m = -1; m <= 1; m++)
		for (int n = -1; n <= 1; n++)
		if (!(m == 0 && n == 0)) 
			{
				//Debug.Log(m.ToString()+" "+n.ToString()+" "+STEP [m+1,n+1].ToString());
				STEP [m+1, n+1] = -1;
				i = I;
				j = J;
				while ((isInRange(i,j))&&(TicTacToeBoard[i,j] == INPUT))
				{
					//Debug.Log(i.ToString()+" "+j.ToString()+" "+TicTacToeBoard[i,j]);
					STEP [m+1, n+1]++;
					i = i + m;
					j = j + n;
				}
				if ((STEP[m+1,n+1] + STEP[-m+1,-n+1] + 1) >= WIN_LENGTH) return true;
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
