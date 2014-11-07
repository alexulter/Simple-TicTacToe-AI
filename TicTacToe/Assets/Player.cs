using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public string Symbol = "?";
	AI AISystem;
	public PlayerType type;
	public string[,] Board;
	public int WinLength;
	public bool AIClean;
	
	void Awake()
	{
		//gameObject.SetActive(false);
	}
	void Start()
	{
		GenerateAI();
	}
	
	public void GenerateAI()
	{
		if (type == PlayerType.AIAlgorythm) AISystem = new AI_Algorythmed(Board, WinLength, AIClean);
		else if (type == PlayerType.AIWeights) 
		{
		AISystem = new AI_SimpleWeight(Board, WinLength, AIClean);
		((AI_SimpleWeight)AISystem).MYMARK = Symbol;
		}
		else if (type == PlayerType.AIRandom) AISystem = new AI_Random(Board, WinLength, AIClean);
	}
	
	public bool isHuman()
	{
		if (type == PlayerType.Human) return true;
		else return false;
	}
	
	public int[] MoveAI()
	{
		return AISystem.Move();
	}
	public void FinishAI(int i)
	{
		if (type == PlayerType.AIWeights) AISystem.FinishAI(i);
	}

}
