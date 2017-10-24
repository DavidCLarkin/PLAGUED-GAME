using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Class to determine whether to show the ammo UI text, if using a ranged weapon
 */

public class HandleAmmoText : MonoBehaviour 
{
	public GameObject player;

	private Text ammoText;
	private PlayerManager playerM;

	// Use this for initialization
	void Start () 
	{
		playerM = player.GetComponent<PlayerManager>();
		ammoText = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		printText ();	
	}

	void printText()
	{
		foreach (GameObject gun in playerM.weapons) 
		{
			if (gun.activeSelf && playerM.isRangedWeapon (gun)) 
			{
				ammoText.text = "Ammo : " + gun.GetComponent<ShootGun> ().clip + "/" + gun.GetComponent<ShootGun> ().ammo;
				ammoText.enabled = true;
			}		
			if (gun.activeSelf && !playerM.isRangedWeapon (gun))
				ammoText.enabled = false;
		}
	}
}
