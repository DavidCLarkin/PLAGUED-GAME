using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour 
{
	public GameObject[] objects;

	// Use this for initialization
	void Start () 
	{
		int index = Random.Range (0, objects.Length);
		if (objects [index].tag == "AmmoBox" || objects[index].tag == "HealthBox") 
		{
			//objects [index].transform.eulerAngles = new Vector3 (90, 0, 0);
			Instantiate (objects [index], gameObject.transform.position, objects[index].transform.rotation*Quaternion.Euler(0,0,0));
			//print (objects [index].transform.eulerAngles.x);
		} 
		else 
			Instantiate (objects [index], gameObject.transform.position, Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () 
	{
	}
}
