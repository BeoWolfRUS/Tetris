using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuLogic : MonoBehaviour {

	public Text LevelText;

	// Use this for initialization
	void Start () {
		LevelText.text = "1";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Restart()
	{
		GameLogic.CurrentLevel = 0;
		Application.LoadLevel("GameScreen");
	}

	public void NewGame()
	{
		Application.LoadLevel("GameScreen");
	}

	public void ChangedValue(float val)
	{
		GameLogic.CurrentLevel = (int)val;
		LevelText.text = (val+1).ToString();
	}
}
