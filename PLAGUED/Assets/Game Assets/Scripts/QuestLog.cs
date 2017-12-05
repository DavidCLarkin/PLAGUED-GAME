using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestLog : MonoBehaviour 
{
	public GameObject[] questLog;

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		//CHANGE THIS 
		for (int i = 0; i < questLog.Length; i++) 
		{
			Quest currentquest = questLog [i].GetComponent<Quest>();
			if (currentquest.state == Quest.QuestState.COMPLETE || currentquest.state == Quest.QuestState.INACTIVE) 
			{
				if(currentquest.state == Quest.QuestState.INACTIVE || currentquest.state == Quest.QuestState.COMPLETE)
				{
					if (currentquest.isChainQuest && currentquest.nextQuestInChain.GetComponent<Quest> ().state == Quest.QuestState.INACTIVE) 
					{
						//currentquest.nextQuestInChain.SetActive (true);
						currentquest.nextQuestInChain.GetComponent<Quest>().state = Quest.QuestState.ACTIVE;
					}
				}
				questLog [i].SetActive (false);
			}
			else if (currentquest.state == Quest.QuestState.ACTIVE)
				questLog [i].SetActive (true);
			/*if (currentquest.state == Quest.QuestState.COMPLETE || currentquest.state == Quest.QuestState.INACTIVE) 
			{
				currentquest.state = (currentquest.state == Quest.QuestState.COMPLETE) ? Quest.QuestState.COMPLETE : Quest.QuestState.INACTIVE; //set complete if complete else set inactive
				
				if (currentquest.isChainQuest && currentquest.nextQuestInChain.GetComponent<Quest> ().state == Quest.QuestState.INACTIVE)
					currentquest.nextQuestInChain.GetComponent<Quest> ().state = Quest.QuestState.ACTIVE;
				
				questLog [i].SetActive (false); //if not active then it should be inactive
			}

			if(questLog[i].GetComponent<Quest>().state == Quest.QuestState.ACTIVE)
				questLog[i].SetActive (true); //if active, set object to active
		}
		*/
		}
		if (checkAllQuestsComplete (questLog)) 
		{
			print ("All quests complete");
			SceneManager.LoadScene ("BeatGame");
		}
			
	}

	//check all quests are complete or not
	bool checkAllQuestsComplete(GameObject[] quests)
	{
		for (int i = 0; i < questLog.Length; i++) 
		{
			if (!(questLog [i].GetComponent<Quest> ().state == Quest.QuestState.COMPLETE))
				return false;
		}
		return true;
	}
}
