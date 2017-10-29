using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour 
{
	private bool isSpawning = false;
	public float minTime = 5.0f;
	public float maxTime = 15.0f;
	public GameObject[] enemies;  // Array of enemy prefabs.
	//private List<GameObject> currentEnemies = new List<GameObject>();
	//public int maximumEnemies;

	void Start()
	{
		foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
			EnemyHandler.currentEnemiesInScene.Add(enemy);
		
		if(EnemyHandler.maximumEnemies > 1)
			EnemyHandler.maximumEnemies -= 1;
	}

	IEnumerator SpawnObject(int index, float seconds)
	{
		yield return new WaitForSeconds(seconds);
		EnemyHandler.currentEnemiesInScene.Add(Instantiate(enemies[index], transform.position, transform.rotation));

		isSpawning = false;
	}

	void Update () 
	{
		foreach(GameObject e in EnemyHandler.currentEnemiesInScene)
		{
			if (e.GetComponent<ZombieManager> ().health <= 0)
				EnemyHandler.currentEnemiesInScene.Remove(e);
		}
		//only spawn one at a time
		if(!isSpawning && !(EnemyHandler.currentEnemiesInScene.Count > EnemyHandler.maximumEnemies))
		{
			isSpawning = true; //spawn zombie
			int enemyIndex = Random.Range(0, enemies.Length);
			StartCoroutine(SpawnObject(enemyIndex, Random.Range(minTime, maxTime)));
		}
	}
}
