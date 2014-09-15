using UnityEngine;
using System.Collections;
using System.IO;

public class SavedData
{
	private int[] index;
	private int[] utility;
	private int state_length;
	public int GetUtility(string current_state)
	{
		int current_index = ConvertStateToIndex (current_state);
//		for (int i = 0; i < index.Length; i++)
//			if (index [i] == current_index) {
//				return utility [i];
//			}
//		return -1f;
		return utility [current_index];
	}

	public void ChangeUtility(string current_state, int change)
	{
		int current_index = ConvertStateToIndex (current_state);
		utility[current_index] += change;
	}
	public void SetUtility(string current_state, int value)
	{
		int current_index = ConvertStateToIndex (current_state);
		utility[current_index] = value;
	}
	

	public int ConvertStateToIndex(string state)
	{
		int index = 0;
		for (int i = 0; i < state.Length; i++) 
			index += TileToFloat(state[i]) * (int)Mathf.Pow(3f,(float)i);
		return index;
	}
	int TileToFloat(char tile)
	{
		if (tile == ' ') return 0;
			else if (tile == 'O') return 1;
			else if (tile == 'X') return 2;
			else {
			Debug.Log("ERROR");
				//ErrorFunc();
				return -1;
			}
	}

	public SavedData(int TicTacToe_state_length)
	{
		int size = (int)Mathf.Pow (3f, (float)TicTacToe_state_length);
		state_length = TicTacToe_state_length;
		//index = new int[size];
		utility = new int [size];
		for (int i = 0; i < size; i++) {
						//index [i] = i;
						utility [i] = 1;
				}
	}
	public void ReadFromFile()
	{
		int i = 0;
		using (StreamReader file = File.OpenText("data_"+state_length.ToString()))
		{
			while (file.Peek() >= 0 )
			{
				utility [i] = int.Parse(file.ReadLine());
//				split_line = file.ReadLine().Split(' ');
//				index[i] = int.Parse(split_line[0]);
//				utility[i] = float.Parse(split_line[1]);
				i++;
			}
		}
	}
		public void SaveToFile()
		{
			using (StreamWriter file = File.CreateText("data_"+state_length.ToString()))
			{
				for (int i = 0; i < utility.Length; i++)
				{
					file.WriteLine(utility[i].ToString());
				}
			}
		}
		


}