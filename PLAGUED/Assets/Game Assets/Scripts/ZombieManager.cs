using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieManager : MonoBehaviour 
{
	private const float IN_ATTACK_RANGE = 1.9f;
	private const float FOLLOW_RANGE_STANDING = 10f;
	private const float FOLLOW_RANGE_CROUCHING = 4f;
	private const float TIME_TO_DESTROY_ZOMBIE = 6f;
	private const float DAMAGE_TIMER = 2.5f;
	private Animator anim;
	private bool attack;
	private bool dying;
	private bool walk;
	private bool run;
	private bool notMoving;
	private GameObject player;
	public int health;
	private float timer;
	private NavMeshAgent mNavMesh;
	private PlayerManager playerManager;

	// Use this for initialization
	void Start () 
	{
		health = 100;
		anim = GetComponent<Animator>();
		mNavMesh = this.GetComponent<NavMeshAgent> (); //quick access later
		player = GameObject.Find("FPSController"); //set player to target
		playerManager = player.GetComponent<PlayerManager>(); 
		//walk = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		destroyThis ();

		//Setting variables
		anim.SetBool("Attack", attack);
		anim.SetBool("Dying", dying);
		anim.SetBool ("Walk", walk);
		anim.SetBool ("Run", run);

		animationControl ();
		handleVariables ();

		if (!playerManager.isCrouchWalking () && !playerManager.isWalking ())
			notMoving = true;
		else
			notMoving = false;
		print (notMoving);
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Bullet") 
		{
			health -= 20;
			Destroy (col.gameObject);
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Axe")
			health -= 50;
	}
		
	void destroyThis()
	{
		if (timer < 0)
			Destroy (gameObject);
	}


	void animationControl()
	{
		if (!gameObject.GetComponent<ZombieAI> ().miniGameAI) 
		{
			if (Vector3.Distance (transform.position, player.transform.position) > 10) 
			{ //WAYPOINTS
				walk = true;
				run = false;
				//mNavMesh.enabled = true;
				//mNavMesh.speed = mNavMesh.speed / 3;
			}
			else if (playerManager.isWalking ()) 
			{
				//TEST IF SHOULD BE IDLE OR FOLLOWING
				if (Vector3.Distance (transform.position, player.transform.position) >= FOLLOW_RANGE_STANDING && !dying && playerManager.soundLevel <= 4) 
				{
					walk = true;
					run = false;
					//IF FAR AWAY, DONT MOVE
					//mNavMesh.enabled = true;
				} 
				else if (((Vector3.Distance (transform.position, player.transform.position) < FOLLOW_RANGE_STANDING) || (playerManager.soundLevel >= 5) && !dying) && !attack) 
				{
					walk = false;
					run = true;
					//mNavMesh.enabled = true;
				}
			} 
			else if (playerManager.isCrouchWalking ()) 
			{
				//TEST IF SHOULD BE IDLE OR FOLLOWING
				if (Vector3.Distance (transform.position, player.transform.position) >= FOLLOW_RANGE_CROUCHING && !dying && playerManager.soundLevel <= 4) 
				{
					walk = true;
					run = false;
					//mNavMesh.enabled = true;
				} 
				else if (((Vector3.Distance (transform.position, player.transform.position) < FOLLOW_RANGE_CROUCHING) || (playerManager.soundLevel >= 5) && !dying) && !attack) 
				{
					run = true;
					walk = false;
					//mNavMesh.enabled = true;
				}
			}

			if (playerManager.isShootingGun()) 
			{
				run = true;
				walk = false;
				//mNavMesh.enabled = true;
			}
				
		}
		else  //When miniGameAI, zombies just follow the player
		{
			run = true;
			//mNavMesh.enabled = true;
		}

		//ATTACKING IF CLOSE
		if(notMoving || playerManager.isWalking() || playerManager.isCrouchWalking())
		{
			if (Vector3.Distance (transform.position, player.transform.position) <= IN_ATTACK_RANGE && !dying) 
			{
				gameObject.GetComponent<Rigidbody> ().isKinematic = false;
				//run = false;
				walk = false;
				attack = true;
				mNavMesh.enabled = false;
			} 
			else if (Vector3.Distance (transform.position, player.transform.position) > IN_ATTACK_RANGE && !dying) 
			{
				gameObject.GetComponent<Rigidbody> ().isKinematic = true; //wont fall through floor
				attack = false;
				//run = true;
				mNavMesh.enabled = true;
			}
		}
	}

	void handleVariables()
	{
		if (run) 
			mNavMesh.speed = 2.5f;
		else if (walk)
			mNavMesh.speed = 0.8f;

		if (attack && playerManager.dmgTimer <= 0) 
		{
			playerManager.takeDamage (20.0f);
			playerManager.dmgTimer = DAMAGE_TIMER;
		}

		if (dying) //Makes sure the player can't take damage after its dead
			this.gameObject.tag = "Untagged";

		if(timer > 0)
			timer -= Time.deltaTime;


		//Dying animation
		if (health <= 0) 
		{
			if (mNavMesh.enabled && !dying) 
			{
				timer += TIME_TO_DESTROY_ZOMBIE;
				//this.GetComponent<AICharacterControl> ().enabled = false; //no null pointers to script
				//mNavMesh.enabled = false;
			}
			dying = true; //set to true after above so it doesnt keep adding to the timer
		}

		if (dying) 
		{
			attack = false;
			gameObject.GetComponent<Rigidbody> ().isKinematic = true; //wont fall through floor
			gameObject.GetComponent<CapsuleCollider> ().enabled = false;
			mNavMesh.enabled = false;
		}
	}
}
