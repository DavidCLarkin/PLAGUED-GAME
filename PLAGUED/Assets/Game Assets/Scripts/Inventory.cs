using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

/*
 * Inventory class that deals with Showing and hiding the inventory UI
 * Has functionality to Add an item, Remove an item, check if the Inventory already has an item.
 */

public class Inventory : MonoBehaviour 
{
	public static bool showInventory;
	private bool placedObject;
	public GameObject inventory;
	public GameObject titleBlock;
	public Sprite[] icons;

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		manageInventory ();
		if (Input.GetKeyDown (KeyCode.I)) 
		{
			showInventory = !showInventory; 
		}
	}

	void manageInventory()
	{
		if (showInventory) 
		{
			inventory.SetActive (true);
			titleBlock.SetActive (true);
			Cursor.visible = true;
		} 
		else if (!showInventory) 
		{
			inventory.SetActive (false);
			titleBlock.SetActive (false);
			Cursor.visible = false;
		}
	}

	//Add item to the inventory
	public void addItem(string itemName)
	{
		Image[] allImages = inventory.GetComponentsInChildren<Image>();
		foreach (Image child in allImages) 
		{
			Image icon = child.gameObject.GetComponent<Image> ();
			if(icon.ToString().Contains("Icon"))
			{
				for(int i = 0; i < icons.Length; i++)
				{
					Sprite pic = icons[i]; //set sprite to i
					string imgName = pic.ToString ();
					if (imgName.Contains (itemName) && icon.sprite == null && !alreadyHasItem(itemName))  //if sprite contains the tag of weapon, dont have the weapon and not null, add it
					{
						icon.sprite = pic;
						return; //return so it only puts it into one spot
					}
				}
			}
		}
	}

	// returns true if item is already in inventory, else false
	bool alreadyHasItem(string itemName)
	{
		Image[] allImages = inventory.GetComponentsInChildren<Image> ();
		foreach (Image child in allImages) 
		{
			Image icon = child.gameObject.GetComponent<Image>();
			if (icon.sprite == null)
				continue;
			if (icon.sprite.ToString ().Contains (itemName)) // if the icon to string contains the tag of item
				return true;
		}
		return false;
	}

	public void removeItem(string itemName)
	{
		Image[] allImages = inventory.GetComponentsInChildren<Image> ();
		foreach (Image child in allImages) 
		{
			Image icon = child.gameObject.GetComponent<Image>();
			if (icon.sprite.ToString ().Contains (itemName)) 
			{
				icon.sprite = null;
			}
		}
	}
		
}
