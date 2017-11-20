using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class KillCount : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		gameObject.GetComponent<Text> ().text = "Zombies Killed: " + GameObject.Find ("FPSController").GetComponent<EnemyCounter> ().zombiesKilled;
	}
}
