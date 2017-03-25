using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		tag = "Ghost";
		foreach(Transform block in transform)
		{
			block.GetComponent<SpriteRenderer>().color = new Color(1f, 1f,1f, 0.2f);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		FollowFigure();
	}

	void FollowFigure()
	{
		Transform current = GameObject.FindGameObjectWithTag("current").transform;
		transform.position = current.position;
		transform.rotation = current.rotation;
		GoDown();
	}

	void GoDown()
	{
		while(IsValidPos())
		{
			transform.position += new Vector3(0,-1,0);
		}
		if(!IsValidPos())
		{
			transform.position += new Vector3(0,1,0);
		}
	}

	bool IsValidPos()
	{
		foreach(Transform block in transform)
		{
			Vector2 pos = FindObjectOfType<GameLogic>().Round(block.position);
			if(FindObjectOfType<GameLogic>().IsInTheGrid(pos) == false)
			{
				return false;
			}
			if(FindObjectOfType<GameLogic>().GetFigureInPostion(pos)!= null && FindObjectOfType<GameLogic>().GetFigureInPostion(pos).parent.tag == "current")
			{
				return true;
			}
			if(FindObjectOfType<GameLogic>().GetFigureInPostion(pos)!= null && FindObjectOfType<GameLogic>().GetFigureInPostion(pos).parent != transform)
			{
				return false;
			}
		}
		return true;
	}
}
