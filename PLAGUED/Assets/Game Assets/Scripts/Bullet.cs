using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class to instantiate a particle effect at the location, and destroy the bullet
 */
public class Bullet : MonoBehaviour 
{
	public float damage;
	public GameObject bloodPrefab;

	void OnCollisionEnter(Collision col)
	{ 
		if (col.gameObject.tag == "Enemy") 
		{ 
			ContactPoint contact = col.contacts [0]; 
			Quaternion rot = Quaternion.FromToRotation (Vector3.up, contact.normal); 
			Instantiate (bloodPrefab, contact.point, rot);  
		} 
		else
			Destroy (gameObject);
			
	}
}
