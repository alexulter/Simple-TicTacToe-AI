using UnityEngine;
using System.Collections;

public class AI_Random : AI {

	public AI_Random (string[,]board, int length, bool clean) : base(board, length, clean) 
	{}
	override public int[] Move ()
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
