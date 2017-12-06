using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour 
{
	public string name;
	public string synopsis;
	public string description;
	public GameObject killCounter;

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
	public GameObject objectToCollect;
	public GameObject player;
	public bool isChainQuest;
	public GameObject nextQuestInChain;

	//private bool completed;

	// Use this for initialization
	void Awake()
	{

	}
	void Start () 
	{
		if (type == QuestType.COLLECTIBLE) 
		{
			objectToCollect.SetActive (true);
			//objectToCollect = GameObject.Find (objectName);
		}
		//completed = false;
	}

	//update
	void FixedUpdate () 
	{
		if(isChainQuest && nextQuestInChain.GetComponent<Quest>().state == Quest.QuestState.INACTIVE && state == QuestState.COMPLETE) //if chained questline, then set next one active
			nextQuestInChain.SetActive (true);

		if (type == QuestType.COLLECTIBLE)
			collectibleQuest();
		if (type == QuestType.KILLING)
			killQuest();

	}
		
	void collectibleQuest()
	{
		if (objectToCollect == null)
		{
			state = QuestState.COMPLETE;
			//completed = true;
		} 
		else 
		{
			state = this.state;
		}
	}

	void killQuest()
	{
		killCounter.SetActive(true);
		if(player.GetComponent<EnemyCounter>().zombiesKilled >= amountToKill)
		{
			state = QuestState.COMPLETE;
			//completed = true;
		} 
		else 
		{
			state = this.state;
		}
	}
}
