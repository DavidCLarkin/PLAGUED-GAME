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
		foreach (GameObject quest in questLog) 
		{
			if (quest.GetComponent<Quest> ().state == Quest.QuestState.COMPLETE)
				quest.GetComponent<Quest> ().enabled = false;
		}

	}
}
