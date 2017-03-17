using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour {

	public int Width = 10;
	public int Height = 20;

	// Use this for initialization
	void Start () {

		Spawn();
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public bool IsInTheGrid(Vector2 pos)
	{
		return ((int)pos.x >= 0 &&  (int)pos.x < Width && (int)pos.y >= 0);
	}

	public Vector2 Round (Vector2 pos)
	{
		return new Vector2(Mathf.Round(pos.x),Mathf.Round(pos.y));
	}

	public void Spawn()
	{
		GameObject nextFigure = (GameObject)Instantiate(Resources.Load(ChooseFigure(),typeof(GameObject)), new Vector2(5f,16f),Quaternion.identity);
	}

	string ChooseFigure()
	{
		int rand = Random.Range(0,7);
		string FigName = "Prefabs/J_tetrimino";
		switch(rand)
		{
		case 1: FigName = "Prefabs/J_tetrimino";
			break;
		case 2: FigName = "Prefabs/L_tetrimino";
			break;
		case 3: FigName = "Prefabs/Line_tetrimino";
			break;
		case 4: FigName = "Prefabs/S_tetrimino";
			break;
		case 5: FigName = "Prefabs/Square_tetrimino";
			break;
		case 6: FigName = "Prefabs/T_tetrimino";
			break;
		case 7: FigName = "Prefabs/Z_tetrimino";
			break;

		}
		return FigName;
	}
}
