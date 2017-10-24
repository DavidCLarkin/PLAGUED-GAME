using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Class that is attached to every gun that shoots bullets.
 * Also handles the No ammo UI element if the gun is out of ammo
 * Instantiates bullets at a set prefab on the barrel of the gun
 */

public class ShootGun : MonoBehaviour 
{

	public GameObject bullet;
	public GameObject bulletEmitter;
	public AudioSource emptyMag;
	public Text noAmmoText;

	public int ammo; //overall starting ammo of a gun
	public int clip;// clip size of the gun
	public int maxClip; //identifier of clip size to add when reloading
	public float reloadTime;
	private float timeStamp;

	// Use this for initialization
	void Start () 
	{
		reloadTime = 0;
		noAmmoText.GetComponent<Text> ().enabled = false; //initially false
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (ammo < 0)
			ammo = 0;
		
		setAmmoTextNotif(); //Keep updating whether the reload prompt needs to be active

		if (reloadTime >= 0)
			reloadTime -= Time.deltaTime;
		

		if (clip <= 0)
			noAmmoText.text = "No Ammo, Press R to reload";
	
		//Only reload/shoot if not paused or inventory opened
		if (!Inventory.showInventory && !Menu.paused) 
		{
			if (Input.GetMouseButton (0) && Time.time >= timeStamp && reloadTime <= 0) 
			{
				shoot ();	
			}

			//Reload function
			if (Input.GetKeyDown (KeyCode.R) && reloadTime <= 0) 
			{
				reload ();
			}	
		}
	}

	void setAmmoTextNotif()
	{
		if (clip > 0)
			noAmmoText.GetComponent<Text> ().enabled = false;
		else if(clip <= 0)
			noAmmoText.GetComponent<Text>().enabled = true;
	}
		

	void reload()
	{
		if (ammo <= 0)	return;

		reloadTime += 2.5f;
		ammo += clip;
		if (ammo >= maxClip) //or 30
			clip = maxClip;
		else
			clip = ammo;
		ammo -= maxClip;
	}

	void shoot()
	{
		if (clip > 0) //If you have ammo left
		{
			//Instantiate bullet and take away 1 ammunition
			GameObject tempBullet; 
			tempBullet = Instantiate (bullet, bulletEmitter.transform.position, Quaternion.identity) as GameObject;
			clip -= 1;

			//Get the rigid body of the bullet
			Rigidbody tempRb;
			tempRb = tempBullet.GetComponent<Rigidbody> ();
			tempRb.AddForce (bulletEmitter.transform.forward * 2000);

			//Play the sound source on the gun (M4 shot)
			this.GetComponent<AudioSource>().Play();

			//Time between each shot
			timeStamp = Time.time + 0.13f;

			Destroy (tempBullet, 5);
		} 
		else if(clip <= 0) //Play the empty clip sound
			emptyMag.Play();
	}
}
