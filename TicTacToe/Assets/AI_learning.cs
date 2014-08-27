using UnityEngine;
using System.Collections;
using System.IO;

public class AI_learning
{

	int Board_Size_X, Board_Size_Y;
	string[,] TicTacToeBoard;

	private string INPUT_FILE = "tictactoe.txt";
	ArrayList state, weight;

	// Use this for initialization
	void Start () {
	

	
	}
	void Init()
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
	// Update is called once per frame
	void Update () {
	
	}
}
