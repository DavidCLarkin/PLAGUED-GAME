using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Simple class to determine whether the weapon has been collected within the game,
 * mainly for the inventory system to determine whether to add it to the inventory etc.
 */

public class GunBoolean : MonoBehaviour 
{
	public bool collected;
	// Use this for initialization
	void Start () 
	{
		collected = false;	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!collected)
			collected = true;
	}
}
