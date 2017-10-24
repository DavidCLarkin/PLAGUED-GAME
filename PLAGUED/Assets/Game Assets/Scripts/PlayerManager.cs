using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

/*
 * Main class for the player object. Contains health, sound level, maximum health, all weapons, and inventory.
 * Allows the player to crouch and move slowly.
 * Deals with collisions with various items such as ammo and health packs
 */
public class PlayerManager : MonoBehaviour 
{
	private CharacterController characterController;
	private FirstPersonController fpsController;
	//private const float AGGRO_RESET_TIME = 10.0f;
	private const float DAMAGE_INTERVAL = 2.5f; //length of zombie attack
	private const float SHOT_RESET_LEVEL = 10.0f;
	private float dmgTimer;
	private float soundTimer;
	private float soundLvlResetTimer;
	private Scene currentScene;
	private Inventory inventory;

	public List<GameObject> weapons = new List<GameObject> ();
	public GameObject ARMS;
	public GameObject M4A1_GUN;
	public GameObject AXE;
	public GameObject UMP45;
	public float maxHealth;
	public float health;
	public float soundLevel;

	// Use this for initialization
	void Start () 
	{
		fpsController = gameObject.GetComponent<FirstPersonController>();
		characterController = gameObject.GetComponent<CharacterController>();
		inventory = GameObject.Find ("GameManager").GetComponent<Inventory>();

		currentScene = SceneManager.GetActiveScene ();
		Cursor.visible = false; //no mouse cursor

		//Weapons
		M4A1_GUN.SetActive (false); //Do not initially have weapon
		AXE.SetActive(false);
		UMP45.SetActive (false);

		health = maxHealth;
	}
	
	// Update is called once per frame
	void Update () 
	{	
		manageSoundLevels();
		crouch ();

		if(dmgTimer > 0)
			dmgTimer -= Time.deltaTime;
		if (soundTimer > 0)
			soundTimer -= Time.deltaTime;


		if (soundLvlResetTimer > 0)
			soundLvlResetTimer -= Time.deltaTime;
		
		if(health <= 0)
			die();

		if (health > maxHealth)
			health = maxHealth;
		
	}

	public void takeDamage(float damage)
	{
		health -= damage;
	}

	//Collecting consumables
	void OnCollisionEnter(Collision col) //Detecting one instance of a collision
	{
		if (col.gameObject.tag == "HealthBox") 
		{
			if (health == maxHealth) return; //if health full, dont do anything

			health += 50;
			Destroy (col.gameObject);
		}

		foreach (GameObject gun in weapons) 
		{
			if (gun.activeSelf && isRangedWeapon(gun)) 
			{
				if (col.gameObject.tag == "AmmoBox") 
				{
					gun.GetComponent<ShootGun> ().ammo += gun.GetComponent<ShootGun> ().clip;
					Destroy (col.gameObject);
				}
			}
		}
			
	}

	void OnTriggerEnter(Collider col)
	{
		print ("collidided");
		print (col.gameObject.tag);
		//activate whatever weapon you collide with + "Body" because of tags
		foreach(GameObject currentGun in weapons) 
		{
			print (currentGun.tag);
			if (col.gameObject.tag == currentGun.tag + "Body") 
			{
				foreach(GameObject otherGun in weapons) 
				{
					if (otherGun.activeSelf) 
					{
						otherGun.SetActive (false);
						inventory.addItem (otherGun.tag);
					}
				}
				currentGun.SetActive (true);
				inventory.removeItem (currentGun.tag);
			}
		}
		Destroy (col.gameObject);

	}
		
	void OnCollisionStay(Collision col) //Detect collisions every frame instead of once every collision
	{
		if (col.gameObject.tag == "Enemy" && dmgTimer <= 0) 
		{
			takeDamage (20.0f);
			dmgTimer = DAMAGE_INTERVAL;
		}
	}
		
	void crouch()
	{
		if (Input.GetKey (KeyCode.C) || Input.GetKey(KeyCode.LeftControl)) 
		{
			characterController.height = 1.0f;
			fpsController.m_WalkSpeed = 3;
			fpsController.m_RunSpeed = 3;
		}
		else 
		{
			characterController.height = 1.8f;
			fpsController.m_WalkSpeed = 5;
			fpsController.m_RunSpeed = 8;
		}
	}

	void die()
	{
		SceneManager.LoadScene ("DeadScreen");
	}

	void manageSoundLevels()
	{
		if (isShootingGun () && !Inventory.showInventory)  //as long as inventory is not active
		{
			soundLevel = 5;
			soundLvlResetTimer = soundTimer; //time before AI resets and doesnt follow anymore, same as sountTimer so it always drops aggro when sound resets
		} else if (isRunning () && soundLvlResetTimer <= 0)
			soundLevel = 3;
		else if (isWalking () && soundLvlResetTimer <= 0)
			soundLevel = 2;
		else if (isCrouchWalking () && soundLvlResetTimer <= 0)
			soundLevel = 1;
		else if(soundLvlResetTimer <= 0)
			soundLevel = 0;
	}

	public bool isWalking()
	{
		if (!(Input.GetKey(KeyCode.C) || Input.GetKey(KeyCode.LeftControl)) 
			&& (Input.GetKey (KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey (KeyCode.S) 
			|| Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D) 
			|| Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)))
			return true;
		return false;

	}
		
	public bool isCrouchWalking()
	{
		if ((Input.GetKey(KeyCode.C) || Input.GetKey(KeyCode.LeftControl) 
			&& (Input.GetKey (KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey (KeyCode.S) 
			|| Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D) 
			|| Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))))
			return true;
		return false;
	}

	public bool isStandingStill()
	{
		if (!(Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.S)
		    || Input.GetKey (KeyCode.C) || Input.GetKey (KeyCode.LeftControl) || Input.GetKey (KeyCode.W) && Input.GetKey (KeyCode.A)
		    || Input.GetKey (KeyCode.W) && Input.GetKey (KeyCode.D)
		    || Input.GetKey (KeyCode.S) && Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.S) && Input.GetKey (KeyCode.D)))
			return true;
		return false;
	}

	public bool isRunning()
	{
		if (Input.GetKey (KeyCode.LeftShift) && isWalking ())
			return true;
		return false;
	}

	// Tests if the player is shooting, only when inventory is not open
	public bool isShootingGun()
	{
		if (soundTimer > 0)
			return true;
		for(int i = 0; i < weapons.Count; i++) 
		{
			if (isRangedWeapon (weapons [i])) 
			{ //if its a ranged weapon
				if ((Input.GetMouseButton (0) || Input.GetMouseButtonDown (0)) && weapons [i].activeSelf && weapons [i].GetComponent<ShootGun> ().reloadTime <= 0 && !Inventory.showInventory && !Menu.paused) 
				{
					soundTimer = SHOT_RESET_LEVEL;
					return true;
				}
			}
		}
		return false;
	}

	public bool isRangedWeapon(GameObject weapon)
	{
		if (weapon.tag == "Axe")
			return false;
		return true;
	}
		
}
