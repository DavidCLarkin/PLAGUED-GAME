using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class ZombieAI : MonoBehaviour 
{

	public NavMeshAgent agent { get; private set; } // the navmesh agent required for the path finding
	public ThirdPersonCharacter character { get; private set; } // the character we are controlling
	public bool miniGameAI = false;
	private Transform target; // target to aim for
	private PlayerManager playerManager;
	private GameObject[] waypoints;
	private int waypointID;


	// Use this for initialization
	private void Start()
	{
		target = GameObject.Find ("FPSController").transform;
		// get the components on the object we need ( should not be null due to require component so no need to check )
		agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
		character = GetComponent<ThirdPersonCharacter>();
		playerManager = target.GetComponent<PlayerManager>();

		agent.updateRotation = false;
		agent.updatePosition = true;
		waypoints = GameObject.FindGameObjectsWithTag ("Waypoint");
		waypointID = Random.Range (0, waypoints.Length);
	}


	// Update is called once per frame
	private void Update()
	{
		float dist = Vector3.Distance(target.position, character.GetComponent<Transform>().position);
		if (!miniGameAI) 
		{
			if (target != null || playerManager.soundLevel > 4) 
			{ //DISTANCE FROM PLAYER 			//&& dist <= 10f;
				//CHECKING IF PLAYER IS WALKING OR CROUCHING, AND CHANGING DISTANCES DEPENDING ON THESE VARIABLES
				if (agent.enabled && playerManager.isWalking ()) 
				{
					if (dist <= 10f)
					{
						agent.SetDestination (target.position); //no null pointers
						rotateTowards (target);
					}
				} 
				else if (agent.enabled && playerManager.isCrouchWalking ()) 
				{
					if (dist <= 4f) 
					{
						agent.SetDestination (target.position);
						rotateTowards (target);
					}
				} 
				else if (agent.enabled && playerManager.soundLevel > 4) 
				{
					agent.SetDestination (target.position);
					rotateTowards (target);
				}
				else if (agent.enabled && dist > 10) 
				{
					if (Vector3.Distance (this.transform.position, waypoints [waypointID].transform.position) >= 2) 
					{
						agent.SetDestination (waypoints [waypointID].transform.position); //move to another waypoint
						character.Move (agent.desiredVelocity, false, false);
					}
					if (Vector3.Distance (this.transform.position, waypoints [waypointID].transform.position) <= 2)
						waypointID = Random.Range (0, waypoints.Length); //if close, choose another waypoint
				}
				character.Move (agent.desiredVelocity, false, false); // move somewhere
			} 
			else 
			{
				// We still need to call the character's move function, but we send zeroed input as the move param.
				character.Move (Vector3.zero, false, false);	
			}
		} 
		else if(miniGameAI)
		{
			if (agent.enabled) 
			{
				agent.SetDestination (target.position);
			}

			character.Move (agent.desiredVelocity, false, false);
		}
	}

	//rotate the AI to the player
	private void rotateTowards(Transform target)
	{
		Vector3 direction = (target.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));    // flattens the vector3
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
	}

	public void SetTarget(Transform target)
	{
		this.target = target;
	}
}
