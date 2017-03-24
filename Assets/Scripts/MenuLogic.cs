using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuLogic : MonoBehaviour {

	public Text LevelText;
	public Text ScoreText;
	public Text HighScore;
	public Text Congrats;
	public static int score;

	// Use this for initialization
	void Start () {
		switch(SceneManager.GetActiveScene().name)
		{
		case "MainMenu":
			HighScore.text = PlayerPrefs.GetInt("HighScore").ToString();
			break;
			case "GameOver":
			ScoreText.text = score.ToString();
			if(score == PlayerPrefs.GetInt("HighScore"))
			{
				Congrats.enabled = true;
			}
			else Congrats.enabled = false;
			
			break;
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Restart()
	{
		GameLogic.CurrentLevel = 0;
		GameLogic.score = 0;
		SceneManager.LoadScene("GameScreen");
	}

	public void NewGame()
	{
		SceneManager.LoadScene("GameScreen");
	}

	public void MainMenu()
	{
		GameLogic.score = 0;
		SceneManager.LoadScene("MainMenu");
	}

	public void ChangedValue(float val)
	{
		GameLogic.CurrentLevel = (int)val;
		LevelText.text = (val+1).ToString();
	}

	public void ResetHS()
	{
		PlayerPrefs.SetInt("HighScore",0);
		HighScore.text = "0";
	}
}
