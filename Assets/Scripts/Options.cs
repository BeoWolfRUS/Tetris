using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour {

	public Text t_BGM;
	public Text t_SFX;
	public Text t_Ghost;

	// Use this for initialization
	void Start () 
	{
		if(PlayerPrefs.GetInt("BGM") == 1)
		{
			t_BGM.text = "ON";
		}
		else t_BGM.text = "OFF";

		if(PlayerPrefs.GetInt("SFX") == 1)
		{
			t_SFX.text = "ON";
		}
		else t_SFX.text = "OFF";

		if(PlayerPrefs.GetInt("GST")==1)
		{
			t_Ghost.text = "ON";
		}
		else t_Ghost.text = "OFF";

	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void ChangeBGM()
	{
		if(t_BGM.text == "ON")
		{
			t_BGM.text = "OFF";
			PlayerPrefs.SetInt("BGM",0);
		}
		else
		{
			t_BGM.text = "ON";
			PlayerPrefs.SetInt("BGM",1);
		}
	}

	public void ChangeSFX()
	{
		if(t_SFX.text == "ON")
		{
			PlayerPrefs.SetInt("SFX",0);
			t_SFX.text = "OFF";
		}
		else
		{
			PlayerPrefs.SetInt("SFX",1);
			t_SFX.text = "ON";
		}
	}

	public void ChangeGhost()
	{
		if(t_Ghost.text == "ON")
		{
			PlayerPrefs.SetInt("GST",0);
			t_Ghost.text = "OFF";
		}
		else
		{
			PlayerPrefs.SetInt("GST",1);
			t_Ghost.text = "ON";
		}
	}
}