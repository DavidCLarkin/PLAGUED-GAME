using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour 
{
	private Animator anim;
	private bool attacking;
	private float timeStamp;
	// Use this for initialization
	void Start () 
	{
		//anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//anim.SetBool ("Attacking", attacking);

		if (Input.GetMouseButtonDown (0) && Time.time >= timeStamp) 
		{
			attacking = true;
			timeStamp = Time.time + 1f;
		}

		if (Time.time >= timeStamp)
			attacking = false;
			
	}
}
