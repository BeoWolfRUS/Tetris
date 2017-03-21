using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockLogic : MonoBehaviour {

	float fall =0;
	public float fallSpeed = 1;
	public bool AllowRotation = true;
	public bool LimitRotation = false;

	public AudioClip moveSound;
	public AudioClip clearRowSound;
	public AudioClip rotateSound;
	public AudioClip landSound;

	private AudioSource src;

	bool played = false;


	// Use this for initialization
	void Start () {
		src = GetComponent<AudioSource>();
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
				FindObjectOfType<GameLogic>().UpdateField(this);
				src.PlayOneShot(moveSound);


			}
			else transform.position += new Vector3(-1,0,0);
		}

		else if(Input.GetKeyDown(KeyCode.LeftArrow))
		{
			transform.position += new Vector3(-1,0,0);

			if(ValidPos())
			{
				FindObjectOfType<GameLogic>().UpdateField(this);
				src.PlayOneShot(moveSound);
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
						FindObjectOfType<GameLogic>().UpdateField(this);
					src.PlayOneShot(rotateSound);
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
			

		else if((Input.GetKey(KeyCode.DownArrow)) || (Time.time - fall >= fallSpeed))
		{
			transform.position += new Vector3(0,-1,0);

			if(ValidPos())
			{
				
				FindObjectOfType<GameLogic>().UpdateField(this);
				if(!played)

			}
			else 
			{
				transform.position += new Vector3(0,1,0);
				src.PlayOneShot(landSound);
				FindObjectOfType<GameLogic>().DeleteRow();
				if(FindObjectOfType<GameLogic>().AboveField(this))
				{
					FindObjectOfType<GameLogic>().GameOver();
				}
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
			if(FindObjectOfType<GameLogic>().GetFigureInPostion(pos) != null && FindObjectOfType<GameLogic>().GetFigureInPostion(pos).parent != transform)
			{
				return false;
			}
		}
		return true;
	}
}
