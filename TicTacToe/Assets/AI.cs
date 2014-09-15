using UnityEngine;
using System.Collections;
using System.IO;

public class AI
{

	protected int Board_Size_X, Board_Size_Y;
	protected int WIN_LENGTH;
	protected string[,] TicTacToeBoard;
	public TypeOfAI AIType;
	
	protected SavedData data;

	public AI (string[,]board, int length, bool toClean)
	{
		TicTacToeBoard = board;
		WIN_LENGTH = length;
		Board_Size_X = board.GetLength(0);
		Board_Size_Y = board.GetLength(1);
		data = new SavedData (Board_Size_X*Board_Size_Y);
		if (toClean == true) data.SaveToFile ();
		else data.ReadFromFile ();
		
	}
	
	virtual public int[] Move()
	{return null;}
	
	protected string ConvertBoard2Line(string[,] board)
	{
		string line = "";
		for (int i = 0; i < board.GetLength(0); i++)
						for (int j = 0; j < board.GetLength(1); j++) {
								line += board [i, j];
						}
		return line;
	}

	virtual public void FinishAI(int gameResult)
	{
		//changing and saving AI knowledge base after finishing a game
	}
//	void FileReaderWriter ()
//	{
//		string line;
//		if (!File.Exists (INPUT_FILE)) 
//		{
//			Debug.Log ("Input file doesn't exist. Creating one.");
//			(File.CreateText(INPUT_FILE)).Close();
//		}
//		StreamReader reader = File.OpenText (INPUT_FILE);
//		while ((line = reader.ReadLine()) != null)
//		{
//			string[] items = line.Split ('|');
//			state.Add (items [0]);
//			weight.Add(float.Parse (items [1]));
//		}
//		reader.Close ();
//		}


	
	protected bool isInRange(int I, int J)
	{
		if (I >= 0 && J >= 0 && I < Board_Size_X && J < Board_Size_Y)
			return true;
		else
			return false;
	}

	
}