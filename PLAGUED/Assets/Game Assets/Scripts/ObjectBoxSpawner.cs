using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBoxSpawner : MonoBehaviour 
{
	public GameObject[] objects;

	// Use this for initialization
	void Start () 
	{
		int index = Random.Range (0, objects.Length-1);
		Instantiate (objects [index], gameObject.transform.position, objects[index].transform.rotation*Quaternion.Euler(0,0,0));
	}

}
