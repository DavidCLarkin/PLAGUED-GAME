using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectWeaponSpawner : MonoBehaviour 
{
	public GameObject[] objects;

	// Use this for initialization
	void Start () 
	{
		int index = Random.Range (0, objects.Length-1);
		Instantiate (objects [index], gameObject.transform.position, Quaternion.identity);
	}

}
