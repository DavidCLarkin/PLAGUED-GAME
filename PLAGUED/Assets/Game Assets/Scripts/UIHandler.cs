using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class to handle input for menus (pause menu, inventory)
 */
public class UIHandler : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{
	}

	void Update () 
	{
		if (Input.GetKey (KeyCode.Escape) || Input.GetKey (KeyCode.I)) 
		{
			if (Inventory.showInventory || Menu.paused)
				Cursor.visible = true;
			else 
				Cursor.visible = false;
		} 
	}
}
