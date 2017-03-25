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

	public Canvas hud_pause;
	public Canvas hud_ext;

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
		Time.timeScale = 1;
		GameLogic.Paused = false;
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
	public void Exit()
	{
		Application.Quit();
	}

	public void MenuFromPause()
	{
		hud_pause.enabled = false;
		hud_ext.enabled = true;
	}

	public void BackToPause()
	{
		hud_pause.enabled = true;
		hud_ext.enabled = false;
	}

	public void ToOptions()
	{
		SceneManager.LoadScene("Options");
	}
}
