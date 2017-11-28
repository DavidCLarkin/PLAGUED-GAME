using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLog : MonoBehaviour 
{
	public GameObject[] questLog;

	// Use this for initialization
	void Start () 
	{
		//foreach(GameObject quest in questLog)
		//{
		//	if (quest.GetComponent<Quest>().state == Quest.QuestState.ACTIVE)
		//		quest.GetComponent<Quest>().enabled = true;
		//}
	}
	
	// Update is called once per frame
	void Update () 
	{
		for(int i = 0; i < questLog.Length; i++) 
		{
			Quest currentquest = questLog [i].GetComponent<Quest>();
			if (currentquest.state == Quest.QuestState.COMPLETE || currentquest.state == Quest.QuestState.INACTIVE) 
			{
				currentquest.state = 
					(currentquest.state == Quest.QuestState.COMPLETE) 
					? Quest.QuestState.COMPLETE : Quest.QuestState.INACTIVE; //set complete if complete else set inactive
				
				if (currentquest.isChainQuest && currentquest.nextQuestInChain.GetComponent<Quest> ().state == Quest.QuestState.INACTIVE)
					currentquest.nextQuestInChain.GetComponent<Quest> ().state = Quest.QuestState.ACTIVE;
				
				questLog [i].SetActive (false); //if not active then it should be inactive
			}
			if(questLog[i].GetComponent<Quest>().state == Quest.QuestState.ACTIVE)
				questLog[i].SetActive (true); //if active, set object to active
		}
	}
}
