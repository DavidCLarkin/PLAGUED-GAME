using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour 
{
	public string name;
	public string description;
	public enum QuestType 
	{
		COLLECTIBLE,
		KILLING 
	};

	public enum QuestState
	{
		INACTIVE,
		ACTIVE,
		COMPLETE
	};

	public QuestType type;
	public QuestState state;

	public int amountToKill;
	public string objectName;
	private GameObject objectToCollect;
	public GameObject player;

	private bool completed;

	// Use this for initialization
	void Start () 
	{
		objectToCollect = GameObject.Find (objectName);
		completed = false;
	}

	//update
	void FixedUpdate () 
	{
		if (type == QuestType.COLLECTIBLE)
			collectibleQuest();
		if (type == QuestType.KILLING)
			killQuest();
		print (state);
		print (objectToCollect);
	}
		
	void collectibleQuest()
	{
		if (!objectToCollect.activeSelf)
		{
			state = QuestState.COMPLETE;
			completed = true;
		} 
		else 
		{
			state = QuestState.ACTIVE;
		}
	}

	void killQuest()
	{
		if(player.GetComponent<EnemyCounter>().zombiesKilled >= amountToKill) 
		{
			state = QuestState.COMPLETE;
			completed = true;
		} 
		else 
		{
			state = QuestState.ACTIVE;
		}
	}
}
