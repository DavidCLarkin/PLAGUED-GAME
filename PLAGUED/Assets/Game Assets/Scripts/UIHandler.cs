using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class to handle input for menus (pause menu)
 */
public class UIHandler : MonoBehaviour 
{
	public GameObject questPanel;
	// Use this for initialization
	void Start () 
	{
	}

	void Update () 
	{
		if(Input.GetKey (KeyCode.Escape) || Input.GetKey (KeyCode.I) || Input.GetKey(KeyCode.E))
		{
			if (Inventory.showInventory || Menu.paused || questPanel.activeInHierarchy)
				Cursor.visible = true;
			else
				Cursor.visible = false;
		} 
	}
}
