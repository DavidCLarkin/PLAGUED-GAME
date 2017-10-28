using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour 
{

	bool isSpawning = false;
	public float minTime = 5.0f;
	public float maxTime = 15.0f;
	public GameObject[] enemies;  // Array of enemy prefabs.

	IEnumerator SpawnObject(int index, float seconds)
	{
		yield return new WaitForSeconds(seconds);
		Instantiate(enemies[index], transform.position, transform.rotation);

		//We've spawned, so now we could start another spawn     
		isSpawning = false;
	}

	void Update () 
	{
		//only spawn one at a time
		if(!isSpawning)
		{
			isSpawning = true; //spawn zombie
			int enemyIndex = Random.Range(0, enemies.Length);
			StartCoroutine(SpawnObject(enemyIndex, Random.Range(minTime, maxTime)));
		}
	}
}
