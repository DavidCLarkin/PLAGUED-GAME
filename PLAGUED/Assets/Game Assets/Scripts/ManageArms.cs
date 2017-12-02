using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine;

/*
 * Class to deal with animations and when to use certain animations by setting the Animator's booleans 
 */
public class ManageArms : MonoBehaviour 
{

	private Animator anim;
	private bool shooting;
	private bool hasRangedWeapon;
	private bool hasAxe;
	private bool aimUMP;
	private bool aiming;
	private bool attackingAxe;
	private bool reloading;
	private float timeStamp;
	private float reloadTime;

	private PlayerManager playerM;
	private GameObject M4A1;
	private GameObject AXE;
	private GameObject UMP45;
	private Camera camera;

	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator>();
		playerM = GetComponentInParent<PlayerManager>();
		camera = GetComponentInParent<Camera>();
		M4A1 = playerM.M4A1_GUN;
		AXE = playerM.AXE;
		UMP45 = playerM.UMP45;
	}
	
	// Update is called once per frame
	void Update () 
	{
		anim.SetBool ("shooting", shooting);
		anim.SetBool ("hasRangedWeapon", hasRangedWeapon);
		anim.SetBool ("aiming", aiming);
		anim.SetBool ("aimUMP", aimUMP);
		anim.SetBool ("attackingAxe", attackingAxe);
		anim.SetBool ("hasAxe", hasAxe);
		anim.SetBool ("reloading", reloading);

		//Check weapons active
		if (M4A1.activeSelf || UMP45.activeSelf)
			hasRangedWeapon = true;
		else
			hasRangedWeapon = false;
		
		if (AXE.activeSelf)
			hasAxe = true;
		else
			hasAxe = false;

		manageAnimations ();
				
	}

	void manageAnimations()
	{
		if (aiming || aimUMP)
			camera.fieldOfView = Mathf.Lerp(60.0f, 30.0f, 0.5f);
		else
			camera.fieldOfView = 60.0f;
			
		if (Time.time >= reloadTime)
			reloading = false;

		if (Time.time >= timeStamp)
			attackingAxe = false;
		
		//INPUTS
		if (!Inventory.showInventory)
		{
			foreach (GameObject gun in playerM.weapons) 
			{
				if(gun.activeSelf) //gun needs to be active or else don't test
				{	
					//Reload only when ammo in clip is less than max clip of a mag 
					if (Input.GetKeyDown (KeyCode.R) && hasRangedWeapon && Time.time >= reloadTime
					&& gun.GetComponent<ShootGun> ().clip < gun.GetComponent<ShootGun> ().maxClip && !Inventory.showInventory && gun.GetComponent<ShootGun>().ammo > 0) 
					{
						reloading = true;
						reloadTime = Time.time + 2.4f;
					}

						if (Input.GetMouseButton (0) && hasRangedWeapon && gun.GetComponent<ShootGun> ().reloadTime <= 0 && gun.GetComponent<ShootGun> ().clip > 0)
							shooting = true;
						else
							shooting = false;

					
						if (Input.GetMouseButton (1) && (M4A1.activeSelf))
							aiming = true;
						else
							aiming = false;


						if (Input.GetMouseButton (1) && (UMP45.activeSelf))
							aimUMP = true;
						else
							aimUMP = false;
				}

			}
				
			if (Input.GetMouseButtonUp (0) && hasAxe && Time.time >= timeStamp) 
			{
				attackingAxe = true;
				timeStamp = Time.time + 1f;
			}
		}
	}
		
}
