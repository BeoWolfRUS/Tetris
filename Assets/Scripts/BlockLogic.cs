using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockLogic : MonoBehaviour {

	float fall =0;
	public float fallSpeed = 1;
	public bool AllowRotation = true;
	public bool LimitRotation = false;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		CheckForInput();
	}

	void CheckForInput()
	{

		if(Input.GetKeyDown(KeyCode.RightArrow))
		{
			transform.position += new Vector3(1,0,0);

			if(ValidPos())
			{
				
			}
			else transform.position += new Vector3(-1,0,0);
		}

		else if(Input.GetKeyDown(KeyCode.LeftArrow))
		{
			transform.position += new Vector3(-1,0,0);

			if(ValidPos())
			{

			}
			else transform.position += new Vector3(1,0,0);
		}

		else if(Input.GetKeyDown(KeyCode.UpArrow))
		{
			if(AllowRotation)
			{
				if(LimitRotation)
				{
					if(transform.rotation.eulerAngles.z >=90)
					{
						transform.Rotate(0,0,-90);
					}
					else
					{
						transform.Rotate(0,0,90);
					}
				}
				else
				{
					transform.Rotate (0,0,90);
				}

					if(ValidPos())
					{

					}
					else
					{
					if(LimitRotation)
					{
						if(transform.rotation.eulerAngles.z >= 90)
						{
							transform.Rotate (0,0,-90);
						}
					else transform.Rotate(0,0,90);
					}
					else
					{
						transform.Rotate(0,0,-90);
					}
				}

			}
		}

		else if((Input.GetKeyDown(KeyCode.DownArrow)) || (Time.time - fall >= fallSpeed))
		{
			transform.position += new Vector3(0,-1,0);

			if(ValidPos())
			{

			}
			else 
			{
				transform.position += new Vector3(0,1,0);
				enabled = false;
				FindObjectOfType<GameLogic>().Spawn();

			}
			fall = Time.time;
		}
	}

	bool ValidPos()
	{
		foreach(Transform block in transform )
		{
			Vector2 pos = FindObjectOfType<GameLogic>().Round (block.position);

			if(FindObjectOfType<GameLogic>().IsInTheGrid(pos) == false)
			{
				return false;
			}
		}
		return true;
	}
}
