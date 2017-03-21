using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour {

	public static int Width = 10;
	public static int Height = 20;
	public static Transform[,] Field = new Transform[Width, Height];

	public int score1 = 100;
	public int score2 = 250;
	public int score3 = 550;
	public int score4 = 1000;

	public int score = 0;

	public Text scoreText;

	public int HowMuchRowsDeleted = 0;


	// Use this for initialization
	void Start () {

		Spawn();
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		UpdateScore(HowMuchRowsDeleted);

	}

	public void UpdateScore(int rows)
	{
		switch(rows)
		{
		case 1:
			score +=score1;
			break;
			
		case 2:
			score +=score2;
			break;
			
		case 3:
			score +=score3;
			break;
			
		case 4:
			score +=score4;
			break;
		}
		scoreText.text = score.ToString();
		HowMuchRowsDeleted = 0;
	}

	public bool AboveField(BlockLogic figure)
	{
		for(int x =0;x<Width;x++)
		{
			foreach(Transform block in figure.transform)
			{
				Vector2 pos = Round(block.position);
				if(pos.y > Height -1)
				{
					return true;
				}
			}
		}
		return false;
	}

	public bool FullRow(int y)
	{
		for(int x = 0; x<Width;x++)
		{
			if(Field[x,y] == null)
			{
					return false;		
			}


		}
		HowMuchRowsDeleted++;
		return true;
	}

	public void ClearBlock(int y)
	{
		for(int x = 0; x<Width;x++)
		{
			Destroy(Field[x,y].gameObject);
			Field[x,y] = null;
		}
	}

	public void MoveRowDown(int y)
	{
		for(int x = 0; x<Width;x++)
		{
			if(Field[x,y] !=null)
			{
				Field[x,y-1] = Field[x,y];
				Field[x,y] = null;
				Field[x,y-1].position += new Vector3(0,-1,0);
			}
		}
	}

	public void MoveEverythingDown(int y)
	{
		for(int i =y;i<Height;i++)
		{
			MoveRowDown(i);
		}
	}

	public void DeleteRow()
	{
		for(int y=0;y<Height;y++)
		{
			if(FullRow(y))
			{
				ClearBlock(y);
				MoveEverythingDown(y+1);
				y--;
			}
		}
	}

	public void UpdateField(BlockLogic block)
	{
		for(int y=0;y<Height;y++)
		{
			for (int x=0;x<Width;x++)
			{
				if(Field[x,y] != null)
				{
					if(Field[x,y].parent == block.transform)
					{
						Field[x,y] = null;
					}
				}
			}
		}
		foreach(Transform bl in block.transform)
		{
			Vector2 pos = Round(bl.position);
			{
				if(pos.y<Height)
				{
					Field[(int)pos.x,(int)pos.y] = bl;
				}
			}
		}
	}

	public Transform GetFigureInPostion(Vector2 pos)
	{
		if(pos.y > Height-1 )
		{
			return null;
		}
		else
		{
			return Field[(int)pos.x,(int)pos.y];
		}
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
		GameObject nextFigure = (GameObject)Instantiate(Resources.Load(ChooseFigure(),typeof(GameObject)), new Vector2(5f,20f),Quaternion.identity);
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

	public void GameOver()
	{
		Application.LoadLevel("GameOver");
	}
}
