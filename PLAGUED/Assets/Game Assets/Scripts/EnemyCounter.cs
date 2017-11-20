using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyCounter : MonoBehaviour 
{
	public int numberOfZombiesToKill;
	public int zombiesKilled;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (zombiesKilled == numberOfZombiesToKill)
			SceneManager.LoadScene ("BeatGame");
	}
}
