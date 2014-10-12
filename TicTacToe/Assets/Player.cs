using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public string Symbol = "?";
	AI AISystem;
	public PlayerType type;
	public bool isHuman;
	
	void Awake()
	{
		//gameObject.SetActive(false);
	}
	void Start()
	{
		isHuman = true;
//		if (type == PlayerType.AIAlgorythm) AISystem = new AI_Algorythmed(TicTacToeBoard, WIN_LENGTH, AIClean);
//		else if (type == PlayerType.AIWeights) AISystem = new AI_SimpleWeight(TicTacToeBoard, WIN_LENGTH, AIClean);
//		else if (type == PlayerType.AIRandom) AISystem = new AI_Random(TicTacToeBoard, WIN_LENGTH, AIClean);
	}
	
//	private void MoveAI()
//	{
//		int[] move_tile;
//		move_tile = AISystem.Move();
//		//if (move_tile [0] < 0) {Debug.Log("game_logic: ERROR with AI"); return;}
//		TicTacToeBoard[(int)move_tile[0], (int)move_tile[1]] = "O";
//		turnAI = false;
//		if (CheckWin ((int)move_tile[0], (int)move_tile[1]))
//		{
//			AISystem.FinishAI(1);
//			Debug.Log("COMPUTER WON");
//			GenerateNewBoard();
//		}
//		FreeTiles--;
//		
//		
//	}
}
