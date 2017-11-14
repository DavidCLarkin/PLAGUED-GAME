﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonFunction : MonoBehaviour 
{
	private PlayerManager playerM;
	private Inventory inventory;

	// Use this for initialization
	void Start () 
	{
		playerM = GameObject.Find ("FPSController").GetComponent<PlayerManager> ();
		inventory = GameObject.Find ("GameManager").GetComponent<Inventory>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void ExitGame()
	{
		Application.Quit();
	}

	public void Restart()
	{
		EnemyHandler.currentEnemiesInScene.Clear ();
		EnemyHandler.maximumEnemies = 5;
		SceneManager.LoadScene ("Level1", LoadSceneMode.Single);
	}

	/*
	 * Method that is called when a button on the inventory is pressed that determines which weapons to remove and add to the Inventory UI
	 */
	public void SetWeapons()
	{
		if (playerM.health <= 0)	return;

		Button button = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Button>();
		Image[] icons = button.gameObject.GetComponentsInChildren<Image> ();
		foreach(GameObject weapon in playerM.weapons)
		{
			for (int i = 0; i < icons.Length; i++) 
			{
				if (icons[i].sprite == null)
					continue;
				if (icons[i].sprite.ToString ().Contains (weapon.tag))  //if sprite name contains the weapons tag
				{
					foreach(GameObject otherWeapon in playerM.weapons) //get all other weapons and deactivate, to make sure you get all OTHER weapons, not the clicked one
					{
						otherWeapon.SetActive (false);
						if(otherWeapon.GetComponent<GunBoolean>().collected)
							inventory.addItem (otherWeapon.tag); //add this disabled weapon to inventory
					}
					weapon.SetActive (true); //add weapon clicked to active
					inventory.removeItem (weapon.tag); //remove the weapon from inventory
					Inventory.showInventory = false; //dont show inventory after making weapon active, better UX
				} 
			}
		}
			
	}

}
