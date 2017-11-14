using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour 
{
	private bool isSpawning = false;
	public float minTime = 5.0f;
	public float maxTime = 15.0f;
	public GameObject[] enemies;  // Array of enemy prefabs.

	void Start()
	{
		foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
			EnemyHandler.currentEnemiesInScene.Add(enemy);
		
		if(EnemyHandler.maximumEnemies > 1)
			EnemyHandler.maximumEnemies -= 1; //so its the actual number. 0-based indexing accouted for

	}

	IEnumerator SpawnObject(int index, float seconds)
	{
		yield return new WaitForSeconds(seconds);
		EnemyHandler.currentEnemiesInScene.Add(Instantiate(enemies[index], transform.position, transform.rotation));

		isSpawning = false;
	}

	void Update () 
	{
		if (EnemyHandler.currentEnemiesInScene.Count != 0) 
		{
			for(int i = 0; i < EnemyHandler.currentEnemiesInScene.Count; i++)
			{ 
				if(EnemyHandler.currentEnemiesInScene[i].GetComponent<ZombieManager>().health <= 0)
				{
					EnemyHandler.currentEnemiesInScene.RemoveAt(i);
				}
			}
		}
		//only spawn one at a time
		if(!isSpawning && (EnemyHandler.currentEnemiesInScene.Count <= EnemyHandler.maximumEnemies))
		{
			isSpawning = true; //spawn zombie
			int enemyIndex = Random.Range(0, enemies.Length);
			StartCoroutine(SpawnObject(enemyIndex, Random.Range(minTime, maxTime)));
		}
	}
}
