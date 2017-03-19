using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour {

	public static int Width = 10;
	public static int Height = 20;
	public static Transform[,] Field = new Transform[Width, Height];

	// Use this for initialization
	void Start () {

		Spawn();
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
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
