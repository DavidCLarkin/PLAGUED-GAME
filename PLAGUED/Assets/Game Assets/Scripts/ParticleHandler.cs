using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHandler : MonoBehaviour 
{

	private ParticleSystem particles;
	private GameObject player;

	// Use this for initialization
	void Start () 
	{
		particles = GetComponent<ParticleSystem>();
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () 
	{
		InvokeRepeating ("emitParticles", 0f, 5f);
	}

	void emitParticles()
	{
		if (Vector3.Distance (player.transform.position, gameObject.transform.position) <= 15f) 
		{
			particles.Play ();
			particles.Emit (1);
		}
		else
			particles.Stop();
	}
}
