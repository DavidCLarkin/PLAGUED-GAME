using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour 
{
	public static EnemyHandler instance;
	public static List<GameObject> currentEnemiesInScene = new List<GameObject>();

	public static int maximumEnemies = 10;

	// Use this for initialization
	void Awake()
	{
		if(instance != null && instance != this)
		{
			Destroy (gameObject); //keep in game, handles AI amount
			return;
		}
		instance = this;
		DontDestroyOnLoad (gameObject);

	}

	void Start () 
	{
	}

}
