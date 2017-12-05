using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class QuestText : MonoBehaviour 
{
	public GameObject questManager;
	private GameObject[] questLog;
	private Text questDetails;
	private string QUEST = "Quest: ";


	// Use this for initialization
	void Start () 
	{
		questDetails = GetComponent<Text>();
		questLog = questManager.GetComponent<QuestLog>().questLog;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (questManager.activeSelf) 
		{
			foreach (GameObject quest in questLog) 
			{
				if (quest.GetComponent<Quest> ().state == Quest.QuestState.ACTIVE) 
				{
					questDetails.text = QUEST + quest.GetComponent<Quest> ().synopsis;
				}
			}
		}
	}
}
