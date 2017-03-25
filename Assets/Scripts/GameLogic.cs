using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.ImageEffects;



public class GameLogic : MonoBehaviour {

	public static int Width = 10;
	public static int Height = 20;
	public static Transform[,] Field = new Transform[Width, Height];

	public int score1 = 100;
	public int score2 = 250;
	public int score3 = 550;
	public int score4 = 1000;

	public int highscore;

	public int LinesCleared;
	public static int CurrentLevel;

	int LinesTillNextLevel;

	public float FallSpeed = 1.0f;

	private AudioSource src;
	public AudioClip clear1;
	public AudioClip clear2;
	public AudioClip clear3;
	public AudioClip clear4;
	public AudioClip levelUp;

	public static bool Paused = false;

	public static int score = 0;

	public Text scoreText;
	public Text LevelText;
	public Text LinesText;

	public Canvas hud_score;
	public Canvas hud_pause;
	public Canvas hud_ext;

	public int HowMuchRowsDeleted = 0;

	private GameObject NextTetrimino;
	private GameObject SpawnTetrimno;
	private GameObject GhostTetrimino;

	private bool GameStarted = false;

	Vector2 NextPos = new Vector2(-6.5f,15);


	// Use this for initialization
	void Start () {
		
		
		LinesTillNextLevel = (CurrentLevel+1)*10;
		src = GetComponent<AudioSource>();

		if(PlayerPrefs.GetInt("BGM")==0)
		{
			src.GetComponent<AudioSource>().Stop();
		}
		else src.GetComponent<AudioSource>().Play();
		Spawn();
	}
	
	// Update is called once per frame
	void Update () 
	{
		CheckInput();
		UpdateScore(HowMuchRowsDeleted);
		UpdateLevel();
		UpdateSpeed();
	}

	public void SpawnGhost()
	{
		if(GameObject.FindGameObjectWithTag("Ghost") != null)
		{
			Destroy(GameObject.FindGameObjectWithTag("Ghost"));
		}
		GhostTetrimino = (GameObject)Instantiate(SpawnTetrimno, SpawnTetrimno.transform.position,Quaternion.identity);
		Destroy(GhostTetrimino.GetComponent<BlockLogic>());
		GhostTetrimino.AddComponent<Ghost>();
	}

	void CheckInput()
	{
		if(Input.GetKeyUp(KeyCode.Escape))
		{
			if(!Paused)
			{
				Time.timeScale= 0;
				src.Pause();
				hud_score.enabled = false;
				hud_pause.enabled = true;
				Paused = true;
				Camera.main.GetComponent<Blur>().enabled = true;
			}
			else
			{
				Time.timeScale = 1;
				src.UnPause();
				Paused = false;
				hud_pause.enabled = false;
				hud_ext.enabled = false;
				hud_score.enabled = true;
				Camera.main.GetComponent<Blur>().enabled = false;
			}
		}

	}

	void UpdateLevel()
	{
		if(LinesCleared>=LinesTillNextLevel)
		{
			CurrentLevel++;
			if(PlayerPrefs.GetInt("SFX")==1)
			{
				src.PlayOneShot(levelUp);
			}
			LinesTillNextLevel += (CurrentLevel+1)*10;
		}
		LevelText.text = (CurrentLevel+1).ToString();

	}

	public void UpdateSpeed()
	{
		FallSpeed = 1.0f - (float)CurrentLevel * 0.1f;
	}

	public void UpdateScore(int rows)
	{
		switch(rows)
		{
		case 1:
			
			if(PlayerPrefs.GetInt("GST")== 0)
			{
				score +=score1;
			}
			else score += score1/2;

			if(PlayerPrefs.GetInt("SFX")==1)
			{
				src.PlayOneShot(clear1);
			}
			break;
			
		case 2:
			if(PlayerPrefs.GetInt("GST")== 0)
			{
				score +=score2;
			}
			else score += score2/2;

			if(PlayerPrefs.GetInt("SFX")==1)
			{
				src.PlayOneShot(clear2);
			}
			break;
			
		case 3:
			if(PlayerPrefs.GetInt("GST")== 0)
			{
				score +=score3;
			}
			else score += score3/2;
			if(PlayerPrefs.GetInt("SFX")==1)
			{
				src.PlayOneShot(clear3);
			}
			break;
			
		case 4:
			if(PlayerPrefs.GetInt("GST")== 0)
			{
				score +=score4;
			}
			else score += score4/2;
			if(PlayerPrefs.GetInt("SFX")==1)
			{
				src.PlayOneShot(clear4);
			}
			break;
		}
		LinesCleared +=rows;
		LinesText.text = LinesCleared.ToString();
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
		if(!GameStarted)
		{
			GameStarted = true;
			SpawnTetrimno = (GameObject)Instantiate(Resources.Load(ChooseFigure(),typeof(GameObject)), new Vector2(5f,20f),Quaternion.identity);
			NextTetrimino = (GameObject)Instantiate(Resources.Load(ChooseFigure(),typeof(GameObject)), NextPos,Quaternion.identity);
			NextTetrimino.GetComponent<BlockLogic>().enabled = false;

			if(PlayerPrefs.GetInt("GST")==1)
			{
				SpawnGhost();
			}
		}
		else
		{
			NextTetrimino.transform.localPosition = new Vector2(5f,20f);
			SpawnTetrimno = NextTetrimino;
			SpawnTetrimno.GetComponent<BlockLogic>().enabled = true;
			SpawnTetrimno.tag = "current";

			if(PlayerPrefs.GetInt("GST")==1)
			{
				SpawnGhost();
			}

			NextTetrimino = (GameObject)Instantiate(Resources.Load(ChooseFigure(),typeof(GameObject)), NextPos,Quaternion.identity);
			NextTetrimino.GetComponent<BlockLogic>().enabled = false;


		}

	}

	string ChooseFigure()
	{
		int rand = Random.Range(0,7);
		string FigName = "Prefabs/J_tetrimino";
		switch(rand)
		{
		case 0: FigName = "Prefabs/J_tetrimino";
			break;
		case 1: FigName = "Prefabs/L_tetrimino";
			break;
		case 2: FigName = "Prefabs/Line_tetrimino";
			break;
		case 3: FigName = "Prefabs/S_tetrimino";
			break;
		case 4: FigName = "Prefabs/Square_tetrimino";
			break;
		case 5: FigName = "Prefabs/T_tetrimino";
			break;
		case 6: FigName = "Prefabs/Z_tetrimino";
			break;

		}
		return FigName;
	}

	public void GameOver()
	{
		if(score > PlayerPrefs.GetInt("HighScore"))
		{
			PlayerPrefs.SetInt("HighScore",score);
		}
		MenuLogic.score = score;
		SceneManager.LoadScene("GameOver");

	}
}
