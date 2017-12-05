using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class to handle input for menus (pause menu)
 */
public class UIHandler : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{
	}

	void Update () 
	{
		if (Input.GetKey (KeyCode.Escape) || Input.GetKey (KeyCode.I) || Input.GetKey(KeyCode.E)) 
		{
			if (Inventory.showInventory || Menu.paused || GameObject.Find("FPSController").GetComponent<Detection>().QuestPanel.activeSelf)
				Cursor.visible = true;
			else 
				Cursor.visible = false;
		} 
	}
}
