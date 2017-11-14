using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Menu : MonoBehaviour 
{
	public static bool paused;
	public GameObject pausePanel;
	public GameObject camera;
	public GameObject player;
	private Inventory inventory;

	// Use this for initialization
	void Start () 
	{
		Cursor.lockState = CursorLockMode.Confined;
	}
	
	// Update is called once per frame
	void Update () 
	{
		updateMenu ();
		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			paused = !paused;
			if (paused) 
			{
				GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController> ().enabled = false;
				Time.timeScale = 0;
			} 
		}

		if (!paused) 
		{
			Time.timeScale = 1;
			GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController> ().enabled = true;
		}
			
	}

	void updateMenu()
	{
		if (paused) 
		{
			pausePanel.SetActive (true);
			//Cursor.visible = true;
		} 
		else if (!paused) 
		{
			pausePanel.SetActive (false);
			//Cursor.visible = false;
		}
	}
		
}
